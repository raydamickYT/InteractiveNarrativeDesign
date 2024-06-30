using System.Collections;
using System.Collections.Generic;
using BlackBoard;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mouse;

public class Restart : MonoBehaviour
{
    public void Reset()
    {
        GlobalBlackBoard.Instance.Cleanup(); //alle lijsten verwijderen en actions legen.
        SceneManager.LoadScene("Voordeur");
        PointerController.Instance.MouseInputEnabled = true;
    }
}
