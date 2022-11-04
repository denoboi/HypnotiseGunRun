using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;
using HCB;

[assembly: RegisterValidator(typeof(ExchangeTypeValidator))]

public class ExchangeTypeValidator : ValueValidator<ExchangeType>
{
    protected override void Validate(ValidationResult result)
    {
        if(this.Value.Equals(ExchangeType.Invalid))
        {
            result.AddError("ExchangeTypes can not be Invalid");
        }

    }
}
