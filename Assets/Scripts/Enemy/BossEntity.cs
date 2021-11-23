using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntity : Entity
{
    //State Instances
    public IdleState idleState { get; private set; } //Make a instance of the idle state
    public AggressiveState aggroState { get; private set; }

    //Array for Movement Positions
    [SerializeField] public Transform[] arrayPositions;

    //State Data
    [SerializeField] public D_Entity entityData;
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_AggressiveState aggroStateData;


    //Extra Data
    private Vector3 currentPhysicalPos;
    public int currentPos = 0;



    public override void Start()
    {
        base.Start();

        //Instantiations
        idleState = new IdleState(this, stateMachine, "idle", idleStateData, this);
        aggroState = new AggressiveState(this, stateMachine, "aggro", aggroStateData, this);

        //Set the initial state
        stateMachine.Intialize(idleState);

        currentPhysicalPos = this.transform.position;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        TrackEnemyPosOnArray();
    }


    //Tracks and Sets the currentPosition Variable, used for the Combat System
    private void TrackEnemyPosOnArray()
    {
        if (currentPhysicalPos != this.transform.position) //Logic to prevent needless running of this switch case
        {
            switch (this.gameObject.transform.position)
            {
                case var value when value == arrayPositions[0].position: // that's the trick - NOTE: Amazing Trick that lets me somehow feed the switch case a variable instead of needing to use a constant?!?!?!
                    Debug.Log("In Position 0");
                    currentPhysicalPos = this.transform.position;
                    currentPos = 0;
                    break;

                case var value when value == arrayPositions[1].position: 
                    Debug.Log("In Position 1");
                    currentPhysicalPos = this.transform.position;
                    currentPos = 1;
                    break;
                
                case var value when value == arrayPositions[2].position: 
                    Debug.Log("In Position 2");
                    currentPhysicalPos = this.transform.position;
                    currentPos = 2;
                    break;
                
                case var value when value == arrayPositions[3].position: 
                    Debug.Log("In Position 3");
                    currentPhysicalPos = this.transform.position;
                    currentPos = 3;
                    break;
            }
        }
    }
}
