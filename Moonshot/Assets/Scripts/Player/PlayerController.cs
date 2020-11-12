using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StateMachine
{
    public bool GotTimeStar { get; set; }

    void Start()
    {
        //this.CurrentState = new IdleState(this, this.gameObject);
    }

    void Update()
    {
        this.CurrentState.Update();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "TimeStar")
        {
            GotTimeStar = true;
            Destroy(col.gameObject);
        }
    }
}
    
