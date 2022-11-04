using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public class GridObjectGizmos : MonoBehaviour
    {
        private GridObject _gridObject;
        private GridObject GridObject => _gridObject == null ? _gridObject = GetComponentInParent<GridObject>() : _gridObject;

        [Tooltip("Gizmos sphere draw padding.")]
        [SerializeField] private float _padding;

        private const float GIZMOS_RADIUS = 0.2f;       

        private void OnDrawGizmos()
        {
            if (GridObject.PivotPoint == null)
                return;

            Gizmos.color = Color.green;            
            foreach (var gridPoint in GridObject.GridPoints)
            {
                Vector3 position = GridObject.PivotPoint.position + new Vector3(gridPoint.x + _padding * gridPoint.x, 0f, -1f* (gridPoint.y + _padding * gridPoint.y));
                Gizmos.DrawSphere(position, GIZMOS_RADIUS);
            }
        }
    }
}
