using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using UnityEngine.Events;
using HCB.Core;

namespace HCB.GridSystem
{
    public abstract class SelectableBase : MonoBehaviour, ISelectable
    {
        private LeanSelectable _leanSelectable;
        public LeanSelectable LeanSelectable => _leanSelectable == null ? _leanSelectable = GetComponentInParent<LeanSelectable>() : _leanSelectable;

        private LeanPlane _leanPlane;
        public LeanPlane LeanPlane => _leanPlane == null ? _leanPlane = FindObjectOfType<SelectablePlane>().LeanPlane : _leanPlane;

        private IMovement _movement;
        protected IMovement Movement => _movement == null ? _movement = GetComponentInParent<IMovement>() : _movement;
        public virtual bool CanSelectable { get; protected set; }
        public bool IsSelected { get; protected set; }
        public Transform T => transform;

        #region Events
        private UnityEvent _onSelected = new UnityEvent();
        public UnityEvent OnSelected => _onSelected;

        private UnityEvent _onDeselected = new UnityEvent();
        public UnityEvent OnDeselected => _onDeselected;
        #endregion

        protected virtual void Awake()
        {
            Setup();
        }

        protected virtual void OnEnable()
        {
            LeanSelectable.OnSelect.AddListener(SelectionRequest);
            LeanSelectable.OnDeselect.AddListener(() => Deselect());
        }

        protected virtual void OnDisable()
        {
            LeanSelectable.OnSelect.RemoveListener(SelectionRequest);
            LeanSelectable.OnDeselect.RemoveListener(() => Deselect());
        }

        public virtual bool Select()
        {
            if (!CanSelectable || IsSelected)
                return false;

            IsSelected = true;
            SetPlaneHeight(transform.position.y);
            Movement.EnableMovement();
            OnSelected.Invoke();
            return true;
        }

        public virtual bool Deselect()
        {
            if (!IsSelected)
                return false;

            IsSelected = false;
            Movement.DisableMovement();
            OnDeselected.Invoke();
            return true;
        }

        private void Setup()
        {
            Movement.Setup(this);
            Movement.DisableMovement();
        }

        protected void SetPlaneHeight(float height)
        {
            Vector3 position = LeanPlane.transform.position;
            position.y = height;
            LeanPlane.transform.position = position;
        }

        protected virtual void SelectionRequest(LeanFinger finger)
        {
            SelectionManager.Instance.SelectionRequest(this);
        }
    }
}
