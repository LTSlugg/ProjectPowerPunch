using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Entity class handles most of the base function any Enemy will do, like Set velocity, set movement direction, face and follow player. ETC....
 */

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public int facingDirection { get; private set; }
    public Animator _animator { get; private set; }
    private SpriteRenderer _renderer { get; set; }


    private Vector2 velocityWorkspace; //Handles the Movement Speed and Direction in this 'workspace'
    private float moveDirectionY = 1f; //Random Y Axis MoveDirection



    //public D_Entity entityData; //TODO: Ref to the Data File

    //Start function monobehaviour
    public virtual void Start() 
    {
        stateMachine = new FiniteStateMachine(); //new instance of the statemachine
        _animator = transform.gameObject.GetComponent<Animator>();
        _renderer = transform.gameObject.GetComponent<SpriteRenderer>();
    }



    //pulls the statemachines current state logic update up
    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }


    //Pulls the statemachines current state physic update function
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }


}