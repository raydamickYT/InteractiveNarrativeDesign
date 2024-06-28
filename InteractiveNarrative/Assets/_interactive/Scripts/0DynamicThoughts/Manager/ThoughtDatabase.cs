 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VNCreator;


[CreateAssetMenu(fileName = "ThoughtDatabase", menuName = "Thoughts/Database")]
public class ThoughtDatabase : ScriptableObject
{
    public List<StoryObject> storyObjects = new List<StoryObject>();


    // Methode om een gedachte op te halen op basis van een specifieke context
    public StoryObject GetThoughtByContext(string context)
    {
        foreach (var storyObject in storyObjects)
        {
            if (storyObject.Context == context)
            {
                // Hier kun je bijvoorbeeld een gedachte genereren of een specifieke gedachte ophalen
                // return storyObject.GenerateThoughtForCurrentContext();
            }
        }

        Debug.LogWarning($"Geen gedachten gevonden voor context: {context}");
        return null;
    }
}
