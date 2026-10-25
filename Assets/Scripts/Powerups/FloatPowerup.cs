//User casts wingardium leviosa on themselves after learning the spell, hence they can fly for a little bit (let's say 30 seconds or so). 
using Unity.VisualScripting;
using UnityEngine;

public class FloatPowerup : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] float flightDuration;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            movement.StartFlight(flightDuration);
        }
    }
}
