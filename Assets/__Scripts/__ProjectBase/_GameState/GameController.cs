using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public string entityPath = "_Prefabs/AutomataDemo/TestEnemy";

    // Start is called before the first frame update
    void Start()
    {
        //Ӧ���������濪ʼ������ֱ�ӽ���ս������
        SingletonManager.Get<GameStateMgr>().SetGameState(__GAME_STATE.AttackStage);
    }

    [Button("SpawnTestEnemy")]
    private void SpawnTestEnemy()
    {
        SingletonManager.Get<EntityManager>().LoadEnemy(entityPath);
    }

}
