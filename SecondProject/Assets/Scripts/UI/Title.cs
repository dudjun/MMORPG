using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "Game";
    
    public void ClickStart()
    {
        Util.FindChild(gameObject, "CharacterChoose").SetActive(true);
    }

    public void ClickLoad()
    {
        Debug.Log("로드");
    }

    public void ClickExit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    public void ClickCharacter(int i)
    {
        if (i == 0)
        {
            Managers.CharacterType = Define.Character.Warrior;
            SceneManager.LoadScene(sceneName);
        }
        else if (i == 1)
        {
            Managers.CharacterType = Define.Character.Wizard;
            SceneManager.LoadScene(sceneName);
        }
        else
            return;
    }
}
