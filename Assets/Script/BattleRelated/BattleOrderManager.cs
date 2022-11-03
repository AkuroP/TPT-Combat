using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOrderManager : MonoBehaviour
{
    public List<Shadow> Shadows;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            Test();
    }

    private void Test()
    {

        for (int i = 0; i < Shadows.Count - 1; ++i)
        {
            for(int j = i + 1; j < Shadows.Count; ++j)
            {
                if(Shadows[j].speed > Shadows[i].speed)
                {
                    Shadow zbi = Shadows[i];
                    Shadows[i] = Shadows [j];
                    Shadows[j] = zbi;
                }

                else if(Shadows[j].speed == Shadows[i].speed)
                {
                    int c = Random.Range(0, 100);
                    if(c >= 50)
                    {
                        Shadow zbi = Shadows[i];
                        Shadows[i] = Shadows[j];
                        Shadows[j] = zbi;
                    }
                }
            }
        }
    }

}
