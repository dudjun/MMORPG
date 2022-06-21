using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    GameObject player;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        switch (Managers.CharacterType)
        {
            case Define.Character.Warrior:
                player = Managers.Game.Spawn(Define.WorldObject.Player, "Units/Characters/Warrior");
                break;
            case Define.Character.Wizard:
                player = Managers.Game.Spawn(Define.WorldObject.Player, "Units/Characters/Wizard");
                break;
            default:
                Debug.Log("캐릭터의 경로 혹은 타입 정의가 잘못 되었습니다");
                break;
        }
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(20);

    }

    public override void Clear()
    {

    }
}
