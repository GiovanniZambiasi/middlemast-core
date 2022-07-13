using System.Collections.Generic;
using UnityEngine;

namespace MiddleMast
{
    public interface IState
    {
        void Enter();

        void Exit();

        void Tick(float deltaTime);
    }

    public class StateMachine<TState>
        where TState : IState
    {
        public delegate void StateChangeHandler(TState previous, TState current);

        public event StateChangeHandler OnStateChanged;

        private List<TState> _states;
        private TState _state;

        public StateMachine(List<TState> states)
        {
            _states = states;
        }

        public TState State => _state;

        public void SetState<T>()
        {
            T state = GetState<T>();

            if (state == null)
            {
                Debug.LogError($"State '{typeof(T).Name}' not found");

                return;
            }

            if (state is TState casted)
            {
                SetState(casted);
            }
            else
            {
                Debug.LogError($"State '{typeof(T).Name}' is incompatible with state machine of '{typeof(TState).Name}'");
            }
        }

        public void SetState(TState state)
        {
            if (_state != null && _state.Equals(state))
            {
                return;
            }

            try
            {
                TState previous = _state;

                _state?.Exit();

                _state = state;

                _state?.Enter();

                OnStateChanged?.Invoke(previous, _state);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to set state of '{typeof(TState).Name}' to '{state.GetType().Name}'. Reason:\n\n{e}");
            }
        }

        public T GetState<T>()
        {
            for (int i = 0; i < _states.Count; i++)
            {
                TState state = _states[i];

                if (state is T castedState)
                {
                    return castedState;
                }
            }

            return default;
        }

        public void Tick(float deltaTime)
        {
            _state?.Tick(deltaTime);
        }
    }
}
