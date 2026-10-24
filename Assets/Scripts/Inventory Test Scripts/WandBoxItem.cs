using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WandBoxItem : MonoBehaviour

{

     WandBox CreateInstance()
    {
        WandBox item = Instantiate(itemToInstantiate);
        return item;
    }


    [Header("FOR VIEWING ONLY")]
    public WandBox wandboxitem;

    [Header("WANDBOXLENGTH")]
    public int length = 0;
    public string WandBoxname;

    
    [SerializeField] WandBox itemToInstantiate; //Set this in the inspector

    
    void OnEnable()
    {
        var path = "WandBox/" + WandBoxname;
        itemToInstantiate = Resources.Load<WandBox>(path);

        wandboxitem = CreateInstance();
        var id = Guid.NewGuid().ToString();
        wandboxitem.id = id;
        wandboxitem.length = length;
    }






}
