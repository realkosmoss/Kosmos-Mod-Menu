using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace KosmosModMenu.Mods // CREDITS TO SHADDY!
{
    internal class AntiBan
    {
        public AntiBan()
        {
            GameObject.Find("SaveModAccountData").GetComponent<GorillaComputer>().enabled = false;
            PhotonNetwork.LocalPlayer.CustomProperties["mods"] = "";
        }
    }
}

namespace AntiCheatGracePeriod
{
    [HarmonyPatch]
    public class GracePatch
    {
        [HarmonyPatch(typeof(GorillaNetworkPublicTestsJoin), "GracePeriod")]
        [HarmonyPatch(typeof(GorillaNetworkPublicTestJoin2), "GracePeriod")]
        [HarmonyPrefix]
        private static bool Prefix()
        {
            Debug.Log("-rep graceperiod check ez bypass");
            return false;
        }
    }
}


namespace AntiKickGunOld
{
    // Token: 0x02000004 RID: 4
    [HarmonyPatch(typeof(PhotonNetworkController), "ProcessJoiningFriendState")]
    public class JoiningPatch
    {
        [HarmonyPrefix]
        private static bool Prefix()
        {
            Debug.Log("-rep on the check for friends");
            return false;
        }
    }
}

namespace AntiGorillaNot
{
    [HarmonyPatch]
    public class NotPatch
    {
        [HarmonyPatch(typeof(GorillaNot), "IncrementRPCCall")]
        [HarmonyPatch(typeof(GorillaNot), "SendReport")]
        [HarmonyPatch(typeof(GorillaNot), "LogErrorCount")]
        [HarmonyPatch(typeof(GorillaNot), "CheckReports")]
        [HarmonyPatch(typeof(GorillaNot), "SendReport")]
        [HarmonyPrefix]
        private static bool Prefix()
        {
            Debug.Log("-rep blocked gorillanot ac event");
            return false;
        }
    }
}
