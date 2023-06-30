using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using BepInEx.Configuration;
using System.Collections;
using Object = UnityEngine.Object;
using Player = GorillaLocomotion.Player;
using GorillaLocomotion.Gameplay;
using GorillaLocomotion.Swimming;
using KosmosModMenu.Mods;
using IronMonke;
using System.Net;
using System.Threading;

namespace KosmosModMenu.Mods
{
    internal class OpMods
    {






        // Monke AI Basement Update \\
        public static void OverPoweredMonkeyAI()
        {
            foreach (MonkeyeAI monkeyeAI in Object.FindObjectsOfType<MonkeyeAI>())
            {
                monkeyeAI.rotationSpeed = 69;
                monkeyeAI.speed = 3;
                monkeyeAI.attackDistance = 6969;
                monkeyeAI.closeFloorTime = 6969;
                monkeyeAI.chaseDistance = 6969;
                monkeyeAI.beginAttackTime = 6969;
            }
        }
        public static void FreezeMonkeyAI()
        {
            foreach (MonkeyeAI monkeyeAI in Object.FindObjectsOfType<MonkeyeAI>())
            {
                monkeyeAI.rotationSpeed = 69;
                monkeyeAI.speed = 3;
            }
        }
        public static void FastMonkeyAI()
        {
                foreach (MonkeyeAI monkeyeAI in Object.FindObjectsOfType<MonkeyeAI>())
                {
                monkeyeAI.rotationSpeed = 69;
                monkeyeAI.speed = 3;
            }
        }
        public static void FreezeAll()
        {
            foreach (VRRig vrrig in Object.FindObjectsOfType<VRRig>())
            {
                foreach (MonkeyeAI_ReplState monkeyeAI_ReplState in Object.FindObjectsOfType<MonkeyeAI_ReplState>())
                {
                    GorillaTagger.StatusEffect statusEffect = (GorillaTagger.StatusEffect)1;
                    GorillaTagger.Instance.ApplyStatusEffect(statusEffect, GorillaTagger.Instance.tagCooldown);
                    monkeyeAI_ReplState.freezePlayer = true;
                }
            }
        }
        // End Of Monke AI \\

        // Trampoline Mods ( In Construction ) \\
        public static void TrampolineSpeed()
        {
            GameObject gameObject = GameObject.Find("Level/canyon/Canyon/CanyonSummer23Geo/TrampolineA_Prefab Variant/BounceyTop");
            GorillaSurfaceOverride component = gameObject.GetComponent<GorillaSurfaceOverride>();
            component.extraVelMaxMultiplier = 100f;
            component.extraVelMultiplier = 150f;
        }

        // End Of Trampoline Mods \\


        // Water Mods Under \\

        public static void MakeWaterBig()
        {
            foreach (WaterParameters waterParameters in Object.FindObjectsOfType(typeof(WaterParameters)))
            {
                waterParameters.splashEffectScale = 69f;
                waterParameters.rippleEffectScale = 69f;
            }
        }

        public static void spamwater()
        {
            for (int i = 1; i <= 5; i++)
            {
                GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", RpcTarget.All, new object[]
                {
            GorillaTagger.Instance.myVRRig.transform.position,
            UnityEngine.Random.rotation,
            4,
            100f,
            false,
            false
                });
            }
        }




        public static void watergunANYWHERE()
        {
            bool value = false;
            bool value2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out value);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out value2);
            if (value2)
            {
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out var hitInfo);
                if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                pointer.transform.position = hitInfo.point;
                if (value)
                {
                    {
                        GorillaTagger.Instance.myVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.myVRRig.transform.position = pointer.transform.position + new Vector3(0, -1, 0);
                        GorillaTagger.Instance.offlineVRRig.transform.position = pointer.transform.position + new Vector3(0, -1, 0);

                        GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", RpcTarget.All, new object[]
                        {
                    hitInfo.point,
                    UnityEngine.Random.rotation,
                    4f,
                    100f,
                    false,
                    false
                        });
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                GorillaTagger.Instance.offlineVRRig.enabled = true;
                UnityEngine.Object.Destroy(pointer);
                pointer = null;
            }
        }



        public static void test()
        {
            PhotonNetwork.RejoinRoom(PhotonNetwork.CurrentRoom.Name);
        }

        public static void watergun()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            bool flag3 = !flag2;
            if (flag3)
            {
                Object.Destroy(pointer);
                pointer = null;
                antiRepeat = false;
            }
            else
            {
                RaycastHit raycastHit;
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                bool flag4 = pointer == null;
                if (flag4)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Object.Destroy(pointer.GetComponent<Rigidbody>());
                    Object.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                pointer.transform.position = raycastHit.point;
                bool flag5 = !flag;
                if (flag5)
                {
                    antiRepeat = false;
                }
                else
                {
                    bool flag6 = !antiRepeat;
                    if (flag6)
                    {
                        GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", 0, new object[]
                        {
                            GorillaTagger.Instance.myVRRig,
                            UnityEngine.Random.rotation,
                            4f,
                            100f,
                            false,
                            false
                        });
                    }
                    antiRepeat = true;
                }
            }
        }


        public static void WaterSpamGun()
        {
            bool flag = waterbox == null;
            bool flag2 = flag;
            if (flag2)
            {
                GameObject.Find("Level").transform.Find("ForestToBeach_Prefab_V4").gameObject.SetActive(true);
                waterbox = Object.Instantiate<GameObject>(GameObject.Find("Level/ForestToBeach_Prefab_V4/CaveWaterVolume"));
                Object.Destroy(waterbox.GetComponentInChildren<Renderer>());
            }

            bool stupidfag = true;
            bool triggerPressed = false;
            bool gripPressed = false;
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevices(devices);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, devices);
            devices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);
            devices[0].TryGetFeatureValue(CommonUsages.gripButton, out gripPressed);

            if (gripPressed)
            {
                RaycastHit raycastHit;
                bool raycastSuccess = Physics.Raycast(Player.Instance.rightControllerTransform.position - Player.Instance.rightControllerTransform.up, -Player.Instance.rightControllerTransform.up, out raycastHit);
                if (raycastSuccess && pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Object.Destroy(pointer.GetComponent<Rigidbody>());
                    Object.Destroy(pointer.GetComponent<Collider>());
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                if (pointer != null)
                {
                    pointer.transform.position = raycastHit.point;
                    GorillaTagger.Instance.bodyCollider.transform.position = raycastHit.point;
                }

                if (triggerPressed)
                {
                    if (pointer != null)
                    {
                        waterbox.transform.position = pointer.transform.position;
                        stupidfag = false;
                        return;
                    }
                }
                if (!triggerPressed && stupidfag)
                {
                    stupidfag = true;
                    waterbox.transform.position = new Vector3(0f, -6969f, 0f);
                }
            }
            else
            {
                waterbox.transform.position = new Vector3(0f, -6969f, 0f);
                Object.Destroy(pointer);
            }
        }


        public static GameObject waterbox;
        public static void SwimInAir()
        {
            bool flag = waterbox == null;
            bool flag2 = flag;
            if (flag2)
            {
                GameObject.Find("Level").transform.Find("ForestToBeach_Prefab_V4").gameObject.SetActive(true);
                waterbox = Object.Instantiate<GameObject>(GameObject.Find("Level/ForestToBeach_Prefab_V4/CaveWaterVolume"));
                Object.Destroy(waterbox.GetComponentInChildren<Renderer>());
            }
            else
            {
                bool flag3;
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.gripButton, out flag3);
                bool flag4;
                InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.gripButton, out flag4);
                bool flag5 = flag3 && flag4;
                bool flag6 = flag5;
                if (flag6)
                {
                    waterbox.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(0f, 1f, 0f);
                }
                else
                {
                    waterbox.transform.position = new Vector3(0f, -6969f, 0f);
                }
            }
        }

        public static void WaterSpam()
        {
            bool flag = waterbox == null;
            bool flag2 = flag;
            if (flag2)
            {
                GameObject.Find("Level").transform.Find("ForestToBeach_Prefab_V4").gameObject.SetActive(true);
                waterbox = Object.Instantiate<GameObject>(GameObject.Find("Level/ForestToBeach_Prefab_V4/CaveWaterVolume"));
                Object.Destroy(waterbox.GetComponentInChildren<Renderer>());
            }
            else
            {
                bool triggerPressed = false;
                List<InputDevice> devices = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, devices);
                devices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);
                if (triggerPressed)
                {
                    waterbox.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(0f, 1f, 0f);
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddExplosionForce(3f, GorillaLocomotion.Player.Instance.transform.position, 12f, 0f, ForceMode.Force);
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(0f, 3f, 0f, ForceMode.Impulse);
                }
                else
                {
                    waterbox.transform.position = new Vector3(0f, -6969f, 0f);
                }
            }
        }

        private static bool isTriggerHeld;
        public static void WaterSpamV2()
        {
            bool flag = waterbox == null;
            bool flag2 = flag;
            if (flag2)
            {
                GameObject level = GameObject.Find("Level");
                Transform forestToBeach = level.transform.Find("ForestToBeach_Prefab_V4");
                forestToBeach.gameObject.SetActive(true);
                waterbox = Object.Instantiate<GameObject>(forestToBeach.Find("CaveWaterVolume").gameObject);
                Object.Destroy(waterbox.GetComponentInChildren<Renderer>());
            }
            else
            {
                List<InputDevice> devices = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, devices);
                devices[0].TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

                if (triggerPressed)
                {
                    isTriggerHeld = true;
                    while (isTriggerHeld)
                    {
                        float minX = 0f;
                        float maxX = 1f;
                        float minY = 0f;
                        float maxY = 1f;
                        float minZ = 0f;
                        float maxZ = 1f;

                        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), UnityEngine.Random.Range(minZ, maxZ));
                        waterbox.transform.position = GorillaTagger.Instance.headCollider.transform.position + randomPosition;

                        devices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);
                        if (!triggerPressed)
                        {
                            isTriggerHeld = false;
                            break;
                        }
                    }
                }
                else
                {
                    isTriggerHeld = false;
                    waterbox.transform.position = new Vector3(0f, -6969f, 0f);
                }
            }
        }
        // end of water mods \\







        // important things \\
        public static bool antiRepeat;
        public static GameObject pointer = null;
        // end of important things \\
    }
}
