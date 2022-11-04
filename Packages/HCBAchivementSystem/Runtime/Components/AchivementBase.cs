using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

namespace HCB.AchivementSystem
{
    public class AchivementBase : MonoBehaviour, IAchivement
    {
        [SerializeField]
        [ValueDropdown("achivementIDs")]
        private string achivementID;
        public string AchivementID => achivementID;

        private AchivementModel achivementModel;
        public AchivementModel AchivementModel { get { return (achivementModel == null) ? achivementModel = AchivementManager.Instance.GetAchivement(AchivementID) : achivementModel; } }

#if UNITY_EDITOR
        private List<string> achivementIDs
        {
            get
            {
                string[] guids = UnityEditor.AssetDatabase.FindAssets("t:AchivementData");
                List<AchivementData> achivementDatas = new List<AchivementData>();
                for (int i = 0; i < guids.Length; i++)
                {
                    AchivementData data = UnityEditor.AssetDatabase.LoadAssetAtPath(UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]), typeof(AchivementData)) as AchivementData;
                    achivementDatas.Add(data);
                }

                List<string> ids = new List<string>();
                for (int i = 0; i < achivementDatas.Count; i++)
                {
                    for (int j = 0; j < achivementDatas[i].Achivements.Count; j++)
                    {
                        if (!ids.Contains(achivementDatas[i].Achivements[j].AchivementID))
                            ids.Add(achivementDatas[i].Achivements[j].AchivementID);

                    }
                }

                return ids;
            }
        }
#endif

        protected virtual void OnEnable()
        {
            AchivementManager.Instance.OnAchivementUnlock.AddListener(GetNextAchivement);
        }

        protected virtual void OnDisable()
        {
            AchivementManager.Instance.OnAchivementUnlock.RemoveListener(GetNextAchivement);
        }

        public virtual void GetNextAchivement(AchivementModel _achivementModel)
        {

            if (!string.Equals(_achivementModel.AchivementID, AchivementModel.AchivementID))
                return;

            AchivementModel newModel = AchivementModel;
            newModel = AchivementManager.Instance.GetAchivement(achivementModel.AchivementID);

            if (newModel != null)
                achivementModel = newModel;
        }

        public virtual void UpdateAchivement(float currentValue)
        {
            AchivementModel.CompleteAchivement(currentValue);
            AchivementManager.Instance.UpdateAchivement(AchivementModel);
        }
    }
}
