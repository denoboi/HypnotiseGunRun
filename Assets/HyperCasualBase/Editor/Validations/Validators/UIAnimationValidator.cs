using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;
using HCB.UI;

[assembly: RegisterValidator(typeof(UIAnimationValidator))]

public class UIAnimationValidator : RootObjectValidator<GameObject>
{
    protected override void Validate(ValidationResult result)
    {
         List<IUIPanelAnimation> uiAnimation = new List<IUIPanelAnimation>(this.Object.GetComponents<IUIPanelAnimation>());


        if (uiAnimation.Count > 1)
            result.AddWarning("Multiple UI Panel Animations may conflict with eachother. Consider using only one animation");
    }
}
