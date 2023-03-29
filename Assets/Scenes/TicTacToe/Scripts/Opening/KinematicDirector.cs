using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Opening
{
    public class KinematicDirector : MonoBehaviour
    {
        enum KinematicScene
        {
            None,
            Portal,
            Travel,
            Fall,
            Mision,
        }

        [SerializeField] KinematicScene InitialKinematicScene;
        [SerializeField] GameObject portalScene;
        [SerializeField] GameObject travelScene;
        [SerializeField] GameObject fallScene;
        [SerializeField] GameObject misionScene;

        PortalScene.KinematicDirector portalDirector;
        TravelScene.KinematicDirector travelDirector;
        FallScene.KinematicDirector fallDirector;
        MisionScene.KinematicDirector misionDirector;

        GameObject currentScene;

        private void Awake()
        {
            portalDirector = portalScene.GetComponent<PortalScene.KinematicDirector>();
            portalDirector.SceneEnded += PortalDirector_SceneEnded;

            travelDirector = travelScene.GetComponent<TravelScene.KinematicDirector>();
            travelDirector.SceneEnded += TravelDirector_SceneEnded;

            fallDirector = fallScene.GetComponent<FallScene.KinematicDirector>();
            fallDirector.SceneEnded += FallDirector_SceneEnded;

            misionDirector = misionScene.GetComponent<MisionScene.KinematicDirector>();
            misionDirector.SceneEnded += MisionDirector_SceneEnded;
        }

        void Start()
        {
            TransitionScene(InitialKinematicScene);
        }

        private void PortalDirector_SceneEnded(object sender, System.EventArgs e)
        {
            TransitionScene(KinematicScene.Travel);
        }

        private void TravelDirector_SceneEnded(object sender, System.EventArgs e)
        {
            TransitionScene(KinematicScene.Fall);
        }

        private void FallDirector_SceneEnded(object sender, System.EventArgs e)
        {
            TransitionScene(KinematicScene.Mision);
        }

        private void MisionDirector_SceneEnded(object sender, System.EventArgs e)
        {
            currentScene?.SetActive(false);
            SceneManager.LoadScene("Scenes/TicTacToe/MenuScene", LoadSceneMode.Single);
        }

        private void TransitionScene(KinematicScene kinematicScene)
        {
            currentScene?.SetActive(false);

            switch (kinematicScene)
            {
                case KinematicScene.None:
                    {
                        currentScene = portalScene; break;
                    }
                case KinematicScene.Portal:
                    {
                        currentScene = portalScene; break;
                    }
                case KinematicScene.Travel:
                    {
                        currentScene = travelScene; break;
                    }
                case KinematicScene.Fall:
                    {
                        currentScene = fallScene; break;
                    }
                case KinematicScene.Mision:
                    {
                        currentScene = misionScene; break;
                    }
                default:
                    {
                        currentScene = null; break;
                    }
            }

            currentScene?.SetActive(true);
        }
    }

}
