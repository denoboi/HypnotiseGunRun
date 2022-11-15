using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MirrorText : MonoBehaviour
{
    private BreakableMirror _breakableMirror;
    private BreakableMirror BreakableMirror => _breakableMirror ??= GetComponent<BreakableMirror>();
        
    public int _durability = 2;
    private TextMeshPro _durabilityText;

  
    private void Start()
    {
        _durabilityText = GetComponentInChildren<TextMeshPro>();
    }
    
    
    public void SetDurability(int durability)
    {
        _durability = durability;
        _durabilityText.text = _durability.ToString();

        if (_durability <= 0)
        {
            _durabilityText.enabled = false;
        }
        
    }

}
