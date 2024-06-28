﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    [Serializable]
    public class NodeData
    {
        public string guid;
        public Sprite characterSpr;
        public string characterName;
        public string dialogueText;
        public Sprite backgroundSpr;
        public bool startNode;
        public bool endNode;
        public int choices = 1;
        [Range(-1, 1)]
        public int GoodOrBad = 0;
        public List<string> choiceOptions;
        public Rect nodePosition;
        public AudioClip soundEffect;
        public AudioClip backgroundMusic;

        public NodeData()
        {
            guid = Guid.NewGuid().ToString();
        }
    }
}