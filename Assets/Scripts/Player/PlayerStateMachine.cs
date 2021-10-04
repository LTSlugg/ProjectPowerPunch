using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    #region The Players State Machine
    //Communicates with the ANimator and allows the proper Animation and Logic to run corresponding with the current Players State
    //This depends entirely upon the Input from the Player Controller


    private Animator _animator;

    public enum ActionStates
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
    public ActionStates currentState;
    public ActionStates previousState;

    public void Start()
    {
        _animator = GetComponent<Animator>();

        currentState = PlayerStateMachine.ActionStates.Idle; //Sets default state
        previousState = currentState; //Sets the previous state to this current state
    }

    void Update()
    {
        StateHandler();
        ResetPreviousStateAnimation();
    }

    //Handles telling Animator when to play certain animations depending on the current state the player is in.
    public void StateHandler()
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
                CallResetToIdleRoutine();
                break;

            case ActionStates.AttackCross:
                _animator.SetTrigger("Cross");
                CallResetToIdleRoutine();
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
    public void ResetStateToIdle()
    {
        _animator.ResetTrigger(FindStateTriggerName(currentState));
        currentState = ActionStates.Idle;
    }


    //Timed Reset of the current State to Default
    public bool isResetCalled = false; //Tracks when the Reset function is called
    public bool isDoingReset = false; //Tracks if the Coroutine is doing the reset
    private IEnumerator TimedResetToIdle()
    {
        while (isDoingReset)
        {
            yield return new WaitForSeconds(GetComponent<PlayerController>().attackSpeedRate); //wait the attack speed rate before resetting to idle animation
            ResetStateToIdle();
            isDoingReset = false; //Turns off its own infinite loop with this boolean
        }
    }
    //A Logic Function that calls the Coroutine to reset the current state to Idle after a set time
    private void CallResetToIdleRoutine()
    {
        if (isResetCalled && !isDoingReset)
        {
            isDoingReset = true;
            StartCoroutine("TimedResetToIdle");
            isResetCalled = false;
        }
    }



    //Handles reseting the current trigger state the player is in
    public void ResetPreviousStateAnimation()
    {
        if (currentState != previousState)
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
