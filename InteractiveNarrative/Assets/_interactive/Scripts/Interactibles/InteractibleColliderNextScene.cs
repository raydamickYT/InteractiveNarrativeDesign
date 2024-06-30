using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Mouse;

public class InteractibleColliderNextScene : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject PopUp;
    public string NextScene;
    private GameObject instantiatedObject;
    private bool hasSpawned;

    void Start()
    {
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene(NextScene);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(hasSpawned);
        if (PointerController.Instance.MouseInputEnabled)
        {
            PointerController.Instance.ChangeMouseToHand();
        }

        if (!hasSpawned)
        {
            instantiatedObject = Instantiate(PopUp);
            instantiatedObject.GetComponentInChildren<Text>().text = $"Go to {NextScene}?";
            hasSpawned = true;
        }
        else
            instantiatedObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerController.Instance.ChangeMouseToCursor();
        instantiatedObject.SetActive(false);

    }
}
