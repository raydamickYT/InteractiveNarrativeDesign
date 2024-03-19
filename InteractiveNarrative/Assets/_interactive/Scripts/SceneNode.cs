using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneNode : MonoBehaviour
{
    public SceneNode NextNode;

    //TODO:
    /// <summary>
    //deze class moet het volgende bevatten
    /// - een class die tekst bevat,
    /// - een array met dit ^
    /// - een ui element waarin de tekst wordt aangepast
    /// - een array met de keuzes
    /// 
    /// </summary>
    public Choice[] choices;
    public float ChoiceImpact = 0.2f;
    private int ChoiceIndex = 0;
    private Text currentText;
    public string Description;
    public Canvas canvas;
    public List<Button> Buttons = new();

    // Start is called before the first frame update
    void Start()
    {
        if (canvas != null)
        {
            Initialise();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateNode()
    {

    }
    public void Initialise()
    {
        Button[] buttonsArray = canvas.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttonsArray.Length; i++)
        {
            Buttons.Add(buttonsArray[i]);
            int buttonIndex = i; // Vang de huidige index
            buttonsArray[i].onClick.AddListener(delegate
            {
                MakeChoice(buttonIndex); //stop hem in de public bool zodat er iets mee kan gebeuren
            });
        }
        currentText = canvas.GetComponentInChildren<Text>();
        UpdateChoice();
    }
    public void UpdateChoice()
    {
        currentText.text = choices[ChoiceIndex].ChoiceText;
        ChoiceIndex++;
    }
    public void MakeChoice(int Index)
    {
        Debug.Log(Index);

        switch (Index)
        {
            case 0: //speler heeft ja gedrukt
                GlobalPersonalityMeter.Instance.UpdateGoodBadIndex?.Invoke(ChoiceImpact);
                break;
            case 1: //speler heeft nee gedrukt
                GlobalPersonalityMeter.Instance.UpdateGoodBadIndex?.Invoke(-ChoiceImpact);
                break;
            default:
                break;
        }
    }
}

