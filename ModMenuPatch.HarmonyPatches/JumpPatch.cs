using GorillaLocomotion;
using HarmonyLib;

namespace ModMenuPatch.HarmonyPatches;

[HarmonyPatch(typeof(Player))]
[HarmonyPatch("LateUpdate", MethodType.Normal)]
internal class JumpPatch
{ 

	private static void Prefix()
	{
}
}