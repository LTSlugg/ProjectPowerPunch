using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    /* TODO: Finish This, Graph This System out
     * Handles all the combat output and input for this Entity.
     * On Entry will locate and 'Sync' with the other Entity.CombatSystem on the field
     * Will determine if a player or NPC attack 'connects' by determining the Entity position on the playfield
     * Will Communicate with Local Health System and send signals to the other Entity.CombatSystem to help that Entity determine its local states
     */

    public int playerPos { get; private set; }
    public int enemyPos { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
