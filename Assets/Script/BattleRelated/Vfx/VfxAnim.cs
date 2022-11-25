using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxAnim : MonoBehaviour
{
    public bool animDestroy;
    
    public void AnimEndDestroy()
    {
        if(!animDestroy)return;
        Destroy(this.gameObject);
    }
}
