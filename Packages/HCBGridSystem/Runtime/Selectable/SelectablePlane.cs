using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

namespace HCB.GridSystem
{
    public class SelectablePlane : MonoBehaviour
    {
        private LeanPlane _leanPlane;
        public LeanPlane LeanPlane => _leanPlane == null ? _leanPlane = GetComponentInParent<LeanPlane>() : _leanPlane;
    }
}
