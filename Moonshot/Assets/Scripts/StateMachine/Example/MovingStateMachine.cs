public class MovingStateMachine : StateMachine
{
    void Start()
    {
        this.CurrentState = new MoveForwardState(this, this.gameObject);
    }

    void Update()
    {
        this.CurrentState.Update();
    }
}
