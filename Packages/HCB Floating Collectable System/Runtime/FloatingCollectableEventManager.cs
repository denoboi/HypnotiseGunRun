using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace HCB.FloatingCollectableSystem
{
    public static class FloatingCollectableEventManager
    {
        public static FloatingCollectableEvent OnFloatingCollectableCollected = new FloatingCollectableEvent();
    }

    public class FloatingCollectableEvent : UnityEvent<Vector3, Action> { }
}
