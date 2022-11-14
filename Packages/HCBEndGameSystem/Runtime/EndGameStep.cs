using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace HCB.EndGameSystem
{
    public class EndGameStep : MonoBehaviour
    {
        private Renderer _renderer;
        public Renderer Renderer => _renderer ??= GetComponentInChildren<Renderer>();

        public bool IsInitialized { get => _isInitialized; private set => _isInitialized = value; }
        public int Multiplier { get => _multiplier; private set => _multiplier = value; }
        public Color Color { get => _color; private set => _color = value; }

        [Header("Cache")]
        [ReadOnly]
        [SerializeField] private int _multiplier;
        [ReadOnly]
        [SerializeField] private Color _color;
        [ReadOnly]
        [SerializeField] private bool _isInitialized;

        //EditorEvent
        [SerializeField]
        private UnityEvent OnInitialized = new UnityEvent();

        public void Initialize(int multiplier, Color color)
        {
            IsInitialized = true;
            Multiplier = multiplier;
            Color = color;
            OnInitialized.Invoke();
        }

        public Bounds GetBounds()
        {
            return Renderer.bounds;
        }
    }
}
