using UnityEngine;
using UnityEngine.InputSystem.Utilities;

[CreateAssetMenu(fileName = "ShelfData", menuName = "NetherportaCode/ShelfData", order = 2)]
public class ShelfData : ScriptableObject
{
    //Stores all actions the player might need to perform
    //Onlu works within a single game object

    public ShelfEvents Events;
}