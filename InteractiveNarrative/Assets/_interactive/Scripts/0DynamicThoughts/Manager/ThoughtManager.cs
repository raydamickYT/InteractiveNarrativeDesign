using System.Collections;
using System.Collections.Generic;
using BlackBoard;
using UnityEngine;

public class ThoughtManager : MonoBehaviour
{
    public ThoughtDatabase thoughtDatabase;
    void Start()
    {
        GlobalBlackBoard.Instance.StartIntrusiveThought += StartThought;
    }

    void StartThought()
    {
        string ctx = GlobalBlackBoard.Instance.GetVariable<string>("IntrusiveThought");
        thoughtDatabase.GetThoughtByContext(ctx);
    }
}
