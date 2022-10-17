using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnSelection : MonoBehaviour, IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{
    [Header("Liste des attaques associ�es")]
    public OnButtonOver[] atkButton;

    [Header ("Sprite de selection")]
    public GameObject selectionSprite;

    [Header ("Debug booleans")]
    public bool canBeSelected;

    public void Awake()
    {
        canBeSelected = false;
        atkButton = FindObjectsOfType<OnButtonOver>();
        selectionSprite = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (!canBeSelected)
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 87);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(canBeSelected)
        selectionSprite.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(canBeSelected)
        selectionSprite.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        if (canBeSelected)
        {
            atkButton[0].myCharacter.EntitySelected = GetComponent<Shadow>();
            atkButton[0].selection = false;
            
            StartCoroutine(AttackAnim());
            Debug.Log("Fonctionne");

        }
    }

    IEnumerator AttackAnim()
    {
        atkButton[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        canBeSelected = false;
        selectionSprite.SetActive(false);
        yield return new WaitForSeconds(1);
        atkButton[0].myCharacter.Attack();
        atkButton[0].selection = true;
    }

}