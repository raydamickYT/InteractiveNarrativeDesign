using System.Collections;
using System.Threading.Tasks;
using BlackBoard;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;


public class ThoughtUIManager : MonoBehaviour
{
    public Slider slider;
    public RectTransform bar;
    public Canvas canvas;
    private Image Vignette;

    [Tooltip("Step size at which the slider value decreases")]
    public int downStep = 1;
    [Tooltip("Time interval between each step in seconds")]
    public float stepInterval = 0.5f;
    private bool isGameOver = false, isMoving = true;
    private int duration = 30;
    private Coroutine coroutine;

    void Start()
    {
        if (Vignette == null)
        {
            var t = canvas.gameObject.transform.Find("Vignette");
            Vignette = t.GetComponent<Image>();
        }
    }
    void Update()
    {
        if (isGameOver)
        {
            StopCoroutine(coroutine);
            Debug.Log("Game Over");
            canvas.gameObject.SetActive(false);
            isGameOver = false;
        }
    }

    public void EnableUI()
    {
        GlobalBlackBoard.Instance.SetVariable("IsThinking", true);
        Vignette.GetComponent<Animator>().SetBool("CanTransition", false);
        canvas.gameObject.SetActive(true);
        Vignette.gameObject.SetActive(true);
        slider.maxValue = duration;
        slider.value = slider.maxValue;
        // Vignette.CrossFadeAlpha(1, 1, false);

        coroutine = StartCoroutine(DecreaseSliderValue());
    }
    public IEnumerator DisableUI()
    {
        Vignette.GetComponent<Animator>().SetBool("CanTransition", true);
        slider.gameObject.SetActive(false);

        while (Vignette.gameObject.activeSelf)
        {
            yield return null; // Wacht één frame
        }

        yield return new WaitForEndOfFrame();
        canvas.gameObject.SetActive(false);
        slider.gameObject.SetActive(true);
    }

    private IEnumerator DecreaseSliderValue()
    {
        while (isMoving)
        {
            yield return new WaitForSeconds(stepInterval);

            if (slider.value > slider.minValue)
            {
                slider.value -= downStep;

                // Ensure the slider value does not drop below the minimum value
                if (slider.value <= slider.minValue)
                {
                    slider.value = slider.minValue;
                    isMoving = false;
                }

                // Hide the handle if the value is at the minimum value
                slider.handleRect.gameObject.SetActive(slider.value != slider.minValue);
            }
        }
    }
    public void AddToSliderValue(int valueToAdd)
    {
        StopCoroutine(coroutine);
        // Add the value to the current slider value
        slider.value = Mathf.Clamp(slider.value + valueToAdd, slider.minValue, slider.maxValue);

        // Restart the countdown if the slider is not at the minimum value
        if (slider.value > slider.minValue)
        {
            isMoving = true;
            coroutine = StartCoroutine(DecreaseSliderValue());
        }

        // Hide the handle if the value is at the minimum value
        slider.handleRect.gameObject.SetActive(slider.value != slider.minValue);
    }
}
