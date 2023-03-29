using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening.FallScene
{
    public class KinematicDirector : MonoBehaviour
    {
        [SerializeField] private GameObject fade;
        [SerializeField] private GameObject desert;
        [SerializeField] private GameObject camping;
        [SerializeField] private GameObject forest;

        private FallActor desertActor;
        private FallActor campingActor;
        private FallActor forestActor;

        private Animator fadeAnimator;
        private FadeActor fadeActor;

        public event EventHandler SceneEnded;

        private void Awake()
        {
            desertActor = desert.GetComponent<FallActor>();
            campingActor = camping.GetComponent<FallActor>();
            forestActor = forest.GetComponent<FallActor>();

            desertActor.Falled += DesertActor_Falled;
            campingActor.Falled += CampingActor_Falled;
            forestActor.Falled += ForestActor_Falled;

            fadeAnimator = fade.GetComponent<Animator>();
            fadeActor = fade.GetComponent<FadeActor>();

            fadeActor.FadeTransitionClimaxStarted += FadeActor_FadeTransitionClimaxStarted;
        }

        private void DesertActor_Falled(object sender, System.EventArgs e)
        {
            desert.SetActive(false);
            camping.SetActive(true);
        }
        private void CampingActor_Falled(object sender, System.EventArgs e)
        {
            camping.SetActive(false);
            forest.SetActive(true);
        }

        private void ForestActor_Falled(object sender, System.EventArgs e)
        {
            forest.SetActive(false);
            fadeAnimator.SetInteger(FadeActor.FADE_PARAM, FadeActor.FADE_TRANSITION);
        }

        private void FadeActor_FadeTransitionClimaxStarted(object sender, System.EventArgs e)
        {
            if (SceneEnded == null) { return; }
            SceneEnded(this, EventArgs.Empty);
        }
    }
}
