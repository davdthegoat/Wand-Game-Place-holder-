//Trying to make it so that this works in editor, and not in play mode, as that would significantly reduce lag probably.
using UnityEngine;
using System.Collections;
using System;

public class AssignWandData : MonoBehaviour
{
    
    [SerializeField]
    private Wand WandDatatoInstance;
    [Header("CopiedData")]
    private Wand WandData;

    Wand CreateInstance()
    {
        Wand item = Instantiate(WandDatatoInstance);
        return item;
    }


    
    
    
    // public string WandBoxname;

    
    
    
   

    

    void Awake()
    {
        foreach (Transform wand in transform)
        {
            if (wand.GetComponent<DroppedItem>() == null){break;}
            else{ 
                
                Wand wanditem = CreateInstance();
                var id = Guid.NewGuid().ToString();
                wanditem.id = id;

                
            }
        }
    }
}

