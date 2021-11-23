using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States
{
    /*
     * Handles the Animator through the entity to set the Animator Bool to False (Play and Stop Animations)
     * Tracks when the state has started, great tool for wait time checks within the state
     */


    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    protected float startTime; //Tracks when the state starts

    protected string animationName;


     //Constructor modifiers to intialize when instanced
    public States(Entity entity, FiniteStateMachine stateMachine, string animationName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animationName = animationName;
    }

    //Do Something when Entering the State
    public virtual void Enter() {
        startTime = Time.time; //When the state has started
        entity._animator.SetBool(animationName, true); //Sets the Animation to Play when entering the state
    }

    //Do Something when Exiting the State
    public virtual void Exit() {

        entity._animator.SetBool(animationName, false); //Sets the Animation to Play when entering the state
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate(){

    }
}
