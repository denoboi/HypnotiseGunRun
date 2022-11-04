using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using TMPro;

namespace HCB.UI
{
    public class LevelTextController : MonoBehaviour
    {
        public TextMeshProUGUI levelDisplayText;
        public TextMeshProUGUI LevelDisplayText { get { return (levelDisplayText == null) ? levelDisplayText = GetComponent<TextMeshProUGUI>() : levelDisplayText; } }


        private void SetFakeLevel()
        {
            int fakeLevel = PlayerPrefs.GetInt("FakeLevel", 1);
            fakeLevel++;
            PlayerPrefs.SetInt("FakeLevel", fakeLevel);

            LevelDisplayText.SetText("Level " + fakeLevel);
        }
    }
}
