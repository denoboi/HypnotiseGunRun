using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameStep : MonoBehaviour
{
    private Renderer _renderer;
    public Renderer Renderer => _renderer ??= GetComponentInChildren<Renderer>();

    public int Multiplier { get => _multiplier; set => _multiplier = value; }
    public Color Color { get => _color; set => _color = value; }  

    [Header("Cache")]
    [ReadOnly]
    [SerializeField] private int _multiplier;
    [ReadOnly]
    [SerializeField] private Color _color;

    //EditorEvent
    [SerializeField]
    private UnityEvent OnInitialized = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnActivated = new UnityEvent();

    public void Initialize(int multiplier, Color color) 
    {
        Multiplier = multiplier;
        Color = color;
        OnInitialized.Invoke();
    }

    public void Activate() 
    {
        OnActivated.Invoke();
    }

    public Bounds GetBounds() 
    {
        return Renderer.bounds;
    }   
}
