using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HCB.GridSystem
{
    public class MouseEventDetector : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent OnMouseDowned = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnMouseUpped = new UnityEvent();

        private void OnMouseDown()
        {
            OnMouseDowned.Invoke();
        }

        private void OnMouseUp()
        {
            OnMouseUpped.Invoke();
        }
    }
}
