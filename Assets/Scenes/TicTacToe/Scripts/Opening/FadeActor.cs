using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening
{
    public class FadeActor : MonoBehaviour
    {
        public event EventHandler FadeInEnded;
        public event EventHandler FadeOutEnded;
        public event EventHandler FadeTransitionClimaxStarted;
        public event EventHandler FadeTransitionEnded;

        Animator animator;

        public const string FADE_PARAM = "Anim";
        public const int FADE_IDLE = 0;
        public const int FADE_IN = 1;
        public const int FADE_TRANSITION = 2;
        public const int FADE_OUT = 3;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void OnFadeInEnded()
        {
            animator.SetInteger(FADE_PARAM, FADE_IDLE);

            if (FadeInEnded == null) { return; }
            FadeInEnded(this, EventArgs.Empty);
        }

        void OnFadeOutEnded()
        {
            animator.SetInteger(FADE_PARAM, FADE_IDLE);

            if (FadeOutEnded == null) { return; }
            FadeOutEnded(this, EventArgs.Empty);
        }

        void OnFadeTransitionClimaxStarted()
        {
            if (FadeTransitionClimaxStarted == null) { return; }
            FadeTransitionClimaxStarted(this, EventArgs.Empty);
        }

        void OnFadeTransitionEnded()
        {
            animator.SetInteger(FADE_PARAM, FADE_IDLE);

            if (FadeTransitionEnded == null) { return; }
            FadeTransitionEnded(this, EventArgs.Empty);
        }
    }

}
