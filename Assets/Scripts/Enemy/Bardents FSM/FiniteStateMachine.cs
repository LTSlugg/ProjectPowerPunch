using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    /*
     * Handles the current state the entity is in via initialization going into the Enter method of the current state, then allowing to change states
     * when called to enter a new state.
     */
    public States currentState { get; private set; }

    public void Intialize(States startingState) //Gets parsed through the starting state
    {
        currentState = startingState; //Sets the starting state
        currentState.Enter(); //Starts the enter function in current state
    }

    public void ChangeState(States newState) //Gets parsed through the new state
    {
        currentState.Exit(); //Starts the Exit function in current state
        currentState = newState; //Sets new state
        currentState.Enter(); //Starts the Enter Function in current State
    }

    //Called to stop the current Animation and Unsubscribe any Subscribers from event handlers
    public void ForceExitState()
    {
        currentState.Exit();
    }
}
