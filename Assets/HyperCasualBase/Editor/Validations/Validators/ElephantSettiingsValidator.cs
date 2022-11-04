using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;

[assembly: RegisterValidator(typeof(ElephantSettiingsValidator))]

public class ElephantSettiingsValidator : RootObjectValidator<ElephantSettings>
{
    protected override void Validate(ValidationResult result)
    {
        if (string.Equals(PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS), "com.hyperboxgames.hypercasualbase"))
            result.AddError("Bundle Identifier is not set.Please check the kick off list");

        if (string.IsNullOrEmpty(this.Object.GameID))
            result.AddError("Elephant Game ID is not set. Please check the kick off list");

        if (string.IsNullOrEmpty(this.Object.GameSecret))
            result.AddError("Elephant Game Secret is not set. Please check the kick off list");

        if (this.Object.GameID.Contains(" "))
            result.AddError("Elephant Game ID Contains Space. This will result in a faulty build. Please Check the Game ID");

        if (this.Object.GameSecret.Contains(" "))
            result.AddError("Elephant Game Secret Contains Space. This will result in a faulty build. Please Check the Game ID");
    }
}
