// using System.Collections;
// using System.Collections.Generic;
// using System.Net.WebSockets;
// using Unity.Mathematics;
// using UnityEngine;
// using UnityEngine.UI;

// public class SceneNode : MonoBehaviour
// {
//     public Choice[] choices;
//     public float ChoiceImpact = 0.2f;
//     private int ChoiceIndex = 0;
//     private Text currentText;
//     // public string Description;
//     public Canvas canvas;
//     public List<Button> Buttons = new();

//     // Start is called before the first frame update
//     void Start()
//     {
//         Debug.Log(SceneManager.Instance);
//         if (canvas != null)
//         {
//             Initialise();
//         }
//         else
//         {
//             Debug.LogError("canvas is niet assigned"); ;
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (!choices[ChoiceIndex-1].EnableChoices)
//         {
//             if (Input.GetKeyDown(KeyCode.Space))
//             {
//                 UpdateChoice();
//             }
//         }
//     }
//     public void Initialise()
//     {
//         SetupStarterScene();
//         Button[] buttonsArray = canvas.GetComponentsInChildren<Button>();

//         for (int i = 0; i < buttonsArray.Length; i++)
//         {
//             Buttons.Add(buttonsArray[i]);
//             int buttonIndex = i; // Vang de huidige index
//             buttonsArray[i].onClick.AddListener(delegate
//             {
//                 MakeChoice(buttonIndex); //stop hem in de public bool zodat er iets mee kan gebeuren
//             });
//         }
//         currentText = canvas.GetComponentInChildren<Text>();
//         UpdateChoice();
//     }
//     private void OnDestroy()
//     {
//         foreach (var item in Buttons)
//         {
//             item.onClick.RemoveAllListeners();
//         }
//     }

//     public void UpdateChoice()
//     {
//         Debug.Log("choice index: " + ChoiceIndex + " choices.length: " + choices.Length);
//         if (ChoiceIndex == choices.Length)
//         {
//             LoadNextScene();
//             return;
//         }
//         currentText.text = choices[ChoiceIndex].ChoiceText;

//         foreach (Button item in Buttons)
//         {
//             item.gameObject.SetActive(choices[ChoiceIndex].EnableChoices);
//             // if (!choices[ChoiceIndex].EnableChoices) break; //als de enablechoices uit staat

//             var textComponent = item.GetComponentInChildren<Text>();
//             switch (item.name.ToLower())
//             {
//                 case "yes":
//                     textComponent.text = choices[ChoiceIndex].PositiveAnswer;
//                     break;
//                 case "no":
//                     textComponent.text = choices[ChoiceIndex].NegativeAnswer;
//                     break;
//                 default:
//                     textComponent.text = choices[ChoiceIndex].NeutralAnswer;
//                     break;
//             }
//         }
//         //als laatste wil ik pas de choice index updaten
//         ChoiceIndex++;
//     }
//     public void LoadNextScene()
//     {
//         SceneManager.Instance.SceneIndex++;
//         var sceneIndex = SceneManager.Instance.SceneIndex;
//         Debug.Log(sceneIndex);
//         Debug.LogWarning("alle choices zijn op");
//         var tempIndex = GlobalPersonalityMeter.Instance.PublicIndex;
//         var tempBorder = GlobalPersonalityMeter.Instance.PersonalityBorder;
//         SceneNode TempNode;

//         if (tempIndex >= tempBorder) //on good path
//         {
//             if (sceneIndex < SceneManager.Instance.GoodScenes.ScenesList.Length)
//             {
//                 TempNode = SceneManager.Instance.GoodScenes.ScenesList[sceneIndex];
//             }
//             else
//             {
//                 TempNode = null;
//                 Debug.LogWarning("er zijn geen nieuwe scenes meer om te laden");
//             }
//         }
//         else if (tempIndex <= -tempBorder) //on bad Path
//         {
//             if (sceneIndex < SceneManager.Instance.BadScenes.ScenesList.Length)
//             {
//                 TempNode = SceneManager.Instance.BadScenes.ScenesList[sceneIndex];
//             }
//             else
//             {
//                 TempNode = null;
//                 Debug.LogWarning("er zijn geen nieuwe scenes meer om te laden");
//             }
//         }
//         else //else we're inbetween and thus on neutral.
//         {
//             if (sceneIndex < SceneManager.Instance.NeutralScenes.ScenesList.Length)
//             {
//                 TempNode = SceneManager.Instance.NeutralScenes.ScenesList[sceneIndex];
//             }
//             else
//             {
//                 TempNode = null;
//                 Debug.LogWarning("er zijn geen nieuwe scenes meer om te laden");
//             }
//         }
//         if (TempNode != null)
//         {
//             choices = TempNode.choices;
//         }
//         ChoiceIndex = 0;
//         UpdateChoice();
//         //voer hier einde scene logica uit
//     }


//     public void SetupStarterScene()
//     {
//         SceneNode TempNode = SceneManager.Instance.NeutralScenes.ScenesList[0]; //moet sowieso altijd 0 zijn
//         choices = TempNode.choices;
//     }
//     public void MakeChoice(int Index)
//     {
//         // Debug.Log(Index);

//         switch (Index)
//         {
//             case 0: //speler heeft ja gedrukt
//                 GlobalPersonalityMeter.Instance.UpdateGoodBadIndex?.Invoke(ChoiceImpact);
//                 UpdateChoice();
//                 break;
//             case 1: //speler heeft nee gedrukt
//                 GlobalPersonalityMeter.Instance.UpdateGoodBadIndex?.Invoke(-ChoiceImpact);
//                 UpdateChoice();
//                 break;
//             case 2: //neutral button
//                 GlobalPersonalityMeter.Instance.UpdateGoodBadIndex?.Invoke(0); //omdat neutraal geen impact heeft
//                 UpdateChoice();
//                 break;
//             default:
//                 break;
//         }
//     }
// }

