using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] Renderer renderers;
    [SerializeField] ColorDataSO colorDataSO;
    public ColorType colorType { get; private set; }

    private bool isCollect = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isCollect == false)
        {
            Player player = other.GetComponent<Player>();
            if (player.colorType == colorType)
            {
                isCollect = true;
                player.AddCube();
                gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("Enemy") && isCollect == false)
        {
            EnemyAI enemyAI = other.GetComponent<EnemyAI>();
            if (enemyAI.colorType == colorType)
            {
                isCollect = true;
                enemyAI.AddCube();
                gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        OnInit();
    }
    private void OnInit()
    {
        isCollect = false;
        colorType = (ColorType)Random.Range(1, 4);
        ChangeColor(colorType);
    }
    public void ChangeColor(ColorType colorType)
    {
        renderers.material = colorDataSO.GetMat(colorType);
    }

    void Update()
    {
        if (isCollect)
        {
            isCollect = false;
            //Init();
        }
    }
}
