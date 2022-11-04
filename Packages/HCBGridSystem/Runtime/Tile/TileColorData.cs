using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HCB.GridSystem
{
    public class TileColorData : SerializedScriptableObject
    {
        [Header("Active")]
        public Color ActiveDefaultColor;
        public Color ActiveOffsetColor;
        [Header("Deactive")]
        public Color DeactiveDefaultColor;
        public Color DeactiveOffsetColor;
        [Header("Invisable")]
        public Color InvisableDefaultColor;
        [Header("Highlight")]
        public Color ActiveHighlightColor;
        public Color DeactiveHighlightColor;
    }
}
