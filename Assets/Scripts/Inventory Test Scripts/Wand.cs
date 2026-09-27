using UnityEngine;

[CreateAssetMenu(fileName = "Wand", menuName = "NetherportaCode/Wand", order = 2)]
public class Wand : ScriptableObject
{

    public string id;

    

    
    public enum CoreType {Unicorn_hair,Phoenix_tail_feather,Dragon_heartstring,ThunderBird_tail_feather, Thestral_tail_hair, Troll_whisker, Coral, Dittany_stalk, Wampus_cat_hair, White_river_monster_spine, Desi_Cock_Beak, King_rats_tail_essence, Horned_serpent_horn, Harambes_bravery, Kelpie_hair, Basilisk_tooth, African_mermaid_scale, World_serpents_shed_skin, Snidget_tail_feather,  Fairy_wing}
    //desi cock beak
    //
    
    public CoreType core;

    public enum WoodType {BeechWood,Willow,Mahogany,Yew,Maple,Ebony,Holly,ElderWood, AppleWood, MangoWood}

    public WoodType wood;

    public string CorrectStorageLocation;

    public string AttributeText;

    public GameObject prefab;
}

