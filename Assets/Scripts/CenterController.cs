using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Side
{
    public static KeyCode UP = KeyCode.U;
    public static KeyCode FRONT = KeyCode.F;
    public static KeyCode LEFT = KeyCode.L;
    public static KeyCode RIGHT = KeyCode.R;
    public static KeyCode BACK = KeyCode.B;
    public static KeyCode DOWN = KeyCode.D;
}

public enum SIDE
{
    UP,
    FRONT,
    LEFT,
    RIGHT,
    BACK,
    DOWN
}

public class CenterController : MonoBehaviour
{

    [SerializeField] LayerMask pieceMask;
    [SerializeField] public SIDE side;

    private GameObject cubeParent;

    public static bool IsRotating;

    private (KeyCode key, bool reverse) dataStore;

    void Start()
    {
        IsRotating = false;
        //pieceMask = LayerMask.NameToLayer("Piece");
        cubeParent = GameObject.Find("WholeCube");

        switch (side)
        {
            case SIDE.UP:
                dataStore.key = Side.UP;
                dataStore.reverse = true;
                break;
            case SIDE.FRONT:
                dataStore.key = Side.FRONT;
                dataStore.reverse = false;
                break;
            case SIDE.LEFT:
                dataStore.key = Side.LEFT;
                dataStore.reverse = false;
                break;
            case SIDE.RIGHT:
                dataStore.key = Side.RIGHT;
                dataStore.reverse = true;
                break;
            case SIDE.BACK:
                dataStore.key = Side.BACK;
                dataStore.reverse = true;
                break;
            case SIDE.DOWN:
                dataStore.key = Side.DOWN;
                dataStore.reverse = false;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(dataStore.key))
        {
            RotatePiece(side, dataStore.reverse, (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)));
        }
    }

    public void RotatePiece(SIDE side, bool reverse, bool caps = false, bool twice = false)
    {
        CubeController.IsRotating = true; // tell the pieces not to rotate

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, .75f, pieceMask); // hit all correct pieces

        foreach (var hit in hitColliders)
        {
            hit.gameObject.transform.SetParent(transform); // set all correct pieces to child of center
        }

        transform.Rotate(CalcRotation(side, reverse ^ caps, twice)); // rotate center

        foreach (var hit in hitColliders)
        {
            hit.gameObject.transform.SetParent(cubeParent.transform); // reset all correct pieces to child of cube
        }
        
        CubeController.IsRotating = false; // tell pieces to rotate
    }


    public static Vector3 CalcRotation(SIDE side, bool reverse, bool twice = false)
    {
        int dir = twice ? 180 : reverse ? 90 : -90;
        Vector3 res = new Vector3(0, 0, 0);

        switch (side)
        {
            case SIDE.UP:
            case SIDE.DOWN:
                res = new Vector3(0, dir, 0);
                break;

            case SIDE.LEFT:
            case SIDE.RIGHT:
                res = new Vector3(dir, 0, 0);
                break;

            case SIDE.FRONT:
            case SIDE.BACK:
                res = new Vector3(0, 0, dir);
                break;

            default:
                res = new Vector3(0, 0, 0);
                break;
        }

        return res;
    }
}
