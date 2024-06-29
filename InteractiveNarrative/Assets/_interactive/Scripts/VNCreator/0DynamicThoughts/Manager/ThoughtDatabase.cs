using System;
using System.Collections;
using System.Collections.Generic;
using BlackBoard;
using Codice.Client.BaseCommands;
using Unity.VisualScripting.IonicZip;
using UnityEngine;
using VNCreator;


[CreateAssetMenu(fileName = "ThoughtDatabase", menuName = "Thoughts/ThoughtDatabase")]
public class ThoughtDatabase : ScriptableObject
{
    public List<StoryObject> storyObjects = new List<StoryObject>();



}
