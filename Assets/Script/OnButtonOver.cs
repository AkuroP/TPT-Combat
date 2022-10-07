using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnButtonOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infos;
    public void OnPointerEnter(PointerEventData eventData)
    {
        infos.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infos.SetActive(false);
    }
}
