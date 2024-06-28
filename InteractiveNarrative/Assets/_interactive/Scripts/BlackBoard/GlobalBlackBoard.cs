using System.Collections.Generic;
using UnityEngine;
using Shared;
using Codice.Client.Common.TreeGrouper;

namespace BlackBoard
{
    public class GlobalBlackBoard
    {
        private static GlobalBlackBoard _instance;
    
        private Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public static GlobalBlackBoard Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalBlackBoard();
                }
                return _instance;
            }
        }

        public T GetVariable<T>(string name)
        {
            if (dictionary.ContainsKey(name))
            {
                return (T)dictionary[name];
            }
            return default;
        }
        public void SetVariable<T>(string name, T variable)
        {
            // UnityEngine.Debug.Log(name + variable.ToString());
            dictionary[name] = variable;
        }

        public void CheckForIntrusiveThoughts(NodeData storyObject)
        {
            // Stel de kans in op een intrusieve gedachte (bijvoorbeeld 10%)
            float chance = 0.1f;
            bool hasIntrusiveThought = Random.value < chance;

            SetVariable("IntrusiveThought", hasIntrusiveThought);
        }
    }
}
