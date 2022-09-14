using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igris : EntityManager
{

    void Start()
    {
        
    }

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.A))
        {
            StatsManagement(hp, atk, def, satk, sdef, spe, special);                               
        }
    }

    protected override void StatsManagement(int _hp, int _atk, int _def, int _satk, int _sdef, int _spe, bool special)
    {
        base.StatsManagement(_hp, _atk, _def, _satk, _sdef, _spe, special);
        
        Debug.Log("Il ne vous reste plus que " + hp + " points de vie");

    }




}
