using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    //public string[] lines;
    public List<string> lines;
    public float textSpeed;

    private int index;
    public bool isIntroDialog;
    public GameObject intro;

    public PlayerBehaviour player;
    
    void OnEnable()
    {
        textComponent.text = string.Empty;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        StartDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
        
    }
    
    private void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        //Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Count - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            if (isIntroDialog)
                intro.SetActive(false);
            
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        player.SwitchActionMap("Player");
    }
}
