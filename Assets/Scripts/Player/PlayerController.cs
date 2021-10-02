using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TODO: This.Now
 * Handles User Input 
 * Communicates with Animator
 * Communicates with Other Game Systems
 * Communicates with Player Game Systems (HP, Daze Meter, Block Meter)
 */


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform[] transformPositionArray;
    [SerializeField] private int currentPlayerPosition;

    Animator _animator;
    Rigidbody2D _rgbd2;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rgbd2 = GetComponent<Rigidbody2D>();

        currentState = ActionStates.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        StateHandler();
    }

    void FixedUpdate()
    {
        TrackMoveDirection();
    }


    /*TODO: WASD Control Scheme two Buttons for attack***************
    *   
    * --2--  |   --W--       <---Array Diagram 
    * 3-0-1  |   A-S-D       <---Control Scheme 
    * 
    * 
    * **************************
    */

    private void TrackMoveDirection()
    {
        //Reset to 0 on no Input
        if(Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) 
            { 
                this.transform.position = transformPositionArray[0].position;
                this.currentPlayerPosition = 0;

                this.currentState = ActionStates.Idle;
            }

        //Horizontal Input
        switch (Input.GetAxisRaw("Horizontal"))
        {
            case 1:
                if (currentPlayerPosition == 2) { break; }; //Breaks out if the player is at Position 2 (Up)
                transform.position = transformPositionArray[1].position;
                this.currentPlayerPosition = 1;
                break;

            case -1:
                if (currentPlayerPosition == 2) { break; }; //Breaks out if the player is at Position 2 (Up)
                transform.position = transformPositionArray[3].position;
                this.currentPlayerPosition = 3;
                break;
        }

        //Vertical Input
        switch (Input.GetAxisRaw("Vertical"))
        {
            case 1:
                if (currentPlayerPosition == 1 || currentPlayerPosition == 3) { break; };
                transform.position = transformPositionArray[2].position;
                this.currentPlayerPosition = 2;
                this.currentState = ActionStates.Blocking;
                break;

            case -1:
                transform.position = transformPositionArray[0].position;
                this.currentPlayerPosition = 0;
                break;
        }
    }


    //The Players State Machine
    private enum ActionStates
    {
        Idle,
        Blocking,
        Attacking,
        GotHit,
        Moving,
        Hurt,
        Dazed,
        KnockedOut,
    }
    private ActionStates currentState = ActionStates.Idle;

    //Handles telling Animator when to play certain animations depending on the current state the player is in.
    private void StateHandler()
    {
        switch (this.currentState)
        {
            case ActionStates.Idle:
                _animator.SetBool("Idle", true);
                _animator.SetBool("Block", false);
                break;

            case ActionStates.Blocking:
                _animator.SetBool("Idle", false);
                _animator.SetBool("Block", true);
                break;

            case ActionStates.Attacking:
                break;
            
            case ActionStates.GotHit:
                break;
            
            case ActionStates.Moving:
                break;
            
            case ActionStates.Hurt:
                break;
            
            case ActionStates.Dazed:
                break;
            
            case ActionStates.KnockedOut:
                break;
        }
    }
}
