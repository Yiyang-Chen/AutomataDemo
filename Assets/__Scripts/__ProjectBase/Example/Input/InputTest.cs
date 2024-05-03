using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SingletonManager.Get<EventCenter>().RemoveEventListener(__EVENTENUMS.Example_JumpPressed, CheckInputDown);
        SingletonManager.Get<EventCenter>().AddEventListener(__EVENTENUMS.Example_JumpPressed, CheckInputDown);
    }

    // Update is called once per frame
   private void CheckInputDown()
    {
        Debug.Log("jump down");
    }
}
