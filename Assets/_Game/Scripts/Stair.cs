using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] Renderer renderers;
    [SerializeField] ColorDataSO colorDataSO;
    public ColorType colorType { get; private set; }
    private bool isCollect = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (colorType != player.colorType)
            {
                if (player.newCube.Count <= 0)
                {
                    player.isMoving = false;
                }
                else
                {
                    colorType = player.colorType;
                    ChangeColor(colorType);
                    player.RemoveCube();
                } 
            }
        }
        if (other.CompareTag("Enemy"))
        {
            EnemyAI enemyAI = other.GetComponent<EnemyAI>();
            if (colorType != enemyAI.colorType)
            {
                colorType = enemyAI.colorType;
                enemyAI.RemoveCube();
                if (enemyAI.newCube.Count > -1)
                {
                    ChangeColor(colorType);
                }
            }
        }
    }

    private void Start()
    {
        colorType = ColorType.None;
        ChangeColor(colorType);
    }
    public void ChangeColor(ColorType colorType)
    {
        renderers.material = colorDataSO.GetMat(colorType);
    }
}
