using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrick : MonoBehaviour
{
    [SerializeField] Renderer renderers;
    [SerializeField] ColorDataSO colorDataSO;
    [SerializeField] GameObject enemyGO;
    public ColorType colorType { get; private set; }
    private bool isCollect = false;
    private EnemyAI enemyAI;
    
    private void Start()
    {
        enemyAI = enemyGO.GetComponent<EnemyAI>();
        colorType = enemyAI.colorType;
        Debug.Log(colorType);
        ChangeColor(colorType);
    }
    public void ChangeColor(ColorType colorType)
    {
        renderers.material = colorDataSO.GetMat(colorType);
    }
}
