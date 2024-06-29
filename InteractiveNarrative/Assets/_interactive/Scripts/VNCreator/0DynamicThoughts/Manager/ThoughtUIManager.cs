using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ThoughtUIManager : MonoBehaviour
{
    public Slider slider;
    public Canvas canvas;

    public int downStep = 1; // Step size at which the slider value decreases
    public float stepInterval = 0.5f; // Time interval between each step in seconds
    private bool isGameOver = false;
    private Coroutine countdownCoroutine;

    private void Start()
    {
        slider.value = slider.maxValue; // Start the slider at the maximum value
        EnableUI();
    }

    private void Update()
    {
        AddToSliderValue(1);
    }

    private IEnumerator DecreaseSliderValue()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(stepInterval);

            if (slider.value > slider.minValue)
            {
                slider.value -= downStep;

                // Ensure the slider value does not drop below the minimum value
                if (slider.value <= slider.minValue)
                {
                    slider.value = slider.minValue;
                    isGameOver = true;
                    Debug.Log("Game Over");
                }
            }

            // Hide the handle if the value is at the minimum value
            slider.handleRect.gameObject.SetActive(slider.value != slider.minValue);
        }
    }
    public void EnableUI()
    {
        slider.value = slider.maxValue; // Start the slider at the maximum value
        countdownCoroutine = StartCoroutine(DecreaseSliderValue());
        canvas.gameObject.SetActive(true);
    }

    public void AddToSliderValue(int valueToAdd)
    {
        // Stop the current countdown coroutine if active
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        // Add the value to the current slider value
        slider.value = Mathf.Clamp(slider.value + valueToAdd, slider.minValue, slider.maxValue);

        // Restart the countdown if the game is not over
        if (!isGameOver)
        {
            countdownCoroutine = StartCoroutine(DecreaseSliderValue());
        }
    }

    private void OnDisable()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        isGameOver = false;
    }
}
