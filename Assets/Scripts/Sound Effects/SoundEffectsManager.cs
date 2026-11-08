using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public AudioSource[] wandLidClose;

    //Going to add 10 or so sound recording, and make a randomizer which selects which recording to play.
    public void CloseLidSFX()
    {
        int random = Random.Range(0, wandLidClose.Length);
        wandLidClose[random].Play();
    }
}
