using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;

public class CustomMenuMgr
{
    //MenuItems

    #region SampleCodes
    //YarnSpinner
    /*[MenuItem("GameObject/YarnSpinner/YarnCollider")]
    public static void CreateYarnCollider(MenuCommand menuC)
    {
        CreatePrefab("_Prefabs/YarnSpinner/YarnCollider");
    }*/
    #endregion

    #region CreateItem
    //codes to create item
    public static void CreatePrefab(string path)
    {
        GameObject newObj = PrefabUtility.InstantiatePrefab(Resources.Load(path)) as GameObject;
        Place(newObj);
    }

    public static void CreateObj(string name, params Type[] types)
    {
        GameObject newObj = ObjectFactory.CreateGameObject(name, types);
        Place(newObj);
    }

    public static void Place(GameObject obj)
    {
        //Find Location
        SceneView lastView = SceneView.lastActiveSceneView;
        obj.transform.position = lastView ? lastView.pivot : Vector3.zero;
        if (Selection.activeGameObject) 
            obj.transform.SetParent(Selection.activeGameObject.transform);
        obj.transform.rotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
        //Make sure proper scene and proper name
        StageUtility.PlaceGameObjectInCurrentStage(obj);
        GameObjectUtility.EnsureUniqueNameForSibling(obj);

        //Record undo, and select
        Undo.RegisterCreatedObjectUndo(obj, $"Create Object: {obj.name}");
        Selection.activeGameObject = obj;

        //For prefabs, mark the scene as dirty to save
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
    #endregion
}
