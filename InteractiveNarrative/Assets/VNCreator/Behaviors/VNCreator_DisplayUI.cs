using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        public Button choiceBtn5;
        [Header("End")]
        public GameObject endScreen;
        [Header("Main menu")]
        [Scene]
        public string mainMenu;

        void Start()
        {
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
                choiceBtn1.onClick.AddListener(delegate { NextNode(0); });
            if (choiceBtn2 != null)
                choiceBtn2.onClick.AddListener(delegate { NextNode(1); });
            if (choiceBtn3 != null)
                choiceBtn3.onClick.AddListener(delegate { NextNode(2); });
            if (choiceBtn4 != null)
                choiceBtn4.onClick.AddListener(delegate { NextNode(3); });
            if (choiceBtn5 != null)
                choiceBtn5.onClick.AddListener(delegate { NextNode(3); });


            if (endScreen = null)
            {
                endScreen.SetActive(false);
            }

            StartCoroutine(DisplayCurrentNode());
        }

        protected override void NextNode(int _choiceId)
        {
            if (lastNode)
            {
                if (endScreen != null)
                {
                    endScreen.SetActive(true);
                }
                return;
            }

            base.NextNode(_choiceId);
            StartCoroutine(DisplayCurrentNode());
        }

        IEnumerator DisplayCurrentNode()
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
                backgroundImg.sprite = currentNode.backgroundSpr;

            if (currentNode.choices <= 1)
            {
                if (nextBtn != null)
                {
                    nextBtn.gameObject.SetActive(true);
                }

                choiceBtn1.gameObject.SetActive(false);
                choiceBtn2.gameObject.SetActive(false);
                choiceBtn3.gameObject.SetActive(false);
                choiceBtn4.gameObject.SetActive(false);
                choiceBtn5.gameObject.SetActive(false);
                if (previousBtn != null)
                {
                    previousBtn.gameObject.SetActive(loadList.Count != 1);
                }
            }
            else
            {
                if (nextBtn != null)
                {
                    nextBtn.gameObject.SetActive(false);
                }

                choiceBtn1.gameObject.SetActive(true);
                choiceBtn1.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[0];

                choiceBtn2.gameObject.SetActive(true);
                choiceBtn2.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[1];

                if (currentNode.choices == 3)
                {
                    choiceBtn3.gameObject.SetActive(true);
                    choiceBtn3.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[2];
                }
                else
                {
                    choiceBtn3.gameObject.SetActive(false);
                }

                if (currentNode.choices > 3)
                {
                    choiceBtn3.gameObject.SetActive(true);
                    choiceBtn4.gameObject.SetActive(true);
                    choiceBtn5.gameObject.SetActive(true);

                    //add text
                    choiceBtn3.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[2];
                    choiceBtn4.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[3];
                    choiceBtn5.transform.GetChild(0).GetComponent<Text>().text = currentNode.choiceOptions[4];
                }
                else
                {
                    choiceBtn4.gameObject.SetActive(false);
                    choiceBtn5.gameObject.SetActive(false);
                }
            }

            if (currentNode.backgroundMusic != null)
                VNCreator_MusicSource.instance.Play(currentNode.backgroundMusic);
            if (currentNode.soundEffect != null)
                VNCreator_SfxSource.instance.Play(currentNode.soundEffect);

            dialogueTxt.text = string.Empty;
            if (GameOptions.isInstantText)
            {
                dialogueTxt.text = currentNode.dialogueText;
            }
            else
            {
                char[] _chars = currentNode.dialogueText.ToCharArray();
                string fullString = string.Empty;
                for (int i = 0; i < _chars.Length; i++)
                {
                    fullString += _chars[i];
                    dialogueTxt.text = fullString;
                    yield return new WaitForSeconds(0.01f / GameOptions.readSpeed);
                }
            }
        }

        protected override void Previous()
        {
            base.Previous();
            StartCoroutine(DisplayCurrentNode());
        }

        void ExitGame()
        {
            SceneManager.LoadScene(mainMenu, LoadSceneMode.Single);
        }
    }
}