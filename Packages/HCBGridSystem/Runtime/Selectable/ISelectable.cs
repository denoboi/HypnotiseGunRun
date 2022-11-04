using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HCB.GridSystem
{
    public interface ISelectable
    {
        Transform T { get; }
        bool IsSelected { get; }
        UnityEvent OnSelected { get; }
        UnityEvent OnDeselected { get; }
        bool Select();
        bool Deselect();
    }
}
