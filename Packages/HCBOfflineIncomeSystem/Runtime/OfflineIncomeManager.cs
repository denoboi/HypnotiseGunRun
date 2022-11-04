using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;
using HCB.Core;
using System;

namespace HCB.OfflineIncomeSystem
{
    public class OfflineIncomeManager : Singleton<OfflineIncomeManager>
    {
        private OfflineIncomeData _offlineIncomeData;        
        public OfflineIncomeData OfflineIncomeData
        {
            get
            {
                if (_offlineIncomeData == null)
                {
                    _offlineIncomeData = SaveLoadManager.LoadPDP<OfflineIncomeData>(SAVE_FILE_NAME, new OfflineIncomeData());
                    if (_offlineIncomeData == null)
                        _offlineIncomeData = new OfflineIncomeData();
                }
                return _offlineIncomeData;
            }
        }

        public double? OfflineIncome { get; private set; }

        public const string SAVE_FILE_NAME = "OfflineIncomeData";

        private const float MAXIMUM_OFFLINE_INCOME = 50000f;
        private const float OFFLINE_INCOME_MULTIPLIER = 10f;

        private void Awake()
        {
            OfflineIncome = GetOfflineIncome();
        }

        private double? GetOfflineIncome()
        {
            string logoutDate = OfflineIncomeData.LogoutDate;
            if (string.IsNullOrEmpty(logoutDate))
                return null;

            DateTime startDate = DateTime.Parse(logoutDate);
            DateTime timeNow = DateTime.Now;

            double minutesDifference = (timeNow - startDate).TotalMinutes;

            //Change the algorithm according to your game
            double income = minutesDifference * OFFLINE_INCOME_MULTIPLIER;
            income = Mathf.Clamp((float)income, 0, MAXIMUM_OFFLINE_INCOME);

            return income;
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveLogoutDate();                
            }
        }

        private void OnApplicationQuit()
        {
            SaveLogoutDate();            
        }

        private void SaveLogoutDate()
        {
            OfflineIncomeData.LogoutDate = DateTime.Now.ToString();
            SaveLoadManager.SavePDP(OfflineIncomeData, SAVE_FILE_NAME);
        }
    }
}
