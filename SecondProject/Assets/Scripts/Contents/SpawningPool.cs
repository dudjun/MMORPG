using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    int _reserveCount = 0;

    [SerializeField]
    int _keepMonsterCount = 0;

    [SerializeField]
    Vector3 _spawnPos = new Vector3(6f, 0f, -166f);
    [SerializeField]
    float _spawnRadius = 50.0f;
    [SerializeField]
    float _spawnTime = 5.0f;

    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }
    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        while (_reserveCount +_monsterCount < _keepMonsterCount)
        {
            StartCoroutine(ReserveSpawn());
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, _spawnTime));
        GameObject obj = null;
        switch(Random.Range(0, 2))
        {
            case 0:
                obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Units/Enemies/Slime");
                break;
            case 1:
                obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Units/Enemies/Pig");
                break;
        }

        Vector3 randPos;
        Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
        randDir.y = 0;
        randPos = _spawnPos + randDir;

        obj.transform.position = randPos;
        _reserveCount--;
    }
}
