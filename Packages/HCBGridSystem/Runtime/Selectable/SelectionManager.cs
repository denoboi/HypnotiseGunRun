using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;

namespace HCB.GridSystem
{
    public class SelectionManager : Singleton<SelectionManager>
    {
        private ISelectable _lastSelectable = null;

        private List<Behaviour> _selectionBlockers = new List<Behaviour>();
        public List<Behaviour> SelectionBlockers { get => _selectionBlockers; private set => _selectionBlockers = value; }

        public void SelectionRequest(ISelectable selectable)
        {
            if (_lastSelectable != null && _lastSelectable.IsSelected)
            {
                _lastSelectable.Deselect();
                _lastSelectable = null;
            }

            if (!IsSelectionAvailable())
                return;

            selectable.Select();
            _lastSelectable = selectable;
        }

        public void AddSelectionBlocker(Behaviour blocker)
        {
            if (!SelectionBlockers.Contains(blocker))
            {
                SelectionBlockers.Add(blocker);
            }
        }

        public void RemoveSelectionBlocker(Behaviour blocker)
        {
            if (SelectionBlockers.Contains(blocker))
            {
                SelectionBlockers.Remove(blocker);
            }
        }

        public bool IsSelectionAvailable()
        {
            return SelectionBlockers.Count == 0;
        }
    }
}
