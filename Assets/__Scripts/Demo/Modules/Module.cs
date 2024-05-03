using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modules/Module")]
public class Module : ScriptableObject
{
    public string moduleName;
    public Texture icon;
    public int exctueTick;
    public float memoryPrencentage;

    public ModuleBuffBase normalBuff;
    public ModuleBuffBase sideBuff;
}
