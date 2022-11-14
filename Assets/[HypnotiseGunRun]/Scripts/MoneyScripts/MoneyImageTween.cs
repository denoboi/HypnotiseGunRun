using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoneyImageTween : MonoBehaviour
{
    private Tween _punchTween;

    private void OnEnable()
    {
        HCB.Core.EventManager.OnMoneyEarned.AddListener(MoneyPunch);
    }

    private void OnDisable()
    {
        HCB.Core.EventManager.OnMoneyEarned.RemoveListener(MoneyPunch);
    }

    void MoneyPunch()
    {
        if (_punchTween != null) //to prevent punchtween bug(it was too big on UI)
            _punchTween.Kill(true);
        _punchTween = transform.DOPunchScale(Vector3.one * 0.05f, 0.9f);
    }
    
}
