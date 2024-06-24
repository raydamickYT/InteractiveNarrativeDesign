using System.Collections.Generic;
using UnityEngine;

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
            return default(T);
        }
        public void SetVariable<T>(string name, T variable)
        {
            // UnityEngine.Debug.Log(name + variable.ToString());
            dictionary[name] = variable;
        }
    }
}
