using System.Collections;
using System.Collections.Generic;
using BlackBoard;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToLastRoom : MonoBehaviour
{
    public void OnButtonPressed()
    {
        var t = GlobalBlackBoard.Instance.LoadLastScene();

        if (t != null)
            SceneManager.LoadScene(t);
        else
            Debug.LogWarning("no previous scene found");
    }
}
