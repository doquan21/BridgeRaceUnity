using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ColorType
{
    None = 0,
    Red = 1,
    Green = 2,
    Blue = 3
}
[CreateAssetMenu(menuName = "ColorDataSO")]
public class ColorDataSO : ScriptableObject
{
    [SerializeField] Material[] materials;
    public Material GetMat(ColorType color)
    {
        return materials[(int)color];
    }
}
