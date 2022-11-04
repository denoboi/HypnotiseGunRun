using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.AchivementSystem
{
    public interface IAchivement
    {
        public string AchivementID { get; }
        public AchivementModel AchivementModel { get; }

        public void UpdateAchivement(float currentValue);
    }
}
