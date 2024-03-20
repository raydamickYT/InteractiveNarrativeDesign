using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    // public SceneNode NextNodeBad, NextNodeGood, NextNeutralNode;
    // public GameObject ScenePrefab;
    public Scenes NeutralScenes, GoodScenes, BadScenes;
    public int SceneIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
