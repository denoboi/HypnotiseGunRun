using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector; 
using HCB.Core; 

namespace HCB.SkinSystem
{
    public enum ControllerType
    {
        Player,
        AI
    }
    public abstract class SkinController : MonoBehaviour
    {
        public ControllerType ControllerType;
        [SerializeField] protected SkinType skinType;

        protected GameObject currentSkin;

        protected virtual void Awake()
        {
            //Try to get latest equipped skin data, if exists equip it else equip the default one
            if (ControllerType == ControllerType.Player)
                ActivateSkin(SkinManager.Instance.GetCurrentSkin(skinType));
            else
                ActivateSkin(SkinManager.Instance.GetRandomSkin(skinType));
        }

        protected virtual void OnEnable()
        {
            if (Managers.Instance == null) return;

            SkinManager.Instance.OnSkinActivated.AddListener(ActivateSkin);
        }

        protected virtual void OnDisable()
        {
            if (Managers.Instance == null) return;

            SkinManager.Instance.OnSkinActivated.RemoveListener(ActivateSkin);
        }

        [Button]
        protected abstract void ActivateSkin(SkinItemData skinData);
    }
}
