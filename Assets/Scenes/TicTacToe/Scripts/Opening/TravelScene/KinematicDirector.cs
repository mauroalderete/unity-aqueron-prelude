using Opening.TravelScene;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening.TravelScene
{
    public class KinematicDirector : MonoBehaviour
    {
        [SerializeField] GameObject boris;
        [SerializeField] GameObject fade;
        
        
        BorisActor borisActor;

        FadeActor fadeActor;
        Animator fadeAnimator;

        public event EventHandler SceneEnded;

        private void Awake()
        {
            borisActor = boris.GetComponent<BorisActor>();
            borisActor.Outted += BorisActor_Outted;

            fadeActor = fade.GetComponent<FadeActor>();
            fadeAnimator = fade.GetComponent<Animator>();

            fadeActor.FadeTransitionClimaxStarted += FadeActor_FadeTransitionClimaxStarted;
        }

        private void FadeActor_FadeTransitionClimaxStarted(object sender, EventArgs e)
        {
            if(SceneEnded == null ) { return; }
            SceneEnded(this, EventArgs.Empty);
        }

        private void BorisActor_Outted(object sender, System.EventArgs e)
        {
            fadeAnimator.SetInteger(FadeActor.FADE_PARAM, FadeActor.FADE_TRANSITION);
        }
    }
}
