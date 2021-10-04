using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TODO: A FEW ITEMS ON LIST NEED TO BE COMPLETED, MISSING FEATURES SUCH AS ATTACKING ANIMATIONS, RESETS, DASHING ANIMATION, ETC
 * TODO: ADD FULL FUNCTIONALITY FOR ATTACKING, ADD HEALTH BAR, DAZE METER, BLOCK METER
 * Handles User Input 
 * Communicates with Animator
 * Communicates with Other Game Systems
 * Communicates with Player Game Systems (HP, Daze Meter, Block Meter)
 */


public class PlayerController : MonoBehaviour
{
    //Player Data
    [SerializeField] private Transform[] transformPositionArray;
    [SerializeField] private int currentPlayerPosition;
    [SerializeField] private float attackSpeedRate = .15f;


    //Game Object Components
    Animator _animator;
    Rigidbody2D _rgbd2;



    //*******************************************************************************************************************
    #region Unity Monobehavior Functions
    //Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rgbd2 = GetComponent<Rigidbody2D>();

        currentState = ActionStates.Idle; //Sets default state
        previousState = currentState; //Sets the previous state to this current state
        currentAttackCoolDown = attackSpeedRate; //Sets the current cooldown to the attackspeed rate
    }

    //Logic Update is called once per frame
    void Update()
    {
        StateHandler();
        ResetPreviousStateAnimation();
        TrackPlayerAttackLogicFunction();
        PlayerAttackFunction();
        MoveDirection();
    }

    //Physics Update
    void FixedUpdate()
    {
    }

    #endregion



    //*************************************************************************************************************
    #region Player Movement
    /*WASD Control Scheme Two Buttons for attack***************
    *   
    * --2--  |   --W--       <---Array Diagram 
    * 3-0-1  |   A-S-D       <---Control Scheme 
    * 
    * Z - Jab
    * C - Cross
    * **************************
    */
    private void MoveDirection()
    {
        //Reset to 0 on no Input
        if(Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) 
            { 
                this.transform.position = transformPositionArray[0].position;
                this.currentPlayerPosition = 0;
                if(currentState == ActionStates.Blocking) { ResetStateToIdle(); } //Will reset to Idle if no input is in and the current State is Blocking to leave block state
            }

        //TODO: ADD DASH ANIMATION EFFECT
        //Horizontal Input
        switch (Input.GetAxisRaw("Horizontal"))
        {
            case 1: //Right
                if (currentPlayerPosition == 2) { break; }; //Breaks out if the player is at Position 2 (Up)
                transform.position = transformPositionArray[1].position;
                this.currentPlayerPosition = 1;
                break;

            case -1: //Left
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

                //TODO: ADD POWERPUNCH LOGIC
            case -1:
                transform.position = transformPositionArray[0].position;
                this.currentPlayerPosition = 0;
                break;
        }
    }


    //Tracks the current attack and adds a cooldown to prevent spammage
    private float currentAttackCoolDown;
    [SerializeField]private bool canAttack = true;
    private void PlayerAttackFunction()
    {
        switch (canAttack)
        {
            case true:
                if (Input.GetKeyDown(KeyCode.Z) && currentState != ActionStates.Blocking && !isDoingReset)
                {
                    currentState = ActionStates.AttackJab;
                    currentAttackCoolDown = 0f;
                    isResetCalled = true;
                }
                else if (Input.GetKeyDown(KeyCode.C) && currentState != ActionStates.Blocking && !isResetCalled)
                {
                    currentState = ActionStates.AttackCross;
                    currentAttackCoolDown = 0f;
                    isResetCalled = true;
                }
                break;

            case false:
                break;
        }
    }



    //Tracks the players ability to Attack using Logic to determine is he is able too or not
    private void TrackPlayerAttackLogicFunction()
    {
        if (currentAttackCoolDown >= attackSpeedRate) 
        { 
            canAttack = true; 
        }
        else  
        {
            currentAttackCoolDown += .005f; 
            canAttack = false; 
        }
    }
    #endregion


    //**************************************************************************************************************************
    #region The Players State Machine


    private enum ActionStates
    {
        Idle,
        Blocking,
        AttackJab,
        AttackCross,
        GotHit,
        Moving,
        Hurt,
        Dazed,
        KnockedOut,
    }
    [SerializeField] private ActionStates currentState;
    private ActionStates previousState;
    //Handles telling Animator when to play certain animations depending on the current state the player is in.
    private void StateHandler()
    {
        switch (this.currentState)
        {
            case ActionStates.Idle:
                _animator.SetTrigger("Idle");
                break;

            case ActionStates.Blocking:
                _animator.SetTrigger("Block");
                break;

            case ActionStates.AttackJab:
                _animator.SetTrigger("Jab");
                CallResetToIdle();
                break;

            case ActionStates.AttackCross:
                _animator.SetTrigger("Cross");
                CallResetToIdle();
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


    //Resets the current State
    private void ResetStateToIdle()
    {
        _animator.ResetTrigger(FindStateTriggerName(currentState));
        currentState = ActionStates.Idle;
    }


    //Timed Reset of the current State to Default
    private bool isResetCalled = false; //Tracks when the Reset function is called
    private bool isDoingReset = false; //Tracks if the Coroutine is doing the reset
    private IEnumerator TimedResetToIdle()
    {
        while (isDoingReset)
        {
            yield return new WaitForSeconds(attackSpeedRate); //wait the attack speed rate before resetting to idle animation
            ResetStateToIdle();
            isDoingReset = false; //Turns off its own infinite loop with this boolean
        }
    }
    //A Logic Function that calls the Coroutine to reset the current state to Idle after a set time
    private void CallResetToIdle()
    {
        if (isResetCalled && !isDoingReset)
        {
            isDoingReset = true;
            StartCoroutine("TimedResetToIdle");
            isResetCalled = false;
        }
    }



    //Handles reseting the current trigger state the player is in
    private void ResetPreviousStateAnimation()
    {
        if(currentState != previousState) 
        {
            _animator.ResetTrigger(FindStateTriggerName(previousState));
            previousState = currentState;
        }
    }


    //Returns the current state trigger name that is parsed through
    private string FindStateTriggerName(ActionStates StateToTrack)
    {
        switch (StateToTrack)
        {
            case ActionStates.Idle:
                return "Idle";

            case ActionStates.Blocking:
                return "Block";

            case ActionStates.AttackJab:
                return "Jab";

            case ActionStates.AttackCross:
                return "Cross";

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

        Debug.LogError("Returning the current State is Broken");
        return null;
    }
    #endregion

}
