using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    #region The Players State Machine
    //Communicates with the Animator and allows the proper Animation and Logic to run corresponding with the current Players State
    //This depends entirely upon the Input from the Player Controller


    private Animator _animator;

    public enum ActionStates
    {
        Idle,
        Blocking,
        AttackJab,
        AttackCross,
        Hurt,
        Dazed,
        KnockedOut,
        PowerPunch,
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

            case ActionStates.Hurt:
                _animator.SetTrigger("Hurt");
                CallResetToIdleRoutine();
                break;

            case ActionStates.Dazed:
                _animator.SetTrigger("Dazed");
                break;

            case ActionStates.KnockedOut:
                _animator.SetTrigger("KnockedOut");
                break;

            case ActionStates.PowerPunch:
                _animator.SetTrigger("PowerPunch");
                CallResetToIdleRoutine();
                break;
        }
    }


    //Resets the current State to the Idle State when Called
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

            case ActionStates.Hurt:
                return "Hurt";

            case ActionStates.Dazed:
                return "Dazed";

            case ActionStates.KnockedOut:
                return "KnockedOut";

            case ActionStates.PowerPunch:
                Debug.Log("Returning PP Trigger Name for Reset");
                return "PowerPunch";
        }

        Debug.LogError("Returning the current State is Broken");
        return null;
    }
    #endregion
}
