//User casts wingardium leviosa on themselves after learning the spell, hence they can fly for a little bit (let's say 30 seconds or so). 
using Unity.VisualScripting;
using UnityEngine;

public class FloatPowerup : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] float flightDuration;
    [SerializeField] float cooldown;
    [SerializeField] SoundEffectsManager SFXmanager;
    [Header("StartFloatVars: ")]
    [SerializeField]float hoverHeight = 3f; 
    [SerializeField]float riseTime = 0.8f;
    [SerializeField]float overshoot = 1.70158f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            movement.StartFlight(flightDuration, cooldown, hoverHeight, riseTime, overshoot);

            //Play SFX
            SFXmanager.FlightPowerup();
        }
    }
}
