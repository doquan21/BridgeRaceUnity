using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrick : MonoBehaviour
{
    [SerializeField] Renderer renderers;
    [SerializeField] ColorDataSO colorDataSO;
    public ColorType colorType { get; private set; }
    private bool isCollect = false;
    
    private void Start()
    {
        ChangeColor(ColorType.Blue);
    }
    public void ChangeColor(ColorType colorType)
    {
        renderers.material = colorDataSO.GetMat(colorType);
    }
}
