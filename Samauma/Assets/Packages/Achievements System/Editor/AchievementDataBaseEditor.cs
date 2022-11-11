using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(AchievementDataBase))]
public class AchievementDataBaseEditor : Editor
{
    AchievementDataBase database;

    void OnEnable()
    {
        database = target as AchievementDataBase;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Gerar Enumerador de Conquistas", GUILayout.Height(30)))
        {
            GenerateEnum();
        }
    }

    void GenerateEnum()
    {
        string filePath = Path.Combine(Application.dataPath, "Achievements.cs");
        string code = "public enum Achievements { ";

        foreach (Achievement achievement in database.achievements)
        {
            code += achievement.id + ", ";
        }

        code += " }";
        File.WriteAllText(filePath, code);
        AssetDatabase.ImportAsset("Assets/Achievements.cs");
    }
}