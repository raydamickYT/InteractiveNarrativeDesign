using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPersonalityMeter : MonoBehaviour
{
    public static GlobalPersonalityMeter Instance;

    protected float GoodOrBadIndex;
    public Action<float> UpdateGoodBadIndex;


    private void Start()
    {
        if (Instance == null) Instance = this;
        UpdateGoodBadIndex = UpdateGoodBad;
    }
    public void UpdateGoodBad(float Index)
    {
        GoodOrBadIndex += Index;
        Debug.Log(GoodOrBadIndex);
    }

    void OnDestroy()
    {
        UpdateGoodBadIndex = null;
    }
}
