using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public EnemyAI enemyAI;

    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {   
        player.OnInit();
        //enemyAI.OnInit();
    }
    public void OnStart()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }
    public void OnFinish()
    {
        UIManager.Ins.OpenFinish();
        GameManager.Ins.ChangeState(GameState.Finish);
    }
}
