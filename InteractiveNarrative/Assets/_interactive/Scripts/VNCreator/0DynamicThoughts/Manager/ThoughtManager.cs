using System.Collections;
using System.Collections.Generic;
using BlackBoard;
using UnityEngine;
using VNCreator;

public class ThoughtManager : MonoBehaviour
{
    public ThoughtDatabase thoughtDatabase;
    public ThoughtUIManager thoughtUIManager;

    private List<StoryObject> runtimeStoryObjects; // Temporary list for runtime use


    private bool hasSubscribed;

    void Start()
    {
        GlobalBlackBoard.Instance.StartIntrusiveThoughtAction += StartThought;

        if (thoughtDatabase == null)
        {
            Debug.LogError($"ThoughtDatabase not assigned in {gameObject.name}.");
        }
        else if (thoughtDatabase.storyObjects.Count == 0)
        {
            Debug.LogWarning($"{thoughtDatabase} is empty in {gameObject.name}.");
        }
        runtimeStoryObjects = new List<StoryObject>(thoughtDatabase.storyObjects);


        if (thoughtUIManager == null)
            Debug.LogError($"{thoughtUIManager} is empty in {gameObject.name}.");

    }

    void StartThought()
    {
        string ctx = GlobalBlackBoard.Instance.GetVariable<string>(GlobalBlackBoard.Instance.ThoughtContextStr);
        GetThoughtByContext(ctx);
    }

    // Methode om een gedachte op te halen op basis van een specifieke context
    public void GetThoughtByContext(string context)
    {
        List<StoryObject> matchingStoryObjects = new List<StoryObject>();

        // Verzamel alle StoryObjects die overeenkomen met de context
        foreach (var storyObject in runtimeStoryObjects)
        {
            if (storyObject.context == context)
            {
                matchingStoryObjects.Add(storyObject);
            }
        }

        // Als er geen overeenkomende StoryObjects zijn, voer een fallback actie uit
        if (matchingStoryObjects.Count == 0)
        {
            Debug.LogWarning($"Geen gedachten gevonden voor context: {context}");
            var str = GlobalBlackBoard.Instance.ThoughtContextStr;
            GlobalBlackBoard.Instance.SetVariable<string>(str, null);
            return;
        }

        // Kies een willekeurige StoryObject uit de lijst
        int randomIndex = Random.Range(0, matchingStoryObjects.Count);
        StoryObject selectedStoryObject = matchingStoryObjects[randomIndex];
        runtimeStoryObjects.Remove(selectedStoryObject); //verwijder hem daarna uit de lijst zodat hij maar een keer gecalled kan worden.

        // Voer de acties uit voor het geselecteerde StoryObject
        VNCreator_DisplayUI dispUI = GlobalBlackBoard.Instance.GetVariable<VNCreator_DisplayUI>("DisplayUI");
        if (!hasSubscribed)
            dispUI.OnGoodOrBadMetreChanged += UpdateSlider;

        if (dispUI != null)
        {
            GlobalBlackBoard.Instance.SetVariable("IsThinking", true); //laat weten dat er een actief is.

            dispUI.story = selectedStoryObject;
            dispUI.StartStory();
            StartCoroutine(dispUI.DisplayCurrentNode());
            thoughtUIManager.EnableUI();
        }

        GlobalBlackBoard.Instance.EnableInputAction?.Invoke(false);
        GlobalBlackBoard.Instance.ChangeMouseToHandAction?.Invoke();

        return;
    }

    private void UpdateSlider(int GoodOrBad)
    {
        Debug.Log(GoodOrBad);
        if (GoodOrBad == 100)
        StartCoroutine(thoughtUIManager.DisableUI());//thought is klaar

        else if(GoodOrBad > 0)
            thoughtUIManager.AddToSliderValue(5);
    }

    void OnDestroy()
    {
        GlobalBlackBoard.Instance.StartIntrusiveThoughtAction -= StartThought;
    }
}
