using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[ManagedSingleton]
public class DataMgr : Singleton
{
    
    /// <summary>
    /// 根据mode选择路径
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
    /// 存储文件
    /// </summary>
    /// <typeparam name="T">data的class</typeparam>
    /// <param name="saveData">要保存的data，这个class中只允许有string,bool,int,float</param>
    /// <param name="mode">DataPath.xx</param>
    /// <param name="path">string,"/xx/xx.xx"，最后要指向一个flie</param>
    public void Save<T>(T saveData, __DataPath mode, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string finalPath = pathMode(mode) + path;
        //用using防止file出错
        using (FileStream stream = new FileStream(finalPath, FileMode.Create))
        {
            formatter.Serialize(stream, saveData);
            stream.Close();
        }
    }

    /// <summary>
    /// 读档文件
    /// </summary>
    /// <typeparam name="T">data的class</typeparam>
    /// <param name="mode">DataPath.xx</param>
    /// <param name="path">string,"/xx/xx.xx"，最后要指向一个flie</param>
    /// <returns>返回data，如果没有取到，就是null</returns>
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
    /// 删除某个保存的文件
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
