using UnityEngine;

[CreateAssetMenu(fileName = "WandBox", menuName = "NetherportaCode/WandBox", order = 2)]
public class WandBox : ScriptableObject
{

    //Create an instance of this data item for each wandbox
    //And then ensure the data is stored in wandBox folder in Data folder in Assets Folder
    public string id;

    
    public int length;

    public Wand wandinBox;
    
    

    
}

