using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Define.Character CharacterType;
    public Vector3 playerPos;
    public Vector3 playerRot;

    public int playerExp;
    public int playerHp;
    public int playerMp;
    public int playerGold;


    public List<string> invenItemName = new List<string>();
    public List<int> invenArrayNumber = new List<int>();
    public List<int> invenItemNumber = new List<int>();

    public List<Vector3> monstersPos = new List<Vector3>();
    public List<string> monstersName = new List<string>();

    public bool isCheckQuest;
    public bool isCheckSuccess;
    public bool isOnQuestState;
}

public class SaveNLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private GameObject thePlayer;
    private Inventory theInven;
    private NPCController theNpc;
    private GameObject theQuestState;

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void SaveData()
    {
        saveData.invenArrayNumber.Clear();
        saveData.invenItemName.Clear();
        saveData.invenItemNumber.Clear();
        saveData.monstersName.Clear();
        saveData.monstersPos.Clear();

        thePlayer = Managers.Game.GetPlayer();
        PlayerStat stat = thePlayer.GetComponent<PlayerStat>();
        theInven = FindObjectOfType<Inventory>();
        theNpc = FindObjectOfType<NPCController>();
        theQuestState = GameObject.Find("StateBox").transform.Find("QuestState").gameObject;

        saveData.playerPos = thePlayer.transform.position;
        saveData.playerRot = thePlayer.transform.eulerAngles;
        saveData.CharacterType = Managers.CharacterType;

        saveData.playerExp = stat.Exp;
        saveData.playerHp = stat.Hp;
        saveData.playerMp = stat.Mp;
        saveData.playerGold = stat.Gold;

        Slot[] slots = theInven.GetSlots();
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }

        List<GameObject> monsters = Managers.Game.GetMonsters();
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i] != null)
            {
                if (monsters[i].name == "Pig(Clone)")
                    saveData.monstersName.Add("Units/Enemies/Pig");
                else if (monsters[i].name == "Slime(Clone)")
                    saveData.monstersName.Add("Units/Enemies/Slime");
                saveData.monstersPos.Add(monsters[i].transform.position);
            }
        }

        saveData.isCheckQuest = theNpc.isCheckQuest;
        saveData.isCheckSuccess = theNpc.isCheckSuccess;
        saveData.isOnQuestState = theQuestState.IsValid();

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadAllData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            thePlayer = Managers.Game.GetPlayer();
            PlayerStat stat = thePlayer.GetComponent<PlayerStat>();
            theInven = FindObjectOfType<Inventory>();
            theNpc = FindObjectOfType<NPCController>();
            theQuestState = GameObject.Find("StateBox").transform.Find("QuestState").gameObject;

            thePlayer.transform.position = saveData.playerPos;
            thePlayer.transform.eulerAngles = saveData.playerRot;

            stat.Exp = saveData.playerExp;
            stat.Hp = saveData.playerHp;
            stat.Mp = saveData.playerMp;
            stat.Gold = saveData.playerGold;

            for (int i = 0; i < saveData.invenItemName.Count; i++)
            {
                theInven.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemNumber[i]);
            }
            for (int i = 0; i < saveData.monstersName.Count; i++)
            {
                GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, saveData.monstersName[i]);
                obj.transform.position = saveData.monstersPos[i];
            }

            theNpc.isCheckQuest = saveData.isCheckQuest;
            theNpc.isCheckSuccess = saveData.isCheckSuccess;
            if (saveData.isOnQuestState)
                theQuestState.GetComponent<Quest>().QuestOn();
            else
                theQuestState.SetActive(saveData.isOnQuestState);
        }
        else
        {
            Debug.Log("세이브 파일이 없습니다");
        }
    }

    public void LoadCharacterType()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            Managers.CharacterType = saveData.CharacterType;
        }
        else
        {
            Debug.Log("세이브 파일이 없습니다");
        }
    }

    public bool isExist()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
            return true;
        else
            return false;
    }
}
