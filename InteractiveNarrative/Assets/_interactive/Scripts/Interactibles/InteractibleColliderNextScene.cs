using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Mouse;
using BlackBoard;

public class InteractibleColliderNextScene : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject PopUp;
    public string NextScene;
    private GameObject instantiatedObject;
    private bool hasSpawned;
    private bool hasLeft;

    public void OnPointerDown(PointerEventData eventData)
    {
        GlobalBlackBoard.Instance.SetScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(NextScene);
        hasLeft = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log(hasSpawned);
        if (PointerController.Instance.MouseInputEnabled)
        {
            Debug.Log(PointerController.Instance.MouseInputEnabled);
            PointerController.Instance.ChangeMouseToHand();

            if (!hasSpawned)
            {
                instantiatedObject = Instantiate(PopUp);
                instantiatedObject.GetComponentInChildren<Text>().text = $"Go to {NextScene}?";
                hasSpawned = true;
            }
            else
                instantiatedObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!hasLeft && PointerController.Instance.MouseInputEnabled)
        {
            PointerController.Instance.ChangeMouseToCursor();
            instantiatedObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        instantiatedObject = null;
        hasLeft = false;
    }
}
