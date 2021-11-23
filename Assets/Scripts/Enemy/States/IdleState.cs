using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class IdleState : States
{
    private D_IdleState stateData;  //Ref to the Data Scriptable Object
    private BossEntity bossEntity; //Ref to the main entity

    private float randomWaitTime;

    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animationName, D_IdleState stateData, BossEntity bossEntity) : base(entity, stateMachine, animationName)
    {
        this.stateData = stateData;
        this.bossEntity = bossEntity;

        this.randomWaitTime = Random.Range(stateData.MinWaitTime, stateData.MaxWaitTime);
    }

    public override void Enter()
    {
        base.Enter();
        bossEntity.transform.position = bossEntity.arrayPositions[0].position;
        Debug.Log("Entered Idle State");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + randomWaitTime)
        {
            Move();
            startTime = Time.time; //Resets the Start Time
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    //TODO: Add more Logic to this Function and allow transitions into other states
    private void Move()
    {
        bossEntity.transform.position = bossEntity.arrayPositions[Random.Range(0,4)].position;
        this.randomWaitTime = Random.Range(stateData.MinWaitTime, stateData.MaxWaitTime);
    }

}
