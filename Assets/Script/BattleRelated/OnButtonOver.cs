using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class OnButtonOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject infos;

    [Header("Entity selection")]
    public Shadow myCharacter;
    public OnSelection[] enemyToSelect;

    [Header("Debug booleans")]
    public bool selection;

    public void Awake()
    {
        selection = true;
        myCharacter = GetComponentInChildren<Shadow>();
        enemyToSelect = FindObjectsOfType<OnSelection>();
    }

    private void Update()
    {
        if (!selection)
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            infos.SetActive(false);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selection)
        {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 40);
        infos.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selection)
        {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        infos.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        if (selection)
        {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            enemyToSelect[0].canBeSelected = true;
            selection = false;
        }
    }

}
