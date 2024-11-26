#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class BYDataMaker
{
    [MenuItem("Assets/BY/Create Binary File for Tab Delimeter (.txt)", false, 1)]
    private static void CreateBinaryFile()
    {
        foreach (UnityEngine.Object obj in Selection.objects)
        {
            TextAsset csvFile = (TextAsset)obj;
            string tablename = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(csvFile));
            Debug.Log("name : " + tablename);

            ScriptableObject scriptableObject = ScriptableObject.CreateInstance(tablename);
            if (scriptableObject == null)
                return;

            AssetDatabase.CreateAsset(scriptableObject, "Assets/Resources/DataTable/" + tablename + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            //
            BYDataBase by = (BYDataBase)scriptableObject;
            by.CreateBinaryFile(csvFile);
            EditorUtility.SetDirty(by);
        }
    }
}
#endif