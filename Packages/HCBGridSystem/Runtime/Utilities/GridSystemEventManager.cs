using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace HCB.GridSystem
{
    public static class GridSystemEventManager
    {
        public static UnityEvent OnTileDataLoaded = new UnityEvent();
        public static UnityEvent OnGridObjectDataLoaded = new UnityEvent();
        public static UnityEvent OnGridObjectCreated = new UnityEvent();
    }
}
