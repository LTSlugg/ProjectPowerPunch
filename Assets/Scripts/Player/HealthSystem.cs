using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    //Set Stats
    [Header("Default Stats")]
    [SerializeField] private int healthPointsMax = 100; //Increases OverTime Determines KO State
    [SerializeField] private int dazePointsMax = 12; //Decreases OverTime Determines Dazed State
    [SerializeField] public int blockMeterMax { get; private set; } //Increases when Blocking Successfully, Decreases when Hit, Determines PowerPunch Capability

    private int jabDamage = 20;
    private int crossDamage = 40;
    private int powerPunch = 75;
    private bool isDazed = false; //A State check


    //Current Stats
    [Header("Current Stats")]
    [SerializeField] private float currentHealthPoints = 0;
    [SerializeField] private float currentDazePoints = 0;
    [SerializeField] public float currentBlockMeter{ get; private set; }

    public UnityAction GotDazed;
    public UnityAction GotKilled;

    //Start Monobehavior Function
    private void Start()
    {
        currentHealthPoints = healthPointsMax; //Sets current HP to the Max HP
        blockMeterMax = 3;
        currentBlockMeter = 3;
    }


    //Logic Update Function
    private void Update()
    {
        EntityStatus();       
    }

    //Checks every frame to see the status of the Entity
    private void EntityStatus()
    {
        if (IsEntityDead()) { return; } //Breaks out the code if condition is met AKA this entity is Dead
        
        HealUpOverTime();
        isDazed = IsEntityDazed(); //Sets the Dazed boolean to the IsEntityDazed Function

        if (IsEntityDazed()) { return; } //Breaks out the code if condition is met
    }



    //Heals the Entity over Time;
    //TODO: Add timer to allow a pause on the healing when the entity is hit
    private void HealUpOverTime()
    {
        if(currentHealthPoints >= healthPointsMax && isDazed == false) 
        {
            currentHealthPoints = healthPointsMax; //Resets the Current Health Points to default incase overshot occured
            DecreaseDazeOverTime(); //Calls on the Decrease Daze Function since player is at MAX HP his daze meter decreases
        }
        else //Heal up Functionality
        {
            currentHealthPoints += .25f;
        }
    }



    //Decreases the Daze Meter over Time, only called when the player is at Full HP
    private void DecreaseDazeOverTime()
    {
        if (currentDazePoints > 0)
        {
            currentDazePoints -= .1f;
        }
    }



    //Checks to see if the Entity is Dazed and returns true or false and calls on UnityAction Events
    private bool IsEntityDazed()
    {
        if (currentDazePoints >= dazePointsMax && isDazed == false)
        {
            GotDazed(); //Calls out to the Listeners
            return true;
        }

        return false;
    }



    //Checks to see if the Entity has Died and returns a true or false value UnityAction Events
    private bool IsEntityDead()
    {
        if(currentHealthPoints <= 0)
        {
            GotKilled(); //Calls out to the Listeners
            return true; 
        }
        else
        {
            return false;
        }
    }
}
