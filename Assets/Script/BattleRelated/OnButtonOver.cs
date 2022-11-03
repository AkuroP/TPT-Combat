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

    public bool selection;
    
    [Header("Debug")]

    public OnButtonOver[] spellList;
    public string nameOfAtk;
    public void Awake()
    {
        selection = true;
        myCharacter = GameObject.FindGameObjectWithTag("PlayerStation").GetComponent<Shadow>();
        enemyToSelect = FindObjectsOfType<OnSelection>();
        spellList = FindObjectsOfType<OnButtonOver>();
        nameOfAtk = this.name;

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
            myCharacter.spellSelected = GetComponentInParent<FightSystem>().spellData;
            print(myCharacter.spellSelected);
            
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

            EveryEnemy(false);
                    
        }
    }

    public void EveryEnemy(bool t)
    {
        int i = 0;
        while (i < enemyToSelect.Length)
        {
            if (!t)
            {
                enemyToSelect[i].canBeSelected = true;
                enemyToSelect[i].atkSelected = nameOfAtk;
                i++;
            }
            else
            {
                enemyToSelect[i].canBeSelected = false;
                i++;
            }
            
        }
    }

}
