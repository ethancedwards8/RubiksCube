using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deprecated code, was going to try to fix the jumbling issue by positioning the pieces.
public class PiecePositioner : MonoBehaviour
{
    private List<GameObject> pieces = new List<GameObject>(), centers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == 6)
            {
                pieces.Add(child.gameObject);
            }
        }
        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == 7) // not sure why using the layerMask doesn't work.
            {
                centers.Add(child.gameObject);
            }
        }

        //Debug.Log($"{centers.Count} and {pieces.Count}");
        //centers.ForEach(f => f.GetComponent<MeshRenderer>().material.color = Color.blue);
        //pieces.ForEach(f => f.GetComponent<MeshRenderer>().material.color = Color.gray);
    }

    void PositionPieces()
    {
    }

    void PositionCenters()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
