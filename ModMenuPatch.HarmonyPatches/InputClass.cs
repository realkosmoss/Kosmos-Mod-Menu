using System;
using System.Collections.Generic;
using GorillaLocomotion;
using HarmonyLib;
using UnityEngine;
using UnityEngine.XR;

namespace Inputs
{


    public class InputClass
    {
        private static void Prefix(Player __instance)
        {
            List<InputDevice> list = new List<InputDevice>();

            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out rightTrigger);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out leftTrigger);
            
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out rightGrip);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out leftGrip);

            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.primaryButton, out rightPrimary);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list);
            list[0].TryGetFeatureValue(CommonUsages.primaryButton, out leftPrimary);

            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.secondaryButton, out rightSecondary);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list);
            list[0].TryGetFeatureValue(CommonUsages.secondaryButton, out leftSecondary);

            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.primary2DAxisClick, out rightJoystick);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list);
            list[0].TryGetFeatureValue(CommonUsages.primary2DAxisClick, out leftJoystick);
        }

        public static bool rightGrip;
        public static bool leftGrip;

        public static bool rightTrigger;
        public static bool leftTrigger;

        public static bool rightPrimary;
        public static bool leftPrimary;

        public static bool rightSecondary;
        public static bool leftSecondary;

        public static bool rightJoystick;
        public static bool leftJoystick;
    }
}
