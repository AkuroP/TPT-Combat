using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnSelection : MonoBehaviour, IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{
    public Shadow enemy;

    [Header("BO")]
    public BattleOrderManager BattleOrder;

    [Header("Attaque choisie")]
    public string atkSelected;

    [Header("Liste des attaques associées")]
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
        enemy = GetComponent<Shadow>();
        BattleOrder = FindObjectOfType<BattleOrderManager>();
    }

    private void Update()
    {
        if (!canBeSelected)
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
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
            StartCoroutine(AttackAnim());
        }
    }

    IEnumerator AttackAnim()
    {
        int i = 0;
        while(i < atkButton.Length)
        {
            if(atkButton[i].nameOfAtk == atkSelected)
            {
                atkButton[i].myCharacter.EntitySelected = GetComponent<Shadow>();
                atkButton[i].selection = false;

                atkButton[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                atkButton[i].EveryEnemy(true);

                selectionSprite.SetActive(false);

                yield return new WaitForSeconds(1);
                // atkButton[i].myCharacter.Attack();
                BattleOrder.FightProgress();
                atkButton[i].selection = true;
                i++;
            }
            else
            {
                i++;
            }
        }

    }

    
}
