using UnityEngine;
using HarmonyLib;

namespace KosmosModMenu.Mods
{
    [HarmonyPatch(typeof(GorillaNot), "IncrementRPCCall")]
    [HarmonyPatch(typeof(GorillaNot), "SendReport")]
    [HarmonyPatch(typeof(GorillaNot), "LogErrorCount")]
    [HarmonyPatch(typeof(GorillaNot), "CheckReports")]
    public class NotPatch : MonoBehaviour
    {
        [HarmonyPrefix]
        private static bool Prefix()
        {
            Debug.Log("Blocked GorillaNot"); // Credits: Shaddy Anti Cheat <--
            return false;
        }
    }

    public class PatchManager : MonoBehaviour
    {
        private void Start()
        {
            CreateGameObjectAndAddComponent();
        }

        private void CreateGameObjectAndAddComponent()
        {
            GameObject newGameObject = new GameObject("AntiBan");
            NotPatch notPatch = newGameObject.AddComponent<NotPatch>();
        }
    }
}
