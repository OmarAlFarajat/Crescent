using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackwardState : State
{
    private GameObject _target;

    public MoveBackwardState(StateMachine stateMachine, GameObject target) : base(stateMachine)
    {
        _target = target;
    }

    public override void OnEnter()
    {
        Debug.Log("Just entered the MoveBackwardsState");
    }


    public override void Update()
    {
        _target.transform.position -= new Vector3(0, 0, 5.0f) * Time.deltaTime;

        if (Input.anyKeyDown)
        {
            _stateMachine.CurrentState = new MoveForwardState(_stateMachine, _target);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Just exited the MoveBackwardsState");
    }
}
