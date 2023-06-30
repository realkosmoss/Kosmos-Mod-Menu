using System.Collections.Generic;
using System.Reflection;
using BepInEx;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR;
using static Photon.Pun.UtilityScripts.TabViewManager;
using UnityEngine.SceneManagement;
using ModsKosmosModMenuBoxESP;
using System;
using UnityEngine.UI;
using Player = GorillaLocomotion.Player;
using UnityEngine.Networking;

namespace KosmosGUI
{
    [BepInPlugin("Kosmos.Gui", "Kosmos Gui", "6.9.9")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            if (init)
            {
                GorillaComputer.instance.networkController.disableAFKKick = true;
                Debug.Log("Kosmos GUI Has Loaded");
                init = false;
            }

            // what happens every frame

            new Harmony("Kosmos.Gui").PatchAll(Assembly.GetExecutingAssembly());
        }

        // what happens on the gui
        public void OnGUI()
        {
            // initialize your colors
            GUI.backgroundColor = purple;
            GUI.color = lightPurple;

            // open/close gui button
            if (GUI.Button(new Rect(0f, 0f, 20f, 20f), ""))
            {
                CloseGUI = !CloseGUI;
            }


            // what happens when the gui is on
            if (!CloseGUI)
            {
                // the background of the gui
                GUI.Box(new Rect(30f, 50f, 165, 140f), "Kosmos GUI");

                // this creates all the buttons on the gui
                modActive[0] = GUI.Toggle(new Rect(50f, 65f, 500f, 25f), modActive[0], modNames[0]);
                modActive[1] = GUI.Toggle(new Rect(50f, 80f, 500f, 25f), modActive[1], modNames[1]);
                modActive[2] = GUI.Toggle(new Rect(50f, 110f, 500f, 25f), modActive[2], modNames[2]);
                modActive[3] = GUI.Toggle(new Rect(50f, 140f, 500f, 25f), modActive[3], modNames[3]);
                modActive[4] = GUI.Toggle(new Rect(50f, 160f, 500f, 25f), modActive[4], modNames[4]);
                modActive[5] = GUI.Toggle(new Rect(50f, 180f, 500f, 25f), modActive[5], modNames[5]);
                modActive[6] = GUI.Toggle(new Rect(50f, 200f, 500f, 25f), modActive[6], modNames[6]);
                // this is where the mods are linked to the buttons
                if (modActive[0])
                {
                    Application.Quit();
                }
                if (modActive[1])
                {
                    PhotonNetwork.Disconnect();
                }
                if (modActive[2])
                {
                    ProcessKeyboardMovement();
                }
                if (modActive[3])
                {
                    UpdateRoomInfo();
                }
                if (modActive[4])
                {
                    PhotonNetwork.ConnectToBestCloudServer();
                }
                if (modActive[5])
                {
                    for (int i = 0; i < 10; i++)
                    {
                        GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", RpcTarget.All, new object[]
                        {
        GorillaTagger.Instance.myVRRig.transform.position,
        UnityEngine.Random.rotation,
        4f,
        100f,
        false,
        false
                        });
                    }
                }
                if (modActive[6])
                {
                    PcGun();
                }
            }
        }

        // colors (make your own!!)
        public static Color purple = new Color(0.5f, 0f, 1f, 1f);

        public static Color lightPurple = new Color(0.75f, 0.3f, 1f, 1f);



        private static bool FastBugOnorOff = false;
        private static bool SuperFastBugOnorOff = false;
        private static void FastBug(bool enable)
        {
            if (enable)
            {
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().maxNaturalSpeed = 10f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobingSpeed = 12f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobMagnintude = 1.2f;
            }
            else if (!enable && !SuperFastBugOnorOff)
            {
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().maxNaturalSpeed = 1f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobingSpeed = 3f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobMagnintude = 0.3f;
            }
        }

        private static void SuperFastBug(bool enable)
        {
            if (enable)
            {
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().maxNaturalSpeed = 10f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobingSpeed = 12f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobMagnintude = 1.2f;
                SuperFastBugOnorOff = true;
            }
            else if (!enable && !FastBugOnorOff)
            {
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().maxNaturalSpeed = 1f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobingSpeed = 3f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobMagnintude = 0.3f;
                SuperFastBugOnorOff = false;
            }
        }

        public static void UpdateRoomInfo()
        {
            {
                GameObject.Find("Level/lower level/UI/CodeOfConduct/COC Text").GetComponent<Text>().text = "INFO";
                GameObject.Find("COC Text").GetComponent<Text>().text = string.Concat(new object[]
                {
                "\nROOM INFO: ",
                PhotonNetwork.CurrentRoom,
                "\nPLAYERS IN ROOM: ",
                PhotonNetwork.CountOfPlayers,
                "\nPLAYERS ONLINE: ",
                PhotonNetwork.CountOfPlayersInRooms,
                "\nTIME: ",
                DateTime.Now.ToString("hh:mm"),
                "\nMASTER?: ",
                PhotonNetwork.LocalPlayer.IsMasterClient,
                "\nNAME: ",
                PhotonNetwork.LocalPlayer.NickName,
                "\nPLAYERID: ",
                PhotonNetwork.LocalPlayer.UserId
                });
            }
        }



        public static GameObject pointer = null;
        public static void PcWaterGun()
        {
            bool mouseButton = Input.GetMouseButton(1);
            if (mouseButton)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;
                bool flag = Physics.Raycast(ray, out raycastHit) && pointer == null;
                if (flag)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Destroy(pointer.GetComponent<Rigidbody>());
                    Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                pointer.transform.position = raycastHit.point;
                Photon.Realtime.Player owner = raycastHit.collider.GetComponentInParent<PhotonView>().Owner;
                bool mouseButtonDown = Input.GetMouseButtonDown(0);
                if (mouseButtonDown)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.myVRRig.transform.position = pointer.transform.position + new Vector3(0, -1, 0);
                    GorillaTagger.Instance.offlineVRRig.transform.position = pointer.transform.position + new Vector3(0, -1, 0);

                    GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", RpcTarget.All, new object[]
{
                    raycastHit.point,
                    UnityEngine.Random.rotation,
                    4f,
                    100f,
                    false,
                    false
                    });
                }
                else
                {
                    GorillaTagger.Instance.myVRRig.enabled = true;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }
        public static void PcGun()
        {
            bool mouseButton = Input.GetMouseButtonDown(1);
            if (mouseButton)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit))
                {
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Destroy(pointer.GetComponent<Rigidbody>());
                        Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }

                    pointer.transform.position = raycastHit.point;
                }
                else
                {
                    if (pointer != null)
                    {
                        Destroy(pointer);
                        pointer = null;
                    }
                }
            }
        }




        public static void spamwater()
        {
            GorillaTagger.Instance.myVRRig.RPC("PlaySplashEffect", RpcTarget.All, new object[]
            {
        GorillaTagger.Instance.myVRRig.transform.position,
        GorillaTagger.Instance.myVRRig.transform.rotation,
        1,
        100f,
        false,
        false
            });
        }









        public static void ProcessKeyboardMovement()
        {
            // this makes your hands go to your head
            GorillaLocomotion.Player.Instance.leftHandFollower.position = GorillaLocomotion.Player.Instance.headCollider.transform.position;
            GorillaLocomotion.Player.Instance.rightHandFollower.position = GorillaLocomotion.Player.Instance.headCollider.transform.position;

            // this makes your gorilla able to move
            if (UnityInput.Current.GetKey(KeyCode.W))
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(GorillaLocomotion.Player.Instance.transform.forward.x * 15f, 0f, GorillaLocomotion.Player.Instance.transform.forward.z * 15f), ForceMode.Acceleration);
                resetRigidVelocity = true;
            }
            if (UnityInput.Current.GetKey(KeyCode.S))
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(GorillaLocomotion.Player.Instance.transform.forward.x * -15f, 0f, GorillaLocomotion.Player.Instance.transform.forward.z * -15f), ForceMode.Acceleration);
                resetRigidVelocity = true;
            }

            // this makes your gorilla able to turn
            if (UnityInput.Current.GetKey(KeyCode.A))
            {
                GorillaLocomotion.Player.Instance.transform.Rotate(0f, -1f, 0f);
            }
            if (UnityInput.Current.GetKey(KeyCode.D))
            {
                GorillaLocomotion.Player.Instance.transform.Rotate(0f, 1f, 0f);
            }

            // this makes your gorilla able to go up and down
            if (UnityInput.Current.GetKey(KeyCode.Space))
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(0f, 3f, 0f, ForceMode.Impulse);
            }
            if (UnityInput.Current.GetKey(KeyCode.LeftControl))
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(0f, -3f, 0f, ForceMode.Impulse);
            }

            // this resets the rigidbody velocity once you dont press any buttons
            if (!UnityInput.Current.GetKey(KeyCode.W) && !UnityInput.Current.GetKey(KeyCode.S) && resetRigidVelocity)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
                resetRigidVelocity = false;
            }
        }

        public static bool resetRigidVelocity = false;

        // important gui variables
        private static bool CloseGUI = true;

        private static bool init = true;

        // mod toggles
        public static bool[] modActive = new bool[7]
        {
            false,
            false,
            false,
            false,
            false,
            false,
            false,
        };

        // mod names
        public static string[] modNames = new string[7]
        {
            "Quit Gorilla Tag",
            "Disconnect",
            "Keyboard Movement",
            "Cool Coc Board With Info!",
            "Less Ping",
            "WATER!",
            "WATERGUN PC!",
        };
    }
}
