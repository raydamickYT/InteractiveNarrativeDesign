using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BlackBoard;
using Mouse;

namespace VNCreator
{
    public class VNCreator_DisplayUI : DisplayBase
    {
        [Header("Text")]
        public Text characterNameTxt;
        public Text dialogueTxt;

        [Header("Visuals")]
        public Image characterImg;
        public Image backgroundImg;
        [Header("Audio")]
        public AudioSource musicSource;
        public AudioSource soundEffectSource;
        [Header("Buttons")]
        public Button nextBtn;
        public Button previousBtn;
        public Button saveBtn;
        public Button menuButton;
        [Header("Choices")]
        public Button choiceBtn1;
        public Button choiceBtn2;
        public Button choiceBtn3;
        public Button choiceBtn4;
        [Header("End")]
        public GameObject endScreen;

        [Header("Main menu Scene")]
        [Scene]
        public string mainMenu;
        [Header("NextScene")]
        [Scene]
        public string NextScene;

        private bool CanPressSpace = false; //voor het geval er choices zijn, dan mag je die niet
        private bool isDisplayingText = false; // Vlag om bij te houden of tekst wordt weergegeven


        void Start()
        {

            // StartCoroutine(DisplayCurrentNode());
        }
        public override void Initialization()
        {
            //run de code in de base
            base.Initialization();

            //local init
            GlobalBlackBoard.Instance.SetVariable("DisplayUI", this);


            if (nextBtn != null)
            {
                nextBtn.onClick.AddListener(delegate { NextNode(0); }); //dit doet eigenlijk het zelfde als choice button 1
            }
            else
            {
                Debug.LogWarning("Add A nextBtn Node");
            }
            if (previousBtn != null)
                previousBtn.onClick.AddListener(Previous);
            if (saveBtn != null)
                saveBtn.onClick.AddListener(Save);
            if (menuButton != null)
                menuButton.onClick.AddListener(ExitGame);

            if (choiceBtn1 != null)
            {
                choiceBtn1.onClick.AddListener(delegate { NextNode(0); });
                choiceBtn1.gameObject.SetActive(false);
            }
            if (choiceBtn2 != null)
            {
                choiceBtn2.onClick.AddListener(delegate { NextNode(1); });
                choiceBtn2.gameObject.SetActive(false);
            }
            if (choiceBtn3 != null)
            {
                choiceBtn3.onClick.AddListener(delegate { NextNode(2); });
                choiceBtn3.gameObject.SetActive(false);
            }
            if (choiceBtn4 != null)
            {
                choiceBtn4.onClick.AddListener(delegate { NextNode(3); });
                choiceBtn4.gameObject.SetActive(false);
            }

            if (dialogueTxt != null)
            {
                dialogueTxt.gameObject.SetActive(false);
            }

            if (characterNameTxt != null)
                characterNameTxt.gameObject.SetActive(false);

            if (endScreen = null)
            {
                endScreen.SetActive(false);
            }

        }
        private void Reset()
        {
            GlobalBlackBoard.Instance.SetVariable("IsThinking", false); //laat weten dat er weer een nieuwe sequence begonnen is.
            PointerController.Instance.EnableInput(true);
            lastNode = false;
            if (dialogueTxt != null)
            {
                dialogueTxt.gameObject.SetActive(false);
            }

            if (characterNameTxt != null)
                characterNameTxt.gameObject.SetActive(false);

            if (endScreen = null)
            {
                endScreen.SetActive(false);
            }
        }

        private void Update()
        {
            if (currentNode != null)
            {
            if (Input.GetKeyDown(KeyCode.Space) && CanPressSpace)
                {
                    HandleSpaceKeyPress();
                }
            }
        }

        private void HandleSpaceKeyPress()
        {
            if (isDisplayingText)
            {
                StopDisplayingText();
                return; // Stop hier om niet door te gaan naar de volgende node
            }

            if (!lastNode)
            {
                NextNode(0);
            }
            else
            {
                Reset();
            }
        }

        private void StopDisplayingText()
        {
            StopCoroutine(displayTextCoroutine);
            dialogueTxt.text = currentNode.dialogueText;
            isDisplayingText = false;
        }

        protected override void NextNode(int _choiceId)
        {
            // Debug.Log(_choiceId);
            // ThoughtManager
            base.NextNode(_choiceId);
            StartCoroutine(DisplayCurrentNode());
        }

        private Coroutine displayTextCoroutine; // Coroutine referentie

        public IEnumerator DisplayCurrentNode()
        {
            if (characterNameTxt != null)
            {
                characterNameTxt.text = currentNode.characterName;
            }
            if (currentNode.characterSpr != null)
            {
                characterImg.sprite = currentNode.characterSpr;
                characterImg.color = Color.white;
            }
            else
            {
                characterImg.color = new Color(1, 1, 1, 0);
            }
            if (currentNode.backgroundSpr != null)
            {
                backgroundImg.sprite = currentNode.backgroundSpr;
            }

            HandleChoices();

            if (currentNode.backgroundMusic != null)
            {
                VNCreator_MusicSource.instance.Play(currentNode.backgroundMusic);
            }
            if (currentNode.soundEffect != null)
            {
                VNCreator_SfxSource.instance.Play(currentNode.soundEffect);
            }

            dialogueTxt.text = string.Empty;
            dialogueTxt.gameObject.SetActive(false);
            characterNameTxt.gameObject.SetActive(true);

            if (GameOptions.isInstantText)
            {
                dialogueTxt.gameObject.SetActive(true);
                dialogueTxt.text = currentNode.dialogueText;
            }
            else
            {
                displayTextCoroutine = StartCoroutine(DisplayDialogueText(currentNode.dialogueText));
                yield return displayTextCoroutine;
            }

        }

        private void HandleChoices()
        {
            bool hasSingleChoice = currentNode.choices <= 1;

            nextBtn?.gameObject.SetActive(hasSingleChoice);
            CanPressSpace = hasSingleChoice;

            choiceBtn1.gameObject.SetActive(false);
            choiceBtn2.gameObject.SetActive(false);
            choiceBtn3.gameObject.SetActive(false);
            choiceBtn4.gameObject.SetActive(false);

            if (hasSingleChoice)
            {
                previousBtn?.gameObject.SetActive(loadList.Count != 1);
            }
            else
            {
                SetChoiceButton(choiceBtn1, currentNode.choiceOptions[0], true);
                SetChoiceButton(choiceBtn2, currentNode.choiceOptions[1], true);

                if (currentNode.choices >= 3)
                {
                    SetChoiceButton(choiceBtn3, currentNode.choiceOptions[2], true);
                }
                if (currentNode.choices == 4)
                {
                    SetChoiceButton(choiceBtn4, currentNode.choiceOptions[3], true);
                }
            }
        }

        private void SetChoiceButton(Button button, string text, bool active)
        {
            button.gameObject.SetActive(active);
            if (active)
            {
                button.transform.GetChild(0).GetComponent<Text>().text = text;
            }
        }

        private IEnumerator DisplayDialogueText(string dialogueText)
        {
            isDisplayingText = true;
            char[] chars = dialogueText.ToCharArray();
            string fullString = string.Empty;

            for (int i = 0; i < chars.Length; i++)
            {
                fullString += chars[i];
                characterNameTxt.gameObject.SetActive(true);
                dialogueTxt.gameObject.SetActive(true);
                dialogueTxt.text = fullString;
                yield return new WaitForSeconds(0.01f / GameOptions.readSpeed);
            }
            isDisplayingText = false;
        }


        protected override void Previous()
        {
            base.Previous();
            StartCoroutine(DisplayCurrentNode());
        }

        void ExitGame()
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenu, LoadSceneMode.Single);
        }

    }
}