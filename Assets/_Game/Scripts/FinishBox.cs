using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLayer Win");
            Debug.Log("PLayer Win");
            Debug.Log("PLayer Win");
            LevelManager.Ins.OnFinish();
        }
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Bot Win");
            Debug.Log("Bot Win");
            Debug.Log("Bot Win");
            LevelManager.Ins.OnFinish();
        }
    }
}
