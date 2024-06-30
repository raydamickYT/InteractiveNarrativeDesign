using System.Collections;
using System.Collections.Generic;
using BlackBoard;
using Mouse;
using UnityEngine;
using UnityEngine.EventSystems;
using VNCreator;

public class InteractibleCollider : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public StoryObject MainStoryObject, SideStoryObject;

    void Awake()
    {
        PointerController.Instance.LoadSprites("MouseSprites");
        // PointerController.Instance.LoadSprites("MouseSprites/MouseHand");
        if (MainStoryObject == null)
        {
            Debug.LogError($"MainStoryObject not assigned in gameobject: \"{transform.gameObject.name}.\". ");
        }

        if (SideStoryObject == null)
        {
            Debug.LogError($"SideStoryObject not assigned in gameobject: \"{transform.gameObject.name}.\". ");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PointerController.Instance.MouseInputEnabled)
        {
            VNCreator_DisplayUI dispUI = GlobalBlackBoard.Instance.GetVariable<VNCreator_DisplayUI>("DisplayUI");
            bool b = GlobalBlackBoard.Instance.GetVariable<bool>(gameObject.name);
            if (!b)
            {
                dispUI.story = MainStoryObject;
                GlobalBlackBoard.Instance.SetVariable(gameObject.name, true);
            }
            else
            {
                dispUI.story = SideStoryObject;
            }

            dispUI.StartStory();
            StartCoroutine(dispUI.DisplayCurrentNode());

            GlobalBlackBoard.Instance.EnableInputAction?.Invoke(false);
            GlobalBlackBoard.Instance.ChangeMouseToHandAction?.Invoke();
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
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
