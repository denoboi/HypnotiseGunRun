using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Facebook.Unity.Settings;
using System.Linq;

public enum Company { None, HyperboxGames, KaijuGames, Gamejon }

[InitializeOnLoad]
public class ProjectSetUpWindow : OdinEditorWindow
{
    [BoxGroup("Setup")]
    [OnValueChanged("SetUpKickOffList")]
    public Company CompanyName = Company.None;
    [OnValueChanged("SetUpKickOffList")]
    [BoxGroup("Setup")]
    public string GameName = string.Empty;
    [BoxGroup("Setup")]
    [OnValueChanged("SetUpKickOffList")]
    public string bundleID = "com.companyname";
    [BoxGroup("Setup")]
    [OnValueChanged("SetUpKickOffList")]
    public string FacebookAppID;
    [OnValueChanged("SetUpKickOffList")]
    [BoxGroup("Setup")]
    [OnValueChanged("SetUpKickOffList")]
    public string ElephantGameID;
    [OnValueChanged("SetUpKickOffList")]
    [BoxGroup("Setup")]
    public string ElephantGameSecret;

    [InlineEditor(InlineEditorModes.GUIOnly)]
    public PackagesData PackagesData;

    public ElephantSettings ElephantSettings { get { return
                (ElephantSettings)AssetDatabase.LoadAssetAtPath("Assets/Resources/ElephantSettings.asset", typeof(ElephantSettings));
        } }

    bool isInitilized;
    protected override void OnEnable()
    {
        base.OnEnable();
        isInitilized = EditorPrefs.GetBool("IsInitialized");
        PackagesData = (PackagesData)AssetDatabase.LoadAssetAtPath("Assets/HyperCasualBase/ProjectSetup/Data/HCBPakages.asset", typeof(PackagesData));
        saveChangesMessage = "There are empty fields. Please fill them";
    }

    private void OnFocus()
    {
        LoadInfo();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    ProjectSetUpWindow()
    {
        if (EditorPrefs.GetBool("isSet"))
            return;

        if (!isInitilized)
            Show();
    }

    private void SetUpKickOffList()
    {
        bundleID = "com." + CompanyName.ToString().ToLower() + "." + GameName.ToLower();
        bundleID = bundleID.Replace(" ", "");
        PlayerSettings.companyName = CompanyName.ToString();
        PlayerSettings.productName = GameName;
        ElephantSettings.GameID = ElephantGameID;
        ElephantSettings.GameSecret = ElephantGameSecret;
        if(!string.IsNullOrEmpty(FacebookAppID))
            FacebookSettings.AppIds[0] = FacebookAppID;

        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, bundleID);
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, bundleID);

        hasUnsavedChanges = CanClose();

        EditorPrefs.SetBool("isSet", !CanClose());
    }

    private bool CanClose()
    {
        if (CompanyName == Company.None)
            return true;

        if (string.IsNullOrEmpty(GameName))
            return true;

        if (string.IsNullOrEmpty(FacebookAppID))
            return true;

        if (string.IsNullOrEmpty(ElephantGameID))
            return true;

        if (string.IsNullOrEmpty(ElephantGameSecret))
            return true;


        return false;
    }

    private void SaveInfo()
    {
        EditorPrefs.SetInt("compName", (int)CompanyName);
        EditorPrefs.SetString("gameName", GameName);
        EditorPrefs.SetString("bundleId", bundleID);
        EditorPrefs.SetString("facebookId", FacebookAppID);
        EditorPrefs.SetString("eGameId", ElephantGameID);
        EditorPrefs.SetString("eGameSecret", ElephantGameSecret);
    }

    private void LoadInfo()
    {
        CompanyName = (Company)EditorPrefs.GetInt("compName", 0);
        GameName = EditorPrefs.GetString("gameName", "ExampleGame");
        bundleID = EditorPrefs.GetString("bundleId", "com." + CompanyName.ToString().ToLower() + "." + GameName.ToLower());
        FacebookAppID = EditorPrefs.GetString("facebookId", FacebookAppID);
        ElephantGameID = EditorPrefs.GetString("eGameId", ElephantGameID);
        ElephantGameSecret = EditorPrefs.GetString("eGameSecret", ElephantGameSecret);
    }

    public override void SaveChanges()
    {
        base.SaveChanges();
        SaveInfo();
    }

    [MenuItem("Debug/ProjectSetup")]
    static void DebugShow()
    {
        ProjectSetUpWindow window = (ProjectSetUpWindow)GetWindow(typeof(ProjectSetUpWindow));
        window.Show();
    }
}
