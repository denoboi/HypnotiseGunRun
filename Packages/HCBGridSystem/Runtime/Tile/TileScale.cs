using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public class TileScale : MonoBehaviour
    {
        private TileBase _tile;
        private TileBase Tile => _tile == null ? _tile = GetComponentInParent<TileBase>() : _tile;

        private Renderer _renderer;
        private Renderer Renderer => _renderer == null ? _renderer = _visualBody.GetComponentInChildren<Renderer>() : _renderer;

        [SerializeField] private Transform _scaleBody;
        [SerializeField] private Transform _visualBody;

        private void Start()
        {
            UpdateScale();
        }

        //Listens Tile Editor Event   
        public void UpdateScale()
        {
            float xMultiplier = Tile.TileInitializeData.Size.x / Renderer.bounds.size.x;
            float zMultiplier = Tile.TileInitializeData.Size.y / Renderer.bounds.size.z;

            Vector3 scale = _scaleBody.localScale;
            scale.x *= xMultiplier;
            scale.z *= zMultiplier;

            _scaleBody.localScale = scale;
        }
    }
}
