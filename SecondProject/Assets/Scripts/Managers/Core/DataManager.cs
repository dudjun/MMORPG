using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> WarriorStatDict { get; private set; } = new Dictionary<int, Data.Stat>();
    public Dictionary<int, Data.Stat> WizardStatDict { get; private set; } = new Dictionary<int, Data.Stat>();
    public void Init()
    {
        WarriorStatDict = LoadJson<Data.StatData, int, Data.Stat>("WarriorStats").MakeDict();
        WizardStatDict = LoadJson<Data.StatData, int, Data.Stat>("WizardStats").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
