using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntity : Entity
{
    //State Instances
    public IdleState idleState { get; private set; } //Make a instance of the idle state
    public AggressiveState aggroState { get; private set; }

    //Stata
    [SerializeField] public Transform[] arrayPositions;

    [SerializeField] public D_Entity entityData;
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_AggressiveState aggroStateData;

    public override void Start()
    {
        base.Start();

        //Instantiations
        idleState = new IdleState(this, stateMachine, "idle", idleStateData, this);
        aggroState = new AggressiveState(this, stateMachine, "aggro", aggroStateData, this);

        //Set the initial state
        stateMachine.Intialize(idleState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}
