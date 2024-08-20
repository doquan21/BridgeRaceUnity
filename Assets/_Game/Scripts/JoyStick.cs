using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public RectTransform joyStickObj;

    private void Awake()
    {   
        joyStickObj = GetComponent<RectTransform>();
    }
}
