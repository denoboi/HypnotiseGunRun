using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public class TileGizmos : MonoBehaviour
    {
        private TileBase _tile;
        private TileBase Tile => _tile == null ? _tile = GetComponentInParent<TileBase>() : _tile;

        private const float ALPHA = 0.5f;
        private const float SIZE = 0.3f;
        private void OnDrawGizmos()
        {
            Color color = Tile.TileInitializeData.TileType == TileType.Available ? Color.green : Color.red;
            color.a = ALPHA;

            Gizmos.color = color;
            Gizmos.DrawCube(transform.position + Vector3.up * SIZE / 2f, SIZE * Vector3.one);
        }
    }
}
