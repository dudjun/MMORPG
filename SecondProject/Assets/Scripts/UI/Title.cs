using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "Game";

    [SerializeField]
    private GameObject BlackFade;

    public static Title instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    [SerializeField]
    private SaveNLoad saveNLoad;
    
    public void ClickStart()
    {
        Util.FindChild(gameObject, "CharacterChoose").SetActive(true);
    }

    public void ClickLoad()
    {
        if (saveNLoad.isExist())
        {
            Debug.Log("로드");
            saveNLoad.LoadCharacterType();
            StartCoroutine(LoadCoroutine());
        }
        else
        {
            Debug.Log("세이브 파일이 없습니다");
        }
    }

    IEnumerator LoadCoroutine()
    {
        BlackFade.SetActive(true);
        yield return new WaitForSeconds(1f);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            // operation.progress를 이용해서 로딩화면 만들 예정
            yield return null;
        }
        saveNLoad = FindObjectOfType<SaveNLoad>();
        saveNLoad.LoadAllData();
        BlackFade.SetActive(false);
        gameObject.SetActive(false);
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
            StartCoroutine(FadeAndLoadScene());
        }
        else if (i == 1)
        {
            Managers.CharacterType = Define.Character.Wizard;
            StartCoroutine(FadeAndLoadScene());
        }
        else
            return;
    }

    IEnumerator FadeAndLoadScene()
    {
        BlackFade.SetActive(true);
        yield return new WaitForSeconds(1f);
        BlackFade.SetActive(false);
        SceneManager.LoadScene(sceneName);
        gameObject.SetActive(false);
    }

}
