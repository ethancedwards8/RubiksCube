using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class CubeController : MonoBehaviour
{
    [SerializeField] List<CenterController> centers = new List<CenterController>();

    Scramble scramble;

    // public because I need to access them outside the class
    public bool scrambled;
    public bool IsRotating;

    void Start()
    {
        Application.targetFrameRate = 60;

        StartCoroutine(Scramble());

        scramble = new Scramble(50);

        //Debug.Log(scramble.GetScramble());
        //scramble = new Scramble("R  U  R' U' ");

    }

    void Update()
    {
        // Rotate the whole cube 
        if (Input.GetKeyDown(KeyCode.X))
            Rotate(KeyCode.X, Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        else if (Input.GetKeyDown(KeyCode.Y))
            Rotate(KeyCode.Y, Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        else if (Input.GetKeyDown(KeyCode.Z))
            Rotate(KeyCode.Z, Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

        // Go back to menu
        if (Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        int solved = 0;
        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == 6)
            {
                // force pieces to snap in place
                Vector3 newPos = new Vector3Int((int)Mathf.Round(child.transform.localPosition.x), (int)Mathf.Round(child.transform.localPosition.y), (int)Mathf.Round(child.transform.localPosition.z));
                child.transform.localPosition = newPos;
                //Debug.Log($"{newPos}");

                // check for solved status
                if (child.transform.localRotation.eulerAngles.Equals(new Vector3(0, 0, 0).normalized))
                {
                    //Debug.Log(child.transform.localRotation.eulerAngles + " " + child.name);
                    solved++;
                }
                else
                {
                    //Debug.Log(child.transform.localRotation.eulerAngles + " " + child.name + " " + Quaternion.Euler(new Vector3(0, 0, 0)));
                }
            }
        }
        if (solved >= 20)
        {
            GameObject.Find("CubeController").GetComponent<UIController>().solved = true;
        }
        else
        {
            GameObject.Find("CubeController").GetComponent<UIController>().solved = false;

        }

    }

    // Rotate the cube
    void Rotate(KeyCode key, bool reverse)
    {
        int dir = reverse ? -90 : 90;

        switch (key)
        {
            case KeyCode.X:
                transform.Rotate(dir, 0, 0);
                break;

            case KeyCode.Y:
                transform.Rotate(0, dir, 0);
                break;

            case KeyCode.Z:
                transform.Rotate(0, 0, dir);
                break;
        }
    }

    IEnumerator Scramble()
    {
        while (true)
        {


            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.S)); // weird hack


            //Debug.Log("pressed");

            var interpret = scramble.InterpretScramble();

            for (int i = 0; i < interpret.sides.Count; i++)
            {
                //Debug.Log("inter " + i + " side " + interpret.sides[i] + " num " + interpret.measures[i]);

                yield return new WaitUntil(() => IsRotating == false); // wait until the cube isn't moving

                // https://stackoverflow.com/questions/16177225/find-element-in-list-that-contains-a-value thank you STO
                centers.Find(c => c.side == interpret.sides[i]).RotatePiece(interpret.sides[i], interpret.measures[i] == -90, false, interpret.measures[i] == 180);
                yield return new WaitUntil(() => IsRotating == false);

            }

            scrambled = true;
            GameObject.Find("CubeController").GetComponent<UIController>().UpdateInstructions("Use UDBFRL to control sides. Happy solving!");

        }
    }
}
