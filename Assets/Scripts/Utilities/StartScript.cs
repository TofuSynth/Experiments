using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class StartCorrectScene
{
    static StartCorrectScene()
    {
        var startScene = EditorBuildSettings.scenes[0].path;
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(startScene);
        EditorSceneManager.playModeStartScene = sceneAsset;
    }
}
