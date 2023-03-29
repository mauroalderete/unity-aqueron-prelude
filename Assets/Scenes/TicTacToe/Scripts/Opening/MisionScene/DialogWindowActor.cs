using Codice.Client.BaseCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Opening.MisionScene
{
    public class DialogWindowActor : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI textField;
        [SerializeField] float ReadingTime;
        [SerializeField] float WritingSpeed;

        public event EventHandler Showed;
        public event EventHandler Hidden;

        string message;
        float extraReadingTime;

        private void Awake()
        {
            Showed += DialogWindowActor_Showed;
        }

        void OnShow()
        {
            if (Showed == null) { return;  }
            Showed(this, EventArgs.Empty);
        }

        void OnHide() {
            if (Hidden == null) { return; }
            Hidden(this, EventArgs.Empty);
        }

        public void Show(string message, float extraReadingTime = 0)
        {
            textField.text = String.Empty;
            this.message = Utils.DialogueCurator.Curate(message);
            this.extraReadingTime = extraReadingTime;

            Animator animator = GetComponent<Animator>();
            animator.SetInteger("Anim", 1);
        }

        public void Hide()
        {
            textField.text = String.Empty;
            this.message = string.Empty;

            Animator animator = GetComponent<Animator>();
            animator.SetInteger("Anim", 2);
        }

        private void DialogWindowActor_Showed(object sender, EventArgs e)
        {
            StartCoroutine("ShowLetter");
        }

        IEnumerator ShowLetter()
        {
            foreach (var m in message) {
                yield return new WaitForSeconds(WritingSpeed);
                textField.text += m;
            }

            yield return new WaitForSeconds( ReadingTime + extraReadingTime);
            Hide();
        }
    }

}
