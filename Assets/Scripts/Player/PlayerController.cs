using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TODO: ADD FULL FUNCTIONALITY FOR ATTACKING, ADD HEALTH BAR, DAZE METER, BLOCK METER
 * Handles User Input 
 * Communicates with Animator
 * Communicates with Other Game Systems
 * Communicates with Player Game Systems (HP, Daze Meter, Block Meter)
 */


public class PlayerController : MonoBehaviour
{
    //Player Data
    [Header("Player Data")]
    [SerializeField] private Transform[] transformPositionArray;
    [SerializeField] private int currentPlayerPosition;
    [SerializeField] public float attackSpeedRate = .15f;



    //Game Object Components / Scripts
    Animator _animator;
    Rigidbody2D _rgbd2;
    HealthSystem _healthSys; //The Players Health System
    
    [Header("Player State Machine Script Reference")]
    [SerializeField]PlayerStateMachine _playerStateMachine;


    private void OnEnable()
    {
        _healthSys =  GetComponentInChildren<HealthSystem>();
        _healthSys.GotDazed += PlayerGotDazed;
        _healthSys.GotKilled += PlayerGotKilled;
    }

    private void OnDisable()
    {
        _healthSys.GotDazed -= PlayerGotDazed;
        _healthSys.GotKilled -= PlayerGotKilled;
    }

    //*******************************************************************************************************************
    #region Unity Monobehavior Functions
    //Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rgbd2 = GetComponentInChildren<Rigidbody2D>();

        attackSpeedRate = .15f;
        currentAttackCoolDown = attackSpeedRate; //Sets the current cooldown to the attackspeed rate
    }

    //Logic Update is called once per frame
    void Update()
    {
        TrackPlayerAttackLogicFunction();
        PlayerAttackFunction();
        MoveDirection();
    }

    #endregion



    //*************************************************************************************************************
    #region Player Movement/Input
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
                if(_playerStateMachine.currentState == PlayerStateMachine.ActionStates.Blocking) { _playerStateMachine.ResetStateToIdle(); } //Will reset to Idle if no input is in and the current State is Blocking to leave block state
            }

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
                _playerStateMachine.currentState = PlayerStateMachine.ActionStates.Blocking;
                break;

                //TODO: ADD and NOW FIXXXXXX POWERPUNCH LOGIC
            case -1:
                transform.position = transformPositionArray[0].position;
                this.currentPlayerPosition = 0;
                
                if (_healthSys.currentBlockMeter >= _healthSys.blockMeterMax && _playerStateMachine.currentState != PlayerStateMachine.ActionStates.PowerPunch) //Checks to see if the current Meter is at the maximum capacity before entering power punch statew
                {
                    _playerStateMachine.currentState = PlayerStateMachine.ActionStates.PowerPunch;
                }
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
                if (Input.GetKeyDown(KeyCode.Z) && _playerStateMachine.currentState != PlayerStateMachine.ActionStates.Blocking && !_playerStateMachine.isDoingReset)
                {
                    _playerStateMachine.currentState = PlayerStateMachine.ActionStates.AttackJab;
                    currentAttackCoolDown = 0f;
                    _playerStateMachine.isResetCalled = true;
                }
                else if (Input.GetKeyDown(KeyCode.C) && _playerStateMachine.currentState != PlayerStateMachine.ActionStates.Blocking && !_playerStateMachine.isDoingReset)
                {
                    _playerStateMachine.currentState = PlayerStateMachine.ActionStates.AttackCross;
                    currentAttackCoolDown = 0f;
                    _playerStateMachine.isResetCalled = true;
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

    #region Player Logic and Reasoning Section
    //Listeners: Change the current state and run algorithms when called
    private void PlayerGotDazed() => _playerStateMachine.currentState = PlayerStateMachine.ActionStates.Dazed;
  
    private void PlayerGotKilled() => _playerStateMachine.currentState = PlayerStateMachine.ActionStates.KnockedOut;
    #endregion



    //**************************************************************************************************************************
}


