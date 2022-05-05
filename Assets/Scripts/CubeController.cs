using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CubeController : MonoBehaviour
{
    //[SerializeField] CenterController[] centers = new CenterController[6];
    [SerializeField] List<CenterController> centers = new List<CenterController>();

    Scramble scramble;

    public bool scrambled;

    public bool IsRotating;

    void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(Move());
        scramble = new Scramble(8);
        //Debug.Log(scramble.GetScramble());
        //scramble = new Scramble("R  U  R' U' ");

    }

    void Update()
    {
        int solved = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == 6)
            {
                Vector3 newPos = new Vector3Int((int)Mathf.Round(child.transform.localPosition.x), (int)Mathf.Round(child.transform.localPosition.y), (int)Mathf.Round(child.transform.localPosition.z));

                child.transform.localPosition = newPos;
                //Debug.Log($"{newPos}");

                if (child.transform.rotation.Equals(Quaternion.Euler(new Vector3(0, 0, 0).normalized).normalized))
                {
                    Debug.Log(child.transform.rotation + " " + child.name);
                    solved++;
                }
                else
                {
                    Debug.Log(child.transform.rotation + " " + child.name + " " + Quaternion.Euler(new Vector3(0, 0, 0)));
                }
            }
        }
        if (solved >= 20)
        {
            GameObject.Find("CubeController").GetComponent<UIController>().solved = true;
        } else
        {
            GameObject.Find("CubeController").GetComponent<UIController>().solved = false; 

        }
    }

    IEnumerator Move()
    {
        while (true)
        {


            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.S)); // weird hack


            //Debug.Log("pressed");

            var interpret = scramble.InterpretScramble();

            for (int i = 0; i < interpret.sides.Count; i++)
            {

                //yield return Utility.wait(.2f);

                //Debug.Log("inter " + i + " side " + interpret.sides[i] + " num " + interpret.measures[i]);

                yield return new WaitUntil(() => IsRotating == false);
                // https://stackoverflow.com/questions/16177225/find-element-in-list-that-contains-a-value
                centers.Find(c => c.side == interpret.sides[i]).RotatePiece(interpret.sides[i], interpret.measures[i] == -90, false, interpret.measures[i] == 180);
                yield return new WaitUntil(() => IsRotating == false);

            }

            scrambled = true;

        }
    }
}
