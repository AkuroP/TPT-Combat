using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Shadow : MonoBehaviour
{
    public SpellsData spellSelected;

    public List<SpellsData> ListOfSpells;

    private OnSelection _onSelection;
    public BattleOrderManager BO;
    public BattleHUD mobsHealthHUD;
    public BattleHUD playerHealthHUD;
    public List<GameObject> mobsInLife = new List<GameObject>();
    public GameObject transition;
    [Header("Type de personnage")]
    public bool anEnemy;
    public bool isBoss;
    private bool fightBoss;

    [Header("Statistiques")] 
    public String _Name;
    public int maxHP;
    public int currentHP;
    public int atk;
    public int sAtk;
    public int def;
    public int sDef;
    public int speed;
    public int mana;

    public int lvl;
    public int exp;

    public float damageFormule;
    public float damages;

    [Header("Status")]
    public bool burned;
    public bool frozen;
    public bool paralyzed;

    [Header("Debug")]
    public EntityData MyEntity;
    
    public Shadow EntitySelected;

    public static Shadow instance;

    private void Awake()
    {
        
        if(instance == null)
            return;
        instance = this;
    }

    private void Start()
    {
        _onSelection = GetComponent<OnSelection>();
        
    }
   
    void OnEnable()
    {
        if (!anEnemy)
        {
            mobsInLife.AddRange(GameObject.FindGameObjectsWithTag("MobStation"));
        }
        
        StartCoroutine(InitEntity());
    }
    
    

    void OnDisable()
    {
        EntitySelected = null;
        
        if (anEnemy)
        {
            InitVar(ref MyEntity, ref _Name, ref maxHP, ref currentHP, ref atk, ref sAtk, ref def, ref sDef, ref speed,
                ref burned, ref frozen, ref paralyzed,ref  ListOfSpells); 
        }
        else
        {
            transition.SetActive(false);
            mana = 1000;
        }



    }

    IEnumerator InitEntity()
    {
        
        
        yield return new WaitForSeconds(1f);
        
        _Name = MyEntity._Name;
        maxHP = MyEntity._hp;
        atk = MyEntity._atk;
        sAtk = MyEntity._sAtk;
        def = MyEntity._def;
        sDef = MyEntity._sDef;
        speed = MyEntity._speed;

        currentHP = maxHP;
        
        burned = MyEntity._burned;
        frozen = MyEntity._frozen;
        paralyzed = MyEntity._paralyzed;

        anEnemy = MyEntity._anEnemy;
        ListOfSpells = MyEntity._ListOfSpells;
    
        BO = FindObjectOfType<BattleOrderManager>();
        BO.Shadows.Add(this);

        IsAnEnemy();
    }
 
    public void InitVar(ref EntityData entityData,ref String _Name, ref int maxhp, ref int CurrentHp, ref int atk, ref int sAtk, ref int def, ref int sDef, ref int speed, 
        ref bool burned, ref bool frozen, ref bool paralyzed, ref List<SpellsData> ListOfSpells)
    {
        entityData = null;
        _Name = null;

        maxhp = 0; CurrentHp = 0;        atk =  0;        sAtk = 0;
        def =  0;        sDef =  0;        speed =  0;
        
        burned = false;
        frozen = false;
        paralyzed = false;
        ListOfSpells = null;
    }
    

    // Update is called once per frame
    void Update()
    {

        if(EntitySelected != null)
            damageFormule = spellSelected._power * atk / EntitySelected.def;
        
        if (EntitySelected is not null && !anEnemy && EntitySelected.isBoss)
        {
            fightBoss = true;
        }

        if (burned)
        {
            atk /= 2;
        }

        if (currentHP <= -0.1f && !anEnemy)
        {
            // Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
        else if (currentHP <= -0.1f && anEnemy)
        {
            if (EntitySelected != null) EntitySelected.EntitySelected = null;
            EntitySelected?.KilledOpponent(this.gameObject);
            
        }

        if (!anEnemy)
        {
            StartCoroutine(OneEnnemyLeft());
            if (AreOpponentsDead())
            {
                
                StartCoroutine(Victory());
            }
            
        }
        else
        {
            EntitySelected = GameObject.FindGameObjectWithTag("PlayerStation").GetComponent<Shadow>();
        }

    }

    IEnumerator OneEnnemyLeft()
    {
        
        yield return new WaitUntil(() => mobsInLife.Count == 1);
        if (mobsInLife.Count > 0)
        {
           EntitySelected = mobsInLife[^1].GetComponent<Shadow>();
        }
        
        
    }

    IEnumerator Victory()
    {
        transition.SetActive(true);
        Destroy(Habillage.instance.Getmob.gameObject);
        
        yield return new WaitForSeconds(1.2f);
        
        if (fightBoss)
        {
            SceneManager.LoadScene("Generique");
        }
        GameManager.instance.gameState = GameManager.GameState.Adventure;
        transition.GetComponent<Animator>().SetTrigger("Transition");
        
        AudioManager.instance.DestroyOST();
        AudioManager.instance.PlayClipAt(AudioManager.instance.allAudio["exploration"], GameObject.FindWithTag("Player").transform.position, AudioManager.instance.ostMixer, false);
        
        yield return new WaitForSeconds(1.2f);
        
        GameManager.instance.ShowMM();
        Habillage.instance.Getmob = null;
        
        transition.SetActive(false);
    }

    public void IsAnEnemy()
    {
        if (anEnemy)
        {
            EntitySelected = GameObject.FindGameObjectWithTag("PlayerStation").GetComponent<Shadow>();

            int randAtk = Random.Range(0, ListOfSpells.Count);
            spellSelected = ListOfSpells[randAtk];
        }
    }
    
    public void Attack()
    {
        
            if (mana >= spellSelected._manaCost)
            {
                if (paralyzed)
                {
                    int cucked = Random.Range(0, 100);

                    if (cucked >= 25)
                        SuccessfulAttack();

                    else
                    {
                        Debug.Log("T'es Paralys� , pas de chance mon boug ! " + MyEntity._Name);
                    }

                }
                else if (frozen)
                {
                    Debug.Log("T'es gel� zebi Kekw " + MyEntity._Name);
                }
                else
                {
                    SuccessfulAttack();
                }

            }
            else
            {
                Debug.Log("T'as plus de mana " + MyEntity._Name);
            }
        
        
    }

    public void SuccessfulAttack()
    {
        // Debug.Log("Attaque r�ussie !, " + MyEntity._Name + " a inflig� " + damageFormule + " d�g�ts avec " + spellSelected._name );
        mana -= spellSelected._manaCost;
        EntitySelected.currentHP -= (int)damageFormule;
        AudioSource sfx = AudioManager.instance.PlayClipAt(AudioManager.instance.allAudio[spellSelected._name], EntitySelected.transform.position, AudioManager.instance.soundEffectMixer, true);
        Instantiate(GameManager.instance.spells[spellSelected._name], EntitySelected.transform);

        if (!anEnemy)
        {
            EntitySelected.mobsHealthHUD.SetHP(EntitySelected.currentHP);
            
        }
        else
        {
            playerHealthHUD.SetHP(EntitySelected.currentHP); 
        }
               

        if(spellSelected.currentStatus == SpellsData.Status.Paralyze)
        {
            EntitySelected.paralyzed = true;
        }
        if (spellSelected.currentStatus == SpellsData.Status.Frozen)
        {
            EntitySelected.frozen = true;
        }
    }
    
    public void KilledOpponent(GameObject opponent)
    {
        if(mobsInLife.Contains(opponent))
        {
            opponent.SetActive(false);
            mobsInLife.Remove(opponent);
        }
        
    }
 
    public bool AreOpponentsDead()
    {
        if(mobsInLife.Count <= 0)
        {
            //dead
            return true;
        }
        else
        {
            //tss alive
            return false;
        }
    }
}
