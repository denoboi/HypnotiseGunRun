using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;

public enum Company { None, HyperboxGames, KaijuGames, Gamejon }

[InitializeOnLoad]
public class ProjectSetUpWindow : OdinEditorWindow
{
    [BoxGroup("Setup")]
    [OnValueChanged("SetCompanyName")]
    public Company CompanyName = Company.None;
    [OnValueChanged("SetCompanyName")]
    [BoxGroup("Setup")]
    public string GameName = string.Empty;
    [BoxGroup("Setup")]
    public string bundleName = "com.companyname";
    [BoxGroup("Setup")]
    public string FacebookAppID;
    [BoxGroup("Setup")]
    public string ElephantGameID;
    [BoxGroup("Setup")]
    public string ElephantGameSecret;

    [InlineEditor(InlineEditorModes.GUIOnly)]
    public PackagesData PackagesData;


#if UNITY_EDITOR

    private static IEnumerable GetAllScriptableObjects()
    {
        return UnityEditor.AssetDatabase.FindAssets("t:PackagesData")
            .Select(x => UnityEditor.AssetDatabase.GUIDToAssetPath(x))
            .Select(x => new ValueDropdownItem(x, UnityEditor.AssetDatabase.LoadAssetAtPath<PackagesData>(x)));
    }
#endif



    bool isInitilized;
    protected override void OnEnable()
    {
        base.OnEnable();
        isInitilized = EditorPrefs.GetBool("IsInitialized");
        PackagesData = (PackagesData)AssetDatabase.LoadAssetAtPath("Assets/HyperCasualBase/ProjectSetup/Data/HCBPakages.asset", typeof(PackagesData));

    }

    //ProjectSetUpWindow()
    //{
    //    if(!isInitilized)
    //        Show();
    //}

    private void SetCompanyName()
    {
        bundleName = "com." + CompanyName.ToString().ToLower() + "." + GameName.ToLower();
    }

    [MenuItem("Debug/ProjectSetup")]
    static void DebugShow()
    {
        ProjectSetUpWindow window = (ProjectSetUpWindow)GetWindow(typeof(ProjectSetUpWindow));
        window.Show();
    }
}
