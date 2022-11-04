using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HCB.GridSystem
{
    public class GridEditorIconData : SerializedScriptableObject
    {
        public Dictionary<string, Texture2D> PreviewImages = new Dictionary<string, Texture2D>();

    }

    public static class GridEditorIconIDHolder
    {
        public static string InvisableTile = "InvisableTile";
        public static string UnuseableTile = "UnuseableTile";
        public static string AvailableTile = "AvailableTile";
    }
}
