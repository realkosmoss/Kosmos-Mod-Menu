using HarmonyLib;
using UnityEngine;
using KosmosModMenu.Mods;
using PunchMod;

namespace ModMenuPatch.HarmonyPatches
{
    public static class Loader
    {
        private static GameObject gameObject;

        public static void Load()
        {
            gameObject = new GameObject("KosmosThingy");
            Object.DontDestroyOnLoad(gameObject);

            gameObject.AddComponent(typeof(PunchMod.Plugin));
            gameObject.AddComponent(typeof(PunchHandManager));
            gameObject.AddComponent(typeof(Tracker.tracker));
            gameObject.AddComponent(typeof(KosmosGUI.Plugin));
            gameObject.AddComponent(typeof(NotPatch));
            gameObject.AddComponent(typeof(ForAntiCheatFucker));
            gameObject.AddComponent(typeof(AntiBan));
            gameObject.AddComponent(typeof(ButtonHandler));
            gameObject.AddComponent(typeof(GorillaPlayer));
            gameObject.AddComponent(typeof(ModMenuPatches));
            gameObject.AddComponent(typeof(PlayerXRController));
            gameObject.AddComponent(typeof(JumpPatch));
            gameObject.AddComponent(typeof(BtnCollider));
            gameObject.AddComponent(typeof(KosmosModMenu));
            gameObject.AddComponent(typeof(ModMenuPatch));
        }
    }
}
