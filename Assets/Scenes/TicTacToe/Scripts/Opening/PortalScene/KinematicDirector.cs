using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening.PortalScene
{
    public class KinematicDirector : MonoBehaviour
    {
        [SerializeField] GameObject fade;
        [SerializeField] GameObject background;
        [SerializeField] GameObject portal;
        [SerializeField] GameObject borix;
        [SerializeField] GameObject gizor;
        [SerializeField] GameObject xirax;

        Animator fadeAnimator;
        FadeActor fadeActor;

        BackgroundActor backgroundScript;

        PortalActor portalDirector;
        Animator portalAnimator;

        Animator borixAnimator;
        Animator gizorAnimator;
        Animator xiraxAnimator;
        SparkActor borixActor;
        SparkActor gizorActor;
        SparkActor xiraxActor;

        public event EventHandler SceneEnded;

        private void Awake()
        {
            backgroundScript = background.GetComponent<BackgroundActor>();
            backgroundScript.Arrived += BackgroundScript_Arrived;

            portalAnimator = portal.GetComponent<Animator>();
            portalDirector = portal.GetComponent<PortalActor>();
            portalDirector.Traversed += PortalDirector_Traversed;

            borixAnimator = borix.GetComponent<Animator>();
            gizorAnimator = gizor.GetComponent<Animator>();
            xiraxAnimator = xirax.GetComponent<Animator>();

            borixActor = borix.GetComponent<SparkActor>();
            gizorActor = gizor.GetComponent<SparkActor>();
            xiraxActor = xirax.GetComponent<SparkActor>();

            borixActor.Blinked += BorixDirector_Blinked;
            gizorActor.Blinked += GizorDirector_Blinked;
            xiraxActor.Blinked += XiraxDirector_Blinked;

            fadeActor = fade.GetComponent<FadeActor>();
            fadeAnimator = fade.GetComponent<Animator>();

            fadeActor.FadeTransitionClimaxStarted += FadeDirector_FadeTransitionClimaxStarted;

            fade.SetActive(true);
        }

        private void Start()
        {
            fadeAnimator.SetInteger(FadeActor.FADE_PARAM, FadeActor.FADE_IN);
        }

        private void BackgroundScript_Arrived(object sender, System.EventArgs e)
        {
            portalAnimator.SetBool("Start", true);
        }

        private void PortalDirector_Traversed(object sender, System.EventArgs e)
        {
            borixAnimator.SetBool("Start", true);
        }

        private void BorixDirector_Blinked(object sender, System.EventArgs e)
        {
            gizorAnimator.SetBool("Start", true);
        }
        private void GizorDirector_Blinked(object sender, System.EventArgs e)
        {
            xiraxAnimator.SetBool("Start", true);
        }

        private void XiraxDirector_Blinked(object sender, System.EventArgs e)
        {
            // TODO: pass next cinematic escene
            fadeAnimator.SetInteger(FadeActor.FADE_PARAM, FadeActor.FADE_TRANSITION);
        }

        private void FadeDirector_FadeTransitionClimaxStarted(object sender, System.EventArgs e)
        {
            if(SceneEnded == null) { return; }
            SceneEnded(this, EventArgs.Empty);
        }
    }

}
