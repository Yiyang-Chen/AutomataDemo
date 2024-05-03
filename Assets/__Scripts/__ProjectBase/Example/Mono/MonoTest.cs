using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 外部非mono添加更新事件的方法
/// </summary>
public class Testtest
{
    public void Update()
    {
        Debug.Log("Test");
    }

    public IEnumerator Test123()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log(123);
    }
}

public class MonoTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Testtest t = new Testtest();
        //MonoManager.GetInstance().AddUpdateListener(t.Update);
        SingletonManager.Get<MonoManager>().StartCoroutine(t.Test123());
    }

}
