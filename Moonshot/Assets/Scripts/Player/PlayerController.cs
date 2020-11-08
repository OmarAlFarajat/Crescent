using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StateMachine
{

    void Start()
    {
        //this.CurrentState = new IdleState(this, this.gameObject);
    }

    void Update()
    {
        this.CurrentState.Update();
    }
}
