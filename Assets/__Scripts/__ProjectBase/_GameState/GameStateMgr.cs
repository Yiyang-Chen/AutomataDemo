
using UnityEngine;

[ManagedSingleton]
public class GameStateMgr : Singleton
{
    public float remainedTime = 0;
    //enum for Game states
    __GAME_STATE currentGameState = __GAME_STATE.Default;

    GameController controller;

    public GameStateMgr()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    //Set game state to a state
    public void SetGameState(__GAME_STATE s)
    {
        if(currentGameState == s)
        {
            return;
        }
        switch (currentGameState)
        {
            case __GAME_STATE.StartScene:
                OnQuitStart();
                break;
            case __GAME_STATE.PrepareStage:
                OnQuitPrepare();
                break;
            case __GAME_STATE.AttackStage:
                OnQuitAttack();
                break;
            default:
                break;
        }
        switch (s)
        {
            case __GAME_STATE.StartScene:
                OnSwitchToStart();
                break;
            case __GAME_STATE.PrepareStage:
                OnSwitchToPrepare();
                break;
            case __GAME_STATE.AttackStage:
                OnSwitchToAttack();
                break;
            default:
                Debug.LogError($"Unusual state {currentGameState}");
                break;
        }
        currentGameState = s;
    }

    //Get current game state
    public __GAME_STATE GetGameState()
    {
        return currentGameState;
    }

    //根据关卡不同需要load不同的敌人和剩余时间
    private void OnSwitchToAttack()
    {
        SingletonManager.Get<EntityManager>().LoadEntities();
        remainedTime = 100f;
    }

    private void OnQuitAttack()
    {

    }

    private void OnSwitchToPrepare()
    {

    }

    private void OnQuitPrepare()
    {

    }

    private void OnSwitchToStart()
    {

    }

    private void OnQuitStart()
    {

    }
}
