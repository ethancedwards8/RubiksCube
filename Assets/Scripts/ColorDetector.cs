using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeColors
{
    public static Color White = new Color(1, 1, 1, 1);
    public static Color Yellow = new Color(1, 0.92f, 0.016f, 1);
    public static Color Red = new Color(1, 0, 0, 1);
    public static Color Orange = new Color(1, 0.64f, 0, 1);
    public static Color Blue = new Color(0, 0, 1, 1);
    public static Color Green = new Color(0, 1, 0, 1);

}

public class ColorDetector : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private GameObject colorCube;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            Color myColor = DetectColor();

            colorCube.GetComponent<MeshRenderer>().material.color = myColor;

        }
        if (Input.GetMouseButtonDown(1))
        {
            colorCube.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }

    private Color DetectColor()
    {
        RaycastHit hit;
        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit);

        if (hit.collider == null)
            return Color.white;

        return hit.transform.GetComponent<MeshRenderer>().material.color;
    }
}
