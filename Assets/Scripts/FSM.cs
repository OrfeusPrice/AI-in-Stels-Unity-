using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class FSM 
{
    public State current_state { get; set; }

    private Dictionary<Type, State> states = 
        new Dictionary<Type, State>();

    public void  AddState(State state)
    {
        states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : State
    {
        var type = typeof(T);

        if (current_state != null && 
            current_state.GetType() == type) 
            return;

        if (states.TryGetValue(type, out var newState))
        {
            current_state?.Exit();
            current_state = newState;
            current_state.Enter();
        }
    }

    public void Update()
    {
        current_state.Update();
    }
}

