using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;
using Facebook.Unity.Settings;

[assembly: RegisterValidator(typeof(FacebookSettingsValidator))]

public class FacebookSettingsValidator : RootObjectValidator<FacebookSettings>
{
    protected override void Validate(ValidationResult result)
    {
        if (string.IsNullOrEmpty(FacebookSettings.AppIds[0]))
            result.AddError("Facebook ID is not set. Please check the kick off list");
    }
}
