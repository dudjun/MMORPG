using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject Pause_Child;
    [SerializeField]
    private SaveNLoad theSaveNLoad;

    private void Start()
    {
        Managers.Input.KeyAction -= CheckExc;
        Managers.Input.KeyAction += CheckExc;
    }

    private void Update()
    {
        if (Pause_Child.IsValid()) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    void CheckExc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause_Child.SetActive(true);
        }
    }

    public void ContinueBtn() 
    {
        Pause_Child.SetActive(false);
    }

    public void ExitBtn()
    {
        theSaveNLoad.SaveData();
        Application.Quit();
    }
}
