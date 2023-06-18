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
using KosmosModMenuChams;
using IronMonke;

namespace KosmosModMenu.Mods
{
    internal class OpMods
    {










        // Trampoline Mods ( In Construction ) \\

        // End Of Trampoline Mods \\


        // Water Mods Under \\
        public static void spamwater()
        {
            for (int i = 1; i <= 20; i++)
            {
                GorillaTagger.Instance.myVRRig.photonView.RPC("PlaySplashEffect", RpcTarget.All, new object[]
                {
            GorillaTagger.Instance.myVRRig.transform.position,
            GorillaTagger.Instance.myVRRig.headConstraint.transform.rotation,
            100f,
            100f,
            false,
            false
                });
            }
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
                        GorillaTagger.Instance.myVRRig.photonView.RPC("PlaySplashEffect", RpcTarget.All, new object[]
                        {
                    raycastHit.point,
                    GorillaTagger.Instance.myVRRig.headConstraint.transform.rotation,
                    100f,
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
                        GorillaTagger.Instance.myVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.myVRRig.leftHandTransform.position = pointer.transform.position;
                        GorillaTagger.Instance.offlineVRRig.leftHandTransform.position = pointer.transform.position;
                        stupidfag = false;
                        return;
                    }
                }
                if (!triggerPressed && stupidfag)
                {
                    GorillaTagger.Instance.myVRRig.enabled = true;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                    stupidfag = true;
                }
            }
            else
            {
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
        // end of water mods \\


        // important things \\
        public static bool antiRepeat;
        public static GameObject pointer = null;
        // end of important things \\
    }
}
