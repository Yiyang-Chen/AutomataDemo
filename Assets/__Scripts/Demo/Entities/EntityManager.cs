using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ManagedSingleton]
public class EntityManager : Singleton
{
    public Player mainPlayer;
    private HashSet<Enemy> enemies;

    public override void Init()
    {
        enemies = new();
    }

    public override void OnReload()
    {
        enemies.Clear();
    }

    public void LoadEntities()
    {
        LoadSceneObjs();
        LoadPlayer();
    }

    private void LoadSceneObjs()
    {
        SingletonManager.Get<ResourceMgr>().Load<GameObject>("_Prefabs/AutomataDemo/BackGround");
    }

    private void LoadPlayer()
    {
        GameObject player = SingletonManager.Get<ResourceMgr>().Load<GameObject>("_Prefabs/AutomataDemo/Player");
        player.transform.position = Vector3.zero;
        mainPlayer = player.GetComponent<Player>();
    }

    public void LoadEnemy(string path)
    {
        SingletonManager.Get<PoolMgr>().GetObj(path, (enemy)=> 
        {
            enemy.transform.position = GetRandomSpawnPos();
            enemy.GetComponent<Enemy>().OnSpawned(100, 100);
            enemies.Add(enemy.GetComponent<Enemy>());
        });
    }

    private Vector2 GetRandomSpawnPos()
    {
        Vector3 playerPos = mainPlayer.transform.position;
        Vector2 randomPos = GetRandomPosInScreen();
        if(Vector3.Distance(playerPos, randomPos) < 2f)
        {
            randomPos = GetRandomSpawnPos();
        }

        return randomPos;
    }

    private Vector2 GetRandomPosInScreen()
    {
        return new Vector2(Random.Range(-1f, 1f) * Camera.main.orthographicSize * Camera.main.aspect,
                                            Random.Range(-1f, 1f) * Camera.main.orthographicSize); ;
    }

    public void EnemyDead(Enemy deadE)
    {
        //TODO: do something when enemy dead
        if (enemies.Contains(deadE))
        {
            enemies.Remove(deadE);
        }
        deadE.OnPooled();
        SingletonManager.Get<PoolMgr>().PushObj(deadE.prefabPath ,deadE.gameObject);
    }

    public void PlayerDead()
    {
        //TODO: do something when player dead
    }
}
