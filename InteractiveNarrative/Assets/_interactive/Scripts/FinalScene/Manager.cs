using System.Collections;
using System.Collections.Generic;
using BlackBoard;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    void Start()
    {
        GlobalBlackBoard.Instance.chance = 0f; //ze mogen niet overdenken tijdens deze sequence
    }
    // Update is called once per frame
    void Update()
    {
        bool End = GlobalBlackBoard.Instance.GetVariable<bool>("CanEnd");
        // Debug.Log(" " + End);
        if (End)
            StartCoroutine(StartEnd());

        bool death = GlobalBlackBoard.Instance.GetVariable<bool>("WillDie");
        // Debug.Log(death);
        if (death)
            StartCoroutine(StartDeath());
    }

    private IEnumerator StartEnd()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("EndScreen");
    }

    private IEnumerator StartDeath()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");

    }
}
