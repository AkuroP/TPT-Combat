using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityManager : MonoBehaviour
{
    [Header("Statistiques")] 
    public string name;
    public int level;
    public int hp;
    public int atk;
    public int def;
    public int satk;
    public int sdef;
    public int spe;
    public bool special = false;
    
    protected virtual void StatsManagement(int _hp, int _atk, int _def, int _satk, int _sdef, int _spe, bool special)
    {
        int dmg;
        if (special)
        {
            dmg = _satk / _sdef;
        }
        else
        {
            dmg = _atk / _def;
        }

        _hp -= dmg;
        hp = _hp;
    }

   // protected abstract void Zinedine();
}
