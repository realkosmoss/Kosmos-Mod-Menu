using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace KosmosModMenu.Patches
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]

    public class ExamplePatch : MonoBehaviour
    {



    }
}
