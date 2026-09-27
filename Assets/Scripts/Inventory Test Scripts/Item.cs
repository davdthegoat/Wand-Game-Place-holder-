using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName  = "NetherportaCode/Item", order =  1)]
public class Item : ScriptableObject
{
    public string id;

    
    public enum CoreType {Unicorn_Hair,Phoenix_Tail_Feather,Dragon_Heartstring}
    
    public CoreType core;

    public enum WoodType {BeechWood,Willow,Mahogany,Yew,Maple,Ebony,Holly}

    public WoodType wood;

    public string CorrectStorageLocation;

    public string AttributeText;

    public GameObject prefab;
}
