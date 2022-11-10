using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealthText : MonoBehaviour
{
    private Enemy _enemy;
    private Enemy Enemy => _enemy ??= GetComponentInParent<Enemy>();

    [SerializeField] TextMeshPro _healthTextMesh;

    private void OnEnable()
    {
        Enemy.OnHit.AddListener(UpdateHealthText);
    }

    private void OnDisable()
    {
        Enemy.OnHit.RemoveListener(UpdateHealthText);
    }

    public void UpdateHealthText()
    {
        _healthTextMesh.SetText(Enemy.Health.ToString());
    }

    private void OnValidate()
    {
        if (Enemy == null)
            return;

        UpdateHealthText();
    }
}
