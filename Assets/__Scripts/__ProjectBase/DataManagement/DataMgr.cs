using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[ManagedSingleton]
public class DataMgr : Singleton
{
    
    /// <summary>
    /// ����modeѡ��·��
    /// </summary>
    private string pathMode(__DataPath mode)
    {
        string _path = "";

        switch (mode)
        {
            case __DataPath.Streaming:
                _path += Application.streamingAssetsPath;
                break;
            case __DataPath.Persistent:
                _path += Application.persistentDataPath;
                break;
            case __DataPath.Temporary:
                _path += Application.temporaryCachePath;
                break;
            default:
                _path += Application.dataPath;
                break;
        }
        return _path;
    }

    /// <summary>
    /// �洢�ļ�
    /// </summary>
    /// <typeparam name="T">data��class</typeparam>
    /// <param name="saveData">Ҫ�����data�����class��ֻ������string,bool,int,float</param>
    /// <param name="mode">DataPath.xx</param>
    /// <param name="path">string,"/xx/xx.xx"�����Ҫָ��һ��flie</param>
    public void Save<T>(T saveData, __DataPath mode, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string finalPath = pathMode(mode) + path;
        //��using��ֹfile����
        using (FileStream stream = new FileStream(finalPath, FileMode.Create))
        {
            formatter.Serialize(stream, saveData);
            stream.Close();
        }
    }

    /// <summary>
    /// �����ļ�
    /// </summary>
    /// <typeparam name="T">data��class</typeparam>
    /// <param name="mode">DataPath.xx</param>
    /// <param name="path">string,"/xx/xx.xx"�����Ҫָ��һ��flie</param>
    /// <returns>����data�����û��ȡ��������null</returns>
    public T Load<T>(__DataPath mode, string path) where T : class
    {
        T data = null;

        string finalPath = pathMode(mode) + path;
        Debug.Log(finalPath);

        if (File.Exists(finalPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            using (FileStream stream = new FileStream(finalPath, FileMode.Open))
            {
                data = formatter.Deserialize(stream) as T;
                stream.Close();
            }
        }

        return data;
    }

    /// <summary>
    /// ɾ��ĳ��������ļ�
    /// </summary>
    /// <param name="mode">DataPath.xx</param>
    /// <param name="path"></param>
    public void Delete(__DataPath mode, string path)
    {
        string _path = pathMode(mode);

        _path += path;

        if (File.Exists(_path))
        {
            File.Delete(_path);
        }
    }
}
