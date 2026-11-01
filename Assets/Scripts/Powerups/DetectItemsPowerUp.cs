//Uses inventory to detect items of
//(i) the same core nearby and
//(ii) same wood type nearby.
//These are two DIFFERENT abilities, but use the same script. Make sure you separate them. I, DVD will not work on this because this involves the inventory heavily.
using UnityEngine;

public class DetectItemsPowerUp : PlayerSystem
{
    
    [SerializeField] PowerUpFunctions powerUpFunctions;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        
    }

    //should handle subsciption of the powerup items
    // should handle invoking of powerup items
    public void subleviosa()
    {
        playerIdentification.Playerdata.Events.leviosa += powerUpFunctions.Floatpowerup;        
    }

    public void unsubleviosa()
    {
        playerIdentification.Playerdata.Events.leviosa -= powerUpFunctions.Floatpowerup;
    }

    public void unsubhighlight()
    {
        
    }

    public void subhighlight()
    {
        
    }

    void OnEnable()
    {
        playerIdentification.Playerdata.Events.subLeviosa += subleviosa;
        playerIdentification.Playerdata.Events.unsubLeviosa += unsubleviosa;

        playerIdentification.Playerdata.Events.subHighlight += subhighlight;
        playerIdentification.Playerdata.Events.unsubHighlight += unsubhighlight;
    }
    void OnDisable()
    {
        playerIdentification.Playerdata.Events.subLeviosa -= subleviosa;
        playerIdentification.Playerdata.Events.unsubLeviosa -= unsubleviosa;

        playerIdentification.Playerdata.Events.subHighlight -= subhighlight;
        playerIdentification.Playerdata.Events.unsubHighlight -= unsubhighlight;
    }


}
