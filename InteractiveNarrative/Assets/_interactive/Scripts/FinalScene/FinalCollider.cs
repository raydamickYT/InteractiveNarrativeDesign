using System.Collections;
using System.Collections.Generic;
using BlackBoard;
using Microsoft.Unity.VisualStudio.Editor;
using Mouse;
using UnityEngine;
using UnityEngine.EventSystems;
using VNCreator;

public class FinalCollider : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject imageObject;
    private Image image;
    private Animator animator;

    void Awake()
    {
        PointerController.Instance.LoadSprites("MouseSprites");
        if (imageObject != null)
        {
            animator = imageObject.GetComponent<Animator>();
            image = imageObject.GetComponent<Image>();
        }

        if (image == null)
            Debug.LogError("Image is null");
        if (animator == null)
            Debug.LogError("Animator is null");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PointerController.Instance.MouseInputEnabled)
        {
            animator.SetBool("CanTransition", true);
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
