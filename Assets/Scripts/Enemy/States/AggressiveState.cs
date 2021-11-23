using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveState : States
{
    private D_AggressiveState stateData; //Ref to the Data Scriptable Object
    private BossEntity bossEntity;

    public AggressiveState(Entity entity, FiniteStateMachine stateMachine, string animationName, D_AggressiveState stateData, BossEntity bossEntity) : base(entity, stateMachine, animationName)
    {
        this.stateData = stateData;
        this.bossEntity = bossEntity;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
