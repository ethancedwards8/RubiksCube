using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// Change font on click
public class FontChanger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] TMP_Text text;

    [SerializeField] TMP_FontAsset[] fonts;

    public void OnPointerDown(PointerEventData eventData)
    {
        text.font = fonts[0];
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        text.font = fonts[1];
    }
}
