using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public AudioSource[] wandLidClose;
    public AudioSource IBelieveICanFly;
    public AudioSource[] wingardiumLeviosa;

    //Going to add 10 or so sound recording, and make a randomizer which selects which recording to play.
    public void CloseLidSFX()
    {
        int random = Random.Range(0, wandLidClose.Length);
        wandLidClose[random].Play();
    }

    public void FlightPowerup()
    {
        int easterEgg = Random.Range(0, 2);
        if (easterEgg == 1)
        {
            IBelieveICanFly.Play();
        }
        else
        {
            int random = Random.Range(0, wingardiumLeviosa.Length);
            wingardiumLeviosa[random].Play();
        }
    }
}
