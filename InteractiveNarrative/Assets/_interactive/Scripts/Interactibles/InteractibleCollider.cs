using System.Collections;
using System.Collections.Generic;
using BlackBoard;
using UnityEngine;
using UnityEngine.EventSystems;
using VNCreator;

public class InteractibleCollider : MonoBehaviour, IPointerDownHandler
{
    public StoryObject StoryObject;

    void Awake()
    {
        if (StoryObject == null)
        {
            Debug.LogError($"StoryObject not assigned in gameobject: \"{transform.gameObject.name}.\". ");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        VNCreator_DisplayUI dispUI = GlobalBlackBoard.Instance.GetVariable<VNCreator_DisplayUI>("DisplayUI");
        dispUI.story = StoryObject;
        dispUI.Initialization();

        StartCoroutine(dispUI.DisplayCurrentNode());
    }
}
