using System.Collections;
using System.Collections.Generic;
using HCB.IncrimantalIdleSystem;
using HCB.Utilities;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
   [SerializeField] private IdleStat _fireRate;

   public IdleStat FireRate => _fireRate;
}
