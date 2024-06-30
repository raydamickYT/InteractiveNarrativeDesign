using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlackBoard
{
    public class GlobalBlackBoard
    {
        private static GlobalBlackBoard _instance;

        public string ThoughtContextStr = "IntrusiveThoughtContext";

        private Dictionary<string, object> dictionary = new Dictionary<string, object>();
        public Action StartIntrusiveThoughtAction, ChangeMouseToHandAction;
        public Action<bool> EnableInputAction;

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
        public GlobalBlackBoard(){
            SetVariable("IsThinking", false);
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

        public void CheckForIntrusiveThoughts(string Context)
        {
            // Stel de kans in op een intrusieve gedachte (bijvoorbeeld 10%)
            float chance = 1f;
            bool hasIntrusiveThought = UnityEngine.Random.value < chance;

            bool isThinking = GetVariable<bool>("IsThinking");
            if (hasIntrusiveThought && !isThinking)
            {
                SetVariable(ThoughtContextStr, Context);
                StartIntrusiveThoughtAction?.Invoke();
            }
        }

        ~GlobalBlackBoard()
        {
            EnableInputAction = null;
            StartIntrusiveThoughtAction = null;
            ChangeMouseToHandAction = null;
        }
    }
}
