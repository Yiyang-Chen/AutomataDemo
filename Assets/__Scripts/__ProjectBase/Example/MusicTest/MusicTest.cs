using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTest : MonoBehaviour
{
    GUIStyle s;
    GUIStyle s1;

    private AudioSource sound = null;
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "��������"))
        {
            SingletonManager.Get<MusicMgr>().PlayBkMusic("Remember_Cello_Loop");
        }
        if (GUI.Button(new Rect(0, 100, 100, 100), "��ͣ����"))
            SingletonManager.Get<MusicMgr>().PauseBkMusic();
        if (GUI.Button(new Rect(0, 200, 100, 100), "ֹͣ����"))
            SingletonManager.Get<MusicMgr>().StopBkMusic();

        if (GUI.Button(new Rect(0, 300, 100, 100), "������Ч"))
            SingletonManager.Get<MusicMgr>().PlaySound("Remember_Cello_Loop", false, (s) =>
            {
                sound = s;
            });

        if (GUI.Button(new Rect(0, 400, 100, 100), "ֹͣ��Ч"))
        {
            SingletonManager.Get<MusicMgr>().StopSound(sound);
            sound = null;
        }

    }
}
