using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;
using HCB.Core;
using System;

namespace HCB.GridSystem
{
    public class GridSystemManager : Singleton<GridSystemManager>
    {
        private GridSystemData _gridSystemData = null;
        public GridSystemData GridSystemData
        {
            get
            {
                if (_gridSystemData == null)
                {
                    _gridSystemData = SaveLoadManager.LoadPDP<GridSystemData>(SAVE_FILE_NAME, new GridSystemData());
                    if (_gridSystemData == null)
                        _gridSystemData = new GridSystemData();
                }
                return _gridSystemData;
            }
        }

        public List<Action> OnApplicationPauseActions = new List<Action>();
        public List<Action> OnApplicationQuitActions = new List<Action>();

        public const string SAVE_FILE_NAME = "GridSystemData";

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                foreach (var pauseAction in OnApplicationPauseActions)
                {
                    pauseAction?.Invoke();
                }

                SaveLoadManager.SavePDP(GridSystemData, SAVE_FILE_NAME);
            }
        }

        private void OnApplicationQuit()
        {
            foreach (var quitAction in OnApplicationQuitActions)
            {
                quitAction?.Invoke();
            }

            SaveLoadManager.SavePDP(GridSystemData, SAVE_FILE_NAME);
        }
    }
}
