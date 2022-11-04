using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

namespace HCB.GridSystem
{
    public class DragAlongMovement : LeanDragTranslateAlong, IMovement
    {
        public bool CanMove { get; private set; }

        protected override void Update()
        {
            if (!CanMove)
                return;

            base.Update();
        }

        public void EnableMovement()
        {
            CanMove = true;
        }

        public void DisableMovement()
        {
            CanMove = false;
        }

        public void Setup(SelectableBase selectableBase)
        {
            ScreenDepth.LeanPlane = selectableBase.LeanPlane;
        }
    }
}
