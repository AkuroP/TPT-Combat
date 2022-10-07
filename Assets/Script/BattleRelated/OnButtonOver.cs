using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnButtonOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject infos;
    public void OnPointerEnter(PointerEventData eventData)
    {
        infos.SetActive(true);
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 40);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infos.SetActive(false);
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
}
