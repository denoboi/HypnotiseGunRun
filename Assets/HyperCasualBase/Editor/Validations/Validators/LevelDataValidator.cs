using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using HCB;

[assembly: RegisterValidator(typeof(LevelDataValidator<>))]

public class LevelDataValidator<T> : ValueValidator<T>
    where T : LevelData
{
    protected override void Validate(ValidationResult result)
    {
        foreach (var level in this.Value.Levels)
        {
            if (string.IsNullOrEmpty(level.LoadLevelID))
                result.AddError("Level Id can not be empty please select a level at Level Data Asset");


            bool isNotABuildSettingScene = false;
            for (int i = 0; i < EditorSceneManager.sceneCountInBuildSettings; i++)
            {
                if (UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i).Contains(level.LoadLevelID))
                    isNotABuildSettingScene = true;
            }


            if (!isNotABuildSettingScene)
                result.AddError("Level " + level.LoadLevelID + " is not added to the build settings");
        }
    }
}
