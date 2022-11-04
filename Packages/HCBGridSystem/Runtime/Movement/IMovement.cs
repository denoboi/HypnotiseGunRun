using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public interface IMovement
    {
        void Setup(SelectableBase selectableBase);
        void EnableMovement();
        void DisableMovement();
    }
}
