//Trying to make it so that this works in editor, and not in play mode, as that would significantly reduce lag probably.
using UnityEngine;
using System;

public class AssignWandData : MonoBehaviour
{
    
    [SerializeField]
    private Wand WandDatatoInstance;
    [Header("CopiedData")]
    private Wand WandData;
    [Header("How many CoreTypes do we have?")]
    [SerializeField]
    private int Countwandcores;
    [Header("How many wood types do we have?")]
    [SerializeField]
    private int Countwoodtypes;
    Wand CreateInstance()
    {
        Wand item = Instantiate(WandDatatoInstance);
        return item;
    }


    
    
    
    // public string WandBoxname;

    
    
    
   

    

    void Awake()
    {
        Countwandcores  = Wand.CoreType.GetValues(typeof(Wand.CoreType)).Length;
        Countwoodtypes  = Wand.WoodType.GetValues(typeof(Wand.WoodType)).Length;
        foreach (Transform wand in transform)
        {
            if (wand.GetComponent<DroppedItem>() == null){break;}
            else{ 
                
                Wand wanditem = CreateInstance();
                var id = Guid.NewGuid().ToString();
                wanditem.id = id;
                Wand.CoreType wandcoreInstance = (Wand.CoreType)UnityEngine.Random.Range(0,Countwandcores);
                float wandlength = wand.localScale.y;
                float wandscale = ReturnWandLength(wandlength);

                Wand.WoodType wandWoodInstance = (Wand.WoodType)UnityEngine.Random.Range(0,Countwoodtypes);

                wanditem.core = wandcoreInstance;
                wanditem.length = wandscale;
                wanditem.wood = wandWoodInstance;

                var WandData = wand.GetComponent<DroppedItem>();
                WandData.item = wanditem;
                
            }
        }
    }




    float ReturnWandLength(float wandlength)
    {
        float wandscale = 10;
                if (wandlength == 1)
                {
                    wandscale = 8;
                }
                if (wandlength == 1.1)
                {
                    wandscale = 8.5f;
                }
                if (wandlength == 1.2)
                {
                    wandscale = 9f;
                }
                if (wandlength == 1.3)
                {
                    wandscale = 9.5f;
                }
                if (wandlength == 1.4)
                {
                    wandscale = 10f;
                }
                if (wandlength == 1.5)
                {
                    wandscale = 10.5f;
                }
                if (wandlength == 1.6)
                {
                    wandscale = 11;
                }
                if (wandlength == 1.7)
                {
                    wandscale = 11.5f;
                }
                if (wandlength == 1.8)
                {
                    wandscale = 12f;
                }
                return wandscale;
    }
}

