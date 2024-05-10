using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    public string characterName;
    //Character's base stats
    [SerializeField]
    public float baseHealth, baseStrengthStat, baseDefenceStat;
    //Character's stats after item stats applied
    public float health, strengthStat, defenceStat;
    //Used for health bar
    public float maxHealth;
    // match start needs to be true so health, strength and defence does not change
    [SerializeField]
    public bool matchStarted = false;
    public bool characterDead = false;


    protected Rigidbody rb;


    List<Item> inventory = new List<Item>();
    
    void Awake()
    {
        health = baseHealth; strengthStat = baseStrengthStat; defenceStat = baseDefenceStat;
/*        [NOT IMPLEMENTED]
 *        foreach (var item in inventory)
        {
            health += item.healthValue;
            strengthStat += item.strengthValue;
            defenceStat += item.defenceValue;
        }
        maxHealth = health;*/

        rb = this.GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        if (inventory.Count < 3) 
        { 
            //[NOT IMPLEMENTED]enables option to equip which will call OnEquip
        }
    }



    void OnEquip(Item item)
    {
        health += item.healthValue;
        strengthStat += item.strengthValue;
        defenceStat += item.defenceValue;
    }

    void OnUnequip(Item item)
    {
        health -= item.healthValue;
        strengthStat -= item.strengthValue;
        defenceStat -= item.defenceValue;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void DealDamage(GameObject target)
    {
        var theTarget = target.GetComponent<Character>();
        if(theTarget != null)
        {
            float damageResult = this.strengthStat - theTarget.defenceStat;
            if (damageResult > 0)
            {
                theTarget.TakeDamage(damageResult);
            }
        }
    }


}
