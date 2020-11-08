using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State _currentState;

    public State CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState != null)
                _currentState.OnExit();

            _currentState = value;

            if (_currentState != null)
                _currentState.OnEnter();
        }
    }
}
