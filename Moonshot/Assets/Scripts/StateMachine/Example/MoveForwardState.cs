using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardState : State
{
    private GameObject _target;

    public MoveForwardState(StateMachine stateMachine, GameObject target) : base(stateMachine)
    {
        _target = target;
    }

    public override void OnEnter()
    {
        Debug.Log("Just entered the MoveForwardState!");
    }

    public override void Update()
    {
        _target.transform.position += new Vector3(0, 0, 5.0f) * Time.deltaTime;

        if (Input.anyKeyDown)
        {
            _stateMachine.CurrentState = new MoveBackwardState(_stateMachine, _target);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Just exited the MoveForwardState!");
    }
}
