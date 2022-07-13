using System.Collections;
using System.Collections.Generic;
using System; 
using System.IO; 
using System.Text; 
using UnityEngine;
using UnityEditor;

public class MapEditor : EditorWindow
{
    //private readonly List<EditorTab> tabs = new List<EditorTab>();
    private int selectedTabIndex = -1;
    private int prevSelectedTabIndex = -1;
    private Vector2 scrollPos;
    private int currentLevel = 0;
    private void OnGUI()
    {
        selectedTabIndex = GUILayout.Toolbar(selectedTabIndex,
            new[] {"게임 셋팅", "맵 디자인"});
        draw();
    }
    
    
    [MenuItem("Tool/Sokoban/Editor", false, 0)]
    private static void Init()
    {
        var window = GetWindow(typeof(MapEditor));
        window.titleContent = new GUIContent("Sokoban Editor");
        
    }

    public void draw()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        var oldLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 90;

        GUILayout.Space(15);

        DrawMenu();

        // if (currentLevel != null)
        // {
        //     var level = currentLevel;
        //     prevWidth = level.width;
        //
        //     GUILayout.Space(15);
        //
        //     GUILayout.BeginVertical();
        //
        //     GUILayout.BeginHorizontal();
        //     GUILayout.Space(15);
        //
        //     GUILayout.BeginVertical();
        //     DrawGeneralSettings();
        //     GUILayout.Space(15);
        //     DrawInGameBoosterSettings();
        //     GUILayout.EndVertical();
        //
        //     GUILayout.Space(300);
        //
        //     GUILayout.BeginVertical();
        //     DrawGoalSettings();
        //     GUILayout.Space(15);
        //     DrawAvailableColorBlockSettings();
        //     GUILayout.EndVertical();
        //
        //     GUILayout.EndHorizontal();
        //
        //     GUILayout.EndVertical();
        //
        //     GUILayout.Space(15);
        //
        //     DrawLevelEditor();
        // }

        EditorGUIUtility.labelWidth = oldLabelWidth;
        EditorGUILayout.EndScrollView();
    }
    
    private void DrawMenu()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("New", GUILayout.Width(100), GUILayout.Height(50)))
        {
            // currentLevel = new Level();
            // currentGoal = null;
            // InitializeNewLevel();
            // CreateGoalsList();
            // CreateAvailableColorBlocksList();
        }

        if (GUILayout.Button("Open", GUILayout.Width(100), GUILayout.Height(50)))
        {
            var path = EditorUtility.OpenFilePanel("Open level", Application.dataPath + "/Resources/Maps",
                "csv");
            string filename = Path.GetFileName(path);
            
            if (!string.IsNullOrEmpty(path))
            {
                
                currentLevel = int.Parse( filename.Substring(2, filename.Length - 6));
                Debug.Log(filename);
                CSVReader.Read("Maps/Lv"+currentLevel);
            }
        }

        if (GUILayout.Button("Save", GUILayout.Width(100), GUILayout.Height(50)))
        {
            //SaveJsonFile(path + "/Levels/" + currentLevel.id + ".json", currentLevel);
            AssetDatabase.Refresh();
        }

        GUILayout.EndHorizontal();
    }
}
