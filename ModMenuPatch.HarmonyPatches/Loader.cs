using HarmonyLib;
using UnityEngine;
using KosmosModMenu.Mods;

namespace ModMenuPatch.HarmonyPatches
{
    public static class Loader
    {
        private static GameObject gameObject;

        public static void Load()
        {
            gameObject = new GameObject("KosmosThingy");
            Object.DontDestroyOnLoad(gameObject);


            gameObject.AddComponent(typeof(Tracker.tracker));
            gameObject.AddComponent(typeof(KosmosGUI.Plugin));
            gameObject.AddComponent(typeof(NotPatch));
            gameObject.AddComponent(typeof(AntiGorillaNot.NotPatch));
            gameObject.AddComponent(typeof(AntiKickGunOld.JoiningPatch));
            gameObject.AddComponent(typeof(AntiCheatGracePeriod.GracePatch));
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
