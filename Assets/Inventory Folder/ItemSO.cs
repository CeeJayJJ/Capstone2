using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;
    public AttributeToChange attributesToChange = new AttributeToChange();
    public int amountToChangeAttribute;


    public bool UseItem()
    {
        if(statToChange == StatToChange.Techbar)
        {
            PlayerMovement playerMovement = GameObject.Find("KaiMC_universal_82").GetComponent<PlayerMovement>();
            if(playerMovement.techbar == playerMovement.maxTechbar)
            {
                return false;
            }
            else
            {
            playerMovement.ChangeTechbar(amountToChangeStat);
                return true;
            }
            
        }
        return false;
    }
    public enum StatToChange
    {
        none,
        Techbar,
        Time
    };

    public enum AttributeToChange
    {
        none, 
        Agility
    };
}
