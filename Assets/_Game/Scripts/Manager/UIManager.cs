using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainmenuUI;
    public GameObject finishUI;
    public GameObject JoyStick;
    public GameObject movementManagerUI;
    //public GameObject winUI;
    public void OpenMainMenu()
    {
        mainmenuUI.SetActive(true);
        finishUI.SetActive(false);
        JoyStick.SetActive(false);
        movementManagerUI.SetActive(false);
        //winUI.SetActive(false);
    }
    public void OpenFinish()
    {
        mainmenuUI.SetActive(false);
        finishUI.SetActive(true);
        JoyStick.SetActive(false);
        movementManagerUI.SetActive(false);
        //winUI.SetActive(false);
    }
    public void PlayButton()
    {
        mainmenuUI.SetActive(false);
        JoyStick.SetActive(true);
        movementManagerUI.SetActive(true);
        //winUI.SetActive(false);

        LevelManager.Ins.OnStart();
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }

    public void RetryButton()
    {
        finishUI.SetActive(false);
        JoyStick.SetActive(true);
        movementManagerUI.SetActive(true);
        //LevelManager.Ins.LoadCurLevel();
        LevelManager.Ins.OnStart();
        GameManager.Ins.ChangeState(GameState.Gameplay);
        //OpenMainMenu();
    }

}
