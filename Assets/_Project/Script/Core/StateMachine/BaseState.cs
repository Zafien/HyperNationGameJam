using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NF.Main.Gameplay
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerController _player;
        protected readonly Animator _animator;

        protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int WalkHash = Animator.StringToHash("Walk");
        protected static readonly int RunHash = Animator.StringToHash("Run");
        protected static readonly int JumpHash = Animator.StringToHash("JumpStart");
        protected static readonly int DashHash = Animator.StringToHash("Dash");
        protected static readonly int AttackHash = Animator.StringToHash("AttackInteract");
        protected static readonly int NormalInteractHash = Animator.StringToHash("NormalInteract");
        protected static readonly int IdleDanceHash = Animator.StringToHash("IdleDance");

        protected const float _crossFadeDuration = 0.2f;

        protected BaseState(PlayerController player/*, Animator animator*/)
        {
            this._player = player;
            //this._animator = animator;
        }


        public virtual void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}

