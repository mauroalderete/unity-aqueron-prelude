using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening.MisionScene
{
    public class KinematicDirector : MonoBehaviour
    {
        [SerializeField] GameObject fade;
        [SerializeField] GameObject dialog;
        [SerializeField] GameObject news;
        [SerializeField] GameObject daily;
        [SerializeField] GameObject secret;
        [SerializeField] GameObject powerupSection;
        [SerializeField] GameObject monsterSection;


        NewsActor newsActor;
        DailyActor dailyActor;
        SecretActor secretActor;
        DialogWindowActor dialogActor;
        FadeActor fadeActor;

        Animator fadeAnimator;

        public event EventHandler SceneEnded;

        enum State
        {
            None,
            News,
            NewsDialogue1,
            NewsDialogue2,
            Daily,
            DailyDialogue,
            Secret,
            SecretDialogue1,
            SecretDialogue2,
            SecretDialogue3,
            SecretDialogue4,
            End
        }

        State currentState;

        private class Dialogue
        {
            public string Message;
            public float ExtraReadingTime;
        }

        Dictionary<SystemLanguage, Dictionary<State, Dialogue>> dialogueLines;

        private void Awake()
        {
            newsActor = news.GetComponent<NewsActor>();
            dailyActor = daily.GetComponent<DailyActor>();
            secretActor = secret.GetComponent<SecretActor>();
            fadeActor = fade.GetComponent<FadeActor>();

            fadeAnimator = fade.GetComponent<Animator>();
            fadeActor.FadeOutEnded += TransitionNextState;

            newsActor.Showed += TransitionNextState;
            dailyActor.Showed += TransitionNextState;
            secretActor.Showed += TransitionNextState;

            dialogActor = dialog.GetComponent<DialogWindowActor>();

            dialogActor.Hidden += TransitionNextState;

            currentState = State.None;

            Dictionary<State, Dialogue> spanishDialogues = new Dictionary<State, Dialogue>
            {
                { State.NewsDialogue1, new Dialogue { Message = "\"¡criaturas del espacio desafian a la humanidad a un combate de videojuegos\"" } },
                { State.NewsDialogue2, new Dialogue { Message = "\"Los expertos no se ponen de acuerdo sobre las verdaderas intenciones que ocultan las criaturas\"" } },
                { State.DailyDialogue, new Dialogue { Message = "\"La ONU y los estudios de videojuegos estan reuniendo fuerzas para seleccionar a los mejores jugadores del mundo\"" } },
                { State.SecretDialogue1, new Dialogue { Message = "¡Felicitaciones!\nFuiste seleccionado para representar a la humanidad." } },
                { State.SecretDialogue2, new Dialogue { Message = "Tu misión será recolectar toda la información acerca las criaturas. ¿Quiénes son? ¿Qué buscan? ¿De dónde vienen?", ExtraReadingTime = 1 } },
                { State.SecretDialogue3, new Dialogue { Message = "No podemos intervenir en los juegos. Pero nuestros expertos diseñaron algunos exploits que te serán muy utiles", ExtraReadingTime = 1 } },
                { State.SecretDialogue4, new Dialogue { Message = "Juega, gana y descubre cual es la verdadera historia detras.\nTienes el futuro de la humadidad en tus manos.", ExtraReadingTime = 2 } }
            };

            Dictionary<State, Dialogue> englishDialogues = new Dictionary<State, Dialogue>
            {
                { State.NewsDialogue1, new Dialogue { Message = "Creatures from space challenge humanity to video game combat!" } },
                { State.NewsDialogue2, new Dialogue { Message = "Experts do not agree on the true intentions hidden by the creatures" } },
                { State.DailyDialogue, new Dialogue { Message = "The UN and video game development studios are joining forces to select the best players in the world" } },
                { State.SecretDialogue1, new Dialogue { Message = "Congratulations!\nYou have been selected to represent humanity." } },
                { State.SecretDialogue2, new Dialogue { Message = "Your mission will be to collect information. Who are they? What do they seek? Where do they come from?", ExtraReadingTime = 1 } },
                { State.SecretDialogue3, new Dialogue { Message = "We cannot intervene in the games. But our experts designed some exploits that will be very useful for you.", ExtraReadingTime = 1 } },
                { State.SecretDialogue4, new Dialogue { Message = "Play, win and discover the true story behind it.\nYou have the future of humanity in your hands.", ExtraReadingTime = 2 } }
            };

            dialogueLines = new Dictionary<SystemLanguage, Dictionary<State, Dialogue>>
            {
                { SystemLanguage.Spanish, spanishDialogues },
                { SystemLanguage.English, englishDialogues }
            };
        }

        private void TransitionNextState(object sender, EventArgs e)
        {
            NextState();
        }

        void Start()
        {
            //first state from None to News
            NextState();
        }

        void NextState()
        {
            switch(currentState)
            {
                case State.None:
                    {
                        currentState = State.News;
                        news.SetActive(true);
                        break;
                    }
                case State.News:
                    {
                        currentState = State.NewsDialogue1;
                        break;
                    }
                case State.NewsDialogue1:
                    {
                        currentState = State.NewsDialogue2;
                        break;
                    }
                case State.NewsDialogue2:
                    {
                        currentState = State.Daily;
                        daily.SetActive(true);
                        break;
                    }
                case State.Daily:
                    {
                        currentState = State.DailyDialogue;
                        break;
                    }
                case State.DailyDialogue:
                    {
                        currentState = State.Secret;
                        secret.SetActive(true);
                        break;
                    }
                case State.Secret:
                    {
                        currentState = State.SecretDialogue1;
                        break;
                    }
                case State.SecretDialogue1:
                    {
                        currentState = State.SecretDialogue2;
                        monsterSection.SetActive(true);
                        break;
                    }
                case State.SecretDialogue2:
                    {
                        currentState = State.SecretDialogue3;
                        powerupSection.SetActive(true);
                        break;
                    }
                case State.SecretDialogue3:
                    {
                        currentState = State.SecretDialogue4;
                        break;
                    }
                case State.SecretDialogue4:
                    {
                        currentState = State.End;
                        fadeAnimator.SetInteger(FadeActor.FADE_PARAM, FadeActor.FADE_OUT);
                        break;
                    }
                case State.End:
                    {
                        if (SceneEnded == null) { return; }
                        SceneEnded(this, EventArgs.Empty);
                        break;
                    }
            }

            if (dialogueLines[Application.systemLanguage].ContainsKey(currentState))
            {
                dialogActor.Show(
                    message: dialogueLines[Application.systemLanguage][currentState].Message,
                    extraReadingTime: dialogueLines[Application.systemLanguage][currentState].ExtraReadingTime);
            }
        }
    }
}
