using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeColors
{
    // https://www.schemecolor.com/rubik-cube-colors.php
    // http://answers.unity.com/answers/1243599/view.html
    public static Color White = new Color32(0xFF, 0xFF, 0xFF, 1); // FFFFFF
    public static Color Yellow = new Color32(0xFF, 0xD5, 0x00, 1); // FFD500
    public static Color Red = new Color32(0xB9, 0x00, 0x00, 1); // B90000
    public static Color Orange = new Color32(0xFF, 0x59, 0x00, 1); // FF5900
    public static Color Blue = new Color32(0x00, 0x45, 0xAD, 1); // 0045AD
    public static Color Green = new Color32(0x00, 0x9B, 0x48, 1); // 009B48

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
