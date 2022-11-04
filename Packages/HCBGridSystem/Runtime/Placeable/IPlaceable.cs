using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public interface IPlaceable
    {
        Transform T { get; }
        bool IsActive { get; }
        bool IsPlaced { get; }
    }
}
