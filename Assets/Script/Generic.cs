using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public  class GetInactiveObject
    {
        public T GetObjectByTag<T>(string tag) where T : class
        {
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].CompareTag(tag))
                    {
                        return objs[i].GetComponent<T>();
                    }
                }
            }

            return null as T;
        }
    }
