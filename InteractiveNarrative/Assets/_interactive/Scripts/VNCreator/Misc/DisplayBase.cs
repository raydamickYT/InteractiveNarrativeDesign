using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BlackBoard;
using UnityEngine;

namespace VNCreator
{
    public class DisplayBase : MonoBehaviour
    {
        public StoryObject story;

        protected NodeData currentNode;
        protected bool lastNode;
        [Range(-100, 100)]
        private int _goodOrBadMetre = 0;
        public int GoodOrBadMetre
        {
            get { return _goodOrBadMetre; }
            set
            {
                if (_goodOrBadMetre != value)
                {
                    _goodOrBadMetre = value;
                    OnGoodOrBadMetreChanged?.Invoke(_goodOrBadMetre);
                    _goodOrBadMetre = 0;
                }
            }
        }

        public Action<int> OnGoodOrBadMetreChanged;

        protected List<string> loadList = new List<string>();

        void Awake()
        {
            Initialization();
        }

        public virtual void Initialization()
        {
            if (PlayerPrefs.GetString(GameSaveManager.currentLoadName) == string.Empty)
            {
                currentNode = story.GetFirstNode();
                loadList.Add(currentNode.guid);
            }
            else
            {
                loadList = GameSaveManager.Load();
                if (loadList == null || loadList.Count == 0)
                {
                    currentNode = story.GetFirstNode();
                    loadList = new List<string>
                    {
                        currentNode.guid
                    };
                }
                else
                {
                    currentNode = story.GetCurrentNode(loadList[loadList.Count - 1]);
                }
            }
        }
        public void StartStory()
        {
            if (PlayerPrefs.GetString(GameSaveManager.currentLoadName) == string.Empty)
            {
                currentNode = story.GetFirstNode();
                loadList.Add(currentNode.guid);
            }
            else
            {
                loadList = GameSaveManager.Load();
                if (loadList == null || loadList.Count == 0)
                {
                    currentNode = story.GetFirstNode();
                    loadList = new List<string>
                    {
                        currentNode.guid
                    };
                }
                else
                {
                    currentNode = story.GetCurrentNode(loadList[loadList.Count - 1]);
                }
            }
        }

        protected virtual void NextNode(int _choiceId)
        {

            if (!lastNode)
            {
                //als er geen last node is, dan is de volgende node een end node
                currentNode = story.GetNextNode(currentNode.guid, _choiceId);
                GoodOrBadMetre += currentNode.GoodOrBad;
                GlobalBlackBoard.Instance.CheckForIntrusiveThoughts(story.context);

                lastNode = currentNode.endNode;
                // Debug.Log("Context2" + story.context);
                loadList.Add(currentNode.guid);
            }
        }

        protected virtual void Previous()
        {
            loadList.RemoveAt(loadList.Count - 1);
            currentNode = story.GetCurrentNode(loadList[loadList.Count - 1]);
            lastNode = currentNode.endNode;
        }

        protected void Save()
        {
            GameSaveManager.Save(loadList);
        }
    }
}
