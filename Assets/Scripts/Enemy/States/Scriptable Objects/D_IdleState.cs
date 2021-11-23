using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="newIdleState",menuName = "Data/State Data/Idle State")]
public class D_IdleState : ScriptableObject
{
    public float MinWaitTime = 0.5f;
    public float MaxWaitTime = 1.5f;


}
