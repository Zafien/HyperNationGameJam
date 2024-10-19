using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


    public enum States
    {

    }

    [Serializable]
    public class StateMachine
    {
        private StateNode _current;
        [ShowInInspector] private Type _type;
        private Dictionary<Type, StateNode> _nodes = new();
        private HashSet<ITransition> _anyTransitions = new();

        public string CurrentState { get { return _type.Name; } }
        public Type Type { get { return _type; } }
        public bool IsNewState = true;

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.To);
            }

            _current.State?.Update();
        }

        public void FixedUpdate() 
        {
            _current.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            _type = state.GetType();
            _current = _nodes[_type];
            _current.State.OnEnter();

            Debug.Log($"Changing State: {_type.Name}");
        }

        private void ChangeState(IState state)
        {
            IsNewState = state != _current.State;

            if (!IsNewState)
            {
                return;
            }

  
            var previousState = _current.State;
            var nextState = _nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();

            _type = state.GetType();
            _current = _nodes[_type];

            Debug.Log($"Changing State: {_type.Name}");
        }

        private ITransition GetTransition()
        {
            foreach(var transition in _anyTransitions)
            {
                if(transition.Condition.Evaluate())
                {
                    return transition;
                }
            }

            foreach(var transition in _current.Transitions)
            {
                if(transition.Condition.Evaluate())
                {
                    return transition;
                }
            }

            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        private StateNode GetOrAddNode(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());

            if(node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
            }

            return node;
        }

        private class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }

    }
