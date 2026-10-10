using UnityEngine;
using UnityEngine.InputSystem.Utilities;

[CreateAssetMenu(fileName = "PlayerData", menuName = "NetherportaCode/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    //Stores all actions the player might need to perform
    //Onlu works within a single game object

    public PlayerEvents Events;
}