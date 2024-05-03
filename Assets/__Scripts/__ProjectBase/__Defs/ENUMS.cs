
#region events
public enum __EVENTENUMS
{
    Example_MonsterDead,
    Example_JumpPressed,
    Example_UpdateProgressBar,
    Default

};
#endregion

//overall game state
#region gamestate
public enum __GAME_STATE
{
    Default,
    StartScene,
    PrepareStage,
    AttackStage
};
#endregion

#region input
public enum __INPUTSTATE
{
    Default,
    Pause
}
#endregion

#region UI
public enum __UI_Layer
{
    Bot,
    Mid,
    Top,
    Popup,
    System,
    Dialogue
}

public enum __PANELS
{
    ExamplePanel,
}
#endregion

#region data
// streaming Ҫ����build��Ķ����� persistent ������̨�����ϵ�һЩ�ļ� temporary ��ʱcache DataPath editor��runtime���᲻һ��
public enum __DataPath { 
    Default,
    Streaming, 
    Persistent, 
    Temporary
};
#endregion

#region AttackDirection
[System.Flags]
public enum __ATTACKDIRECTION
{
    None = 0,
    Up = 1,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
    UpLeft = 1 << 4,
    DownLeft = 1 << 5,
    UpRight = 1 << 6,
    DownRight = 1 << 7
}
#endregion

public enum __EBulletType
{
    Enemy,
    Player
}