using System.Collections;
using System.Collections.Generic;
using BlackBoard;
using Mouse;
using UnityEngine;
using UnityEngine.EventSystems;
using VNCreator;

public class InteractibleCollider : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public StoryObject StoryObject;

    void Awake()
    {
        PointerController.Instance.LoadSprites("MouseSprites");
        // PointerController.Instance.LoadSprites("MouseSprites/MouseHand");
        if (StoryObject == null)
        {
            Debug.LogError($"StoryObject not assigned in gameobject: \"{transform.gameObject.name}.\". ");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bool canClick = PointerController.Instance.MouseInputEnabled;
        if (canClick)
        {
            VNCreator_DisplayUI dispUI = GlobalBlackBoard.Instance.GetVariable<VNCreator_DisplayUI>("DisplayUI");
            dispUI.story = StoryObject;
            dispUI.StartStory();

            StartCoroutine(dispUI.DisplayCurrentNode());
            PointerController.Instance.EnableInput(false);
            PointerController.Instance.ChangeMouseToCursor();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
        if (PointerController.Instance.MouseInputEnabled)
        {
            PointerController.Instance.ChangeMouseToHand();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
        PointerController.Instance.ChangeMouseToCursor();
    }
}
