using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    //[SerializeField] CenterController[] centers = new CenterController[6];
    [SerializeField] List<CenterController> centers = new List<CenterController>();

    Scramble scramble;
    public static bool IsRotating;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
        scramble = new Scramble(20);
        Debug.Log(scramble.GetScramble());
        //scramble = new Scramble("R  U  R' U' ");

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Move()
    {
        while (true)
        {


            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.S)); // weird hack

            if (true)
            {
                //Debug.Log("pressed");

                var interpret = scramble.InterpretScramble();

                for (int i = 0; i < interpret.sides.Count; i++)
                {

                    yield return Utility.wait(1f);

                    //Debug.Log("inter " + i + " side " + interpret.sides[i] + " num " + interpret.measures[i]);

                    yield return new WaitUntil(() => IsRotating == false);
                    // https://stackoverflow.com/questions/16177225/find-element-in-list-that-contains-a-value
                    centers.Find(c => c.side == interpret.sides[i]).RotatePiece(interpret.sides[i], interpret.measures[i] == -90, false, interpret.measures[i] == 180);
                    yield return new WaitUntil(() => IsRotating == false);

                }

            }
        }
    }
}
