public abstract class State
{
    protected StateMachine _stateMachine;

    public State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void OnEnter() { }

    public abstract void Update();

    public virtual void OnExit() { }
}
