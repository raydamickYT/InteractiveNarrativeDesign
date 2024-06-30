using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Choice
{
    [TextArea, Tooltip("Vul hier in welke tekst het karakter moet zeggen")]
    public string ChoiceText;
    
    [TextArea, Tooltip("dit is wat er op de knoppen komt")]
    public string NegativeAnswer, PositiveAnswer, NeutralAnswer;
    [Tooltip("Toggle buttons")]
    public bool EnableChoices = false;

    //hieronder kan je andere dingen toevoegen, zoals welke sprite er op beeld moet komen en welke keuzes er zijn.
}

[System.Serializable]
public class Scenes
{
    // public SceneNode[] ScenesList;
}
