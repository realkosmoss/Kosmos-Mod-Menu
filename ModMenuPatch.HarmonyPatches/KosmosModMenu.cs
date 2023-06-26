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
using System.ComponentModel.Design;
using ModsKosmosModMenuBoxESP;
using PlayFab.MultiplayerModels;

namespace ModMenuPatch.HarmonyPatches
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("LateUpdate", 0)]
    internal class KosmosModMenu
    {
        public enum PhotonEventCodes
        {
            left_jump_photoncode = 69,
            right_jump_photoncode,
            left_jump_deletion,
            right_jump_deletion
        }
        public class TimedBehaviour : MonoBehaviour
        {
            public bool complete = false;

            public bool loop = true;

            public float progress = 0f;

            protected bool paused = false;

            protected float startTime;

            public static int bigmonkecooldown;

            public static bool beachmap;

            protected float duration = 2f;

            public virtual void Start()
            {
                startTime = Time.time;
            }

            public virtual void Update()
            {
                if (complete)
                {
                    return;
                }
                progress = Mathf.Clamp((Time.time - startTime) / duration, 0f, 1f);
                if (Time.time - startTime > duration)
                {
                    if (loop)
                    {
                        OnLoop();
                    }
                    else
                    {
                        complete = true;
                    }
                }
            }

            public virtual void OnLoop()
            {
                startTime = Time.time;
            }
        }

        public class ColorChanger : TimedBehaviour
        {
            public Renderer gameObjectRenderer;

            public Gradient colors = null;

            public Color color;

            public bool timeBased = true;

            public override void Start()
            {
                base.Start();
                gameObjectRenderer = GetComponent<Renderer>();
            }
        }

        public static bool ResetSpeed = false;

        private static string[] buttons = new string[]
        {
        "Disconnect",
        "Join Random",
        "ThumbDisconnect",
        "Platforms (UD)",
        "SlideControl (UD)",
        "GhostMonke (UD)",
        "Noclip Fly (UD)",
        "IronMonke (UD)",
        "Noclip (UD)",
        "HeadSpin (UD)",
        "AllInOneFun (UD)",
        "Steam LongArms",
        "TriggerUpsideDownHead (UD)",
        "Invisiblity (UD)",
        "Low Gravity (UD)",
        "Tracers (UD)",
        "GrabALLIds (UD)",
        "Teleport Gun (UD)",
        "SetMaster (D)",
        "SetNameKKK (UD)",
        "SetNameKosmos (UD)",
        "SetNameInvisible (UD)",
        "No Handtap Cooldown",
        "ESP",
        "Trap All Modders (NT)(UD)",
        "Big And Small (CS)(UD)",
        "GiveHuntWatch (CS)",
        "TeleportToRandom (TRIGGER)(UD)",
        "ScareGun (UD)",
        "All Cosmetics (CS)(UD)",
        "Tag ALL (UD)",
        "Tag Gun (UD)",
        "Tag Aura (UD)",
        "Nametags",
        "Teleport To Stump (UD)",
        "Swim In Air",
        "Slingshot ALL (SS)(UD)",
        "Slingshot Self (SS)(UD)",
        "Waterspam (Trigger)",
        "Waterspam GUN",
        "Destroy Gun",
        "Delete ALL",
        "LAG ALL EXTREME BAN SELF AHAHAH",
        "Grab Bug",
        "Cool FPS Counter",
        "Water Gun",
        "Spam Water",
        "Big Water Splash (For Water Mods)",
        "Spin Monke (G)",
        "Heaven Monke (G)",
        "Spin Around Monke (G)",
        "Rape Gun NEEDS FIX",
        "Spin Around Cursor Gun",
        "Follow Movement Gun",
        "Fast Trampoline Speed",
        "Freeze ALL (D?)",
        "No Tag On Join Pub (UD)",
        "Low Quality",
        "Watergun TEST",
    };

        private static bool?[] buttonsActive = new bool?[]
        {
        false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};

        private static bool gripDown;

        private static GameObject menu = null;

        private static GameObject canvasObj = null;

        private static GameObject reference = null;

        public static int framePressCooldown = 0;

        private static GameObject pointer = null;

        private static bool gravityToggled = false;

        private static int btnCooldown = 0;

        private static int soundCooldown = 0;

        private static float? maxJumpSpeed = null;

        private static float? jumpMultiplier = null;

        private static object index;

        public static int BlueMaterial = 5;

        public static int TransparentMaterial = 6;

        public static int LavaMaterial = 2;

        public static int RockMaterial = 1;

        public static int DefaultMaterial = 5;

        public static int NeonRed = 3;

        public static int RedTransparent = 4;

        public static int self = 0;

        private static Vector3? leftHandOffsetInitial = null;

        private static Vector3? rightHandOffsetInitial = null;

        private static float? maxArmLengthInitial = null;

        private static bool noClipDisabledOneshot = false;

        private static bool noClipEnabledAtLeastOnce = false;

        private static bool ghostToggle = false;

        private static bool bigMonkeyEnabled = false;

        private static bool bigMonkeAntiRepeat = false;

        private static int bigMonkeCooldown = 0;

        private static bool ghostMonkeEnabled = false;

        private static bool ghostMonkeAntiRepeat = false;

        private static int ghostMonkeCooldown = 0;

        private static bool checkedProps = false;

        private static bool teleportGunAntiRepeat = false;

        private static Color colorRgbMonke = new Color(0f, 0f, 0f);

        private static float hueRgbMonke = 0f;

        private static float timerRgbMonke = 0f;

        private static float updateRateRgbMonke = 0f;

        private static float updateTimerRgbMonke = 0f;

        private static bool flag2 = false;

        private static bool flag1 = true;

        private static Vector3 scale = new Vector3(0.0125f, 0.28f, 0.3825f);

        private static bool gripDown_left;

        private static bool gripDown_right;

        private static bool once_left;

        private static bool once_right;

        private static bool once_left_false;

        private static bool once_right_false;

        private static bool once_networking;

        private static GameObject[] jump_left_network = new GameObject[9999];

        private static GameObject[] jump_right_network = new GameObject[9999];

        private static GameObject jump_left_local = null;

        private static GameObject jump_right_local = null;

        private static GradientColorKey[] colorKeysPlatformMonke = new GradientColorKey[4];

        private static Vector3? checkpointPos;

        private static bool checkpointTeleportAntiRepeat = false;

        private static bool foundPlayer = false;

        private static int btnTagSoundCooldown = 0;

        private static float timeSinceLastChange = 0f;

        private static float myVarY1 = 0f;

        private static float myVarY2 = 0f;

        private static bool gain = false;

        private static bool less = false;

        private static bool reset = false;

        private static bool fastr = false;

        private static float monkeScale = 1f;

        private static float ArmScale = 1f;


        private static bool speed1 = true;

        private static float gainSpeed = 1f;

        private static int pageSize = 4;

        private static int pageNumber = 0;

        public static bool triggerpress2 { get; private set; }

        public static string version = "2"; // Update whenever Update : - )


        private static void Prefix()
        {
            try
            {
                if (!maxJumpSpeed.HasValue)
                {
                    maxJumpSpeed = GorillaLocomotion.Player.Instance.maxJumpSpeed;
                }
                if (!jumpMultiplier.HasValue)
                {
                    jumpMultiplier = GorillaLocomotion.Player.Instance.jumpMultiplier;
                }
                if (!maxArmLengthInitial.HasValue)
                {
                    maxArmLengthInitial = GorillaLocomotion.Player.Instance.maxArmLength;
                    leftHandOffsetInitial = GorillaLocomotion.Player.Instance.leftHandOffset;
                    rightHandOffsetInitial = GorillaLocomotion.Player.Instance.rightHandOffset;
                }
                List<InputDevice> list = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list);
                list[0].TryGetFeatureValue(CommonUsages.secondaryButton, out gripDown);
                if (gripDown && menu == null)
                {
                    Draw();
                    if (reference == null)
                    {
                        reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        reference.transform.parent = GorillaLocomotion.Player.Instance.rightControllerTransform;
                        reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
                        reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    }
                }
                else if (!gripDown && menu != null)
                {
                    UnityEngine.Object.Destroy(menu);
                    menu = null;
                    UnityEngine.Object.Destroy(reference);
                    reference = null;
                }
                if (gripDown && menu != null)
                {
                    menu.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                    menu.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                }
                if (buttonsActive[0] == true)
                {
                    PhotonNetwork.Disconnect();
                }
                if (buttonsActive[1] == true)
                {
                    PhotonNetwork.JoinRandomOrCreateRoom();
                }
                if (buttonsActive[2] == true)
                {
                    ThumbDisconnect();
                }
                if (buttonsActive[3] == true)
                {
                    ProcessPlatformMonke();
                }
                if (buttonsActive[4] == true)
                {
                    if (GorillaLocomotion.Player.Instance.slideControl == 0f)
                    {
                        GorillaLocomotion.Player.Instance.slideControl = 1f;
                    }
                    else
                    {
                        GorillaLocomotion.Player.Instance.slideControl = 0f;
                    }
                }
                if (buttonsActive[5] == true)
                {
                    ProcessGhostMonke();
                }
                if (buttonsActive[6] == true)
                {
                    ProcessNoClip();
                    bool flag11 = false;
                    bool flag12 = false;
                    list = new List<InputDevice>();
                    InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
                    list[0].TryGetFeatureValue(CommonUsages.primaryButton, out flag11);
                    list[0].TryGetFeatureValue(CommonUsages.secondaryButton, out flag12);
                    if (flag11)
                    {
                        GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 20f;
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        if (!flying)
                        {
                            flying = true;
                        }
                    }
                    else if (flying)
                    {
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 50f;
                        flying = false;
                    }
                    if (flag12)
                    {
                        if (!gravityToggled && GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.useGravity)
                        {
                            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.useGravity = false;
                            gravityToggled = true;
                        }
                        else if (!gravityToggled && !GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.useGravity)
                        {
                            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.useGravity = true;
                            gravityToggled = true;
                        }
                    }
                    else
                    {
                        gravityToggled = false;
                    }
                }
                if (buttonsActive[7] == true)
                {
                    IronMonkePlugin.FixedUpdate(true);
                }
                else
                {
                    IronMonkePlugin.FixedUpdate(false);
                }
                if (buttonsActive[8] == true)
                {
                    ProcessNoClip();
                }
                if (buttonsActive[9] == true)
                { headspin(true); }
                else
                { headspin(false); }
                if (buttonsActive[10] == true)
                {
                    fastmonke(true);
                    bigmonke(true);
                    IronMonkePlugin.FixedUpdate(true);

                }
                else
                {
                    fastmonke(false);
                    bigmonke(false);
                    IronMonkePlugin.FixedUpdate(false);
                }
                if (buttonsActive[11] == true)
                {
                    longarms(true);
                }
                else
                {
                    longarms(false);
                }
                if (buttonsActive[12] == true)
                {
                    TriggerToUpsideDownHead();
                }
                if (buttonsActive[13] == true)
                {
                    RigUnderFloor();
                }
                if (buttonsActive[14] == true)
                {
                    Physics.gravity = new Vector3(0f, -3f, 0f);
                }
                else
                {
                    Physics.gravity = new Vector3(0f, -9.81f, 0f);
                }
                if (buttonsActive[15] == true)
                {
                    Tracers();
                }
                if (buttonsActive[16] == true)
                {
                    GrabAllIDs();
                }
                if (buttonsActive[17] == true)
                {
                    ProcessTeleportGun();
                }
                if (buttonsActive[18] == true)
                {
                    masterbuddy();
                }
                if (buttonsActive[19] == true)
                {
                    BypassName("KKK");
                }
                if (buttonsActive[20] == true)
                {
                    BypassName("Kosmos");
                }
                if (buttonsActive[21] == true)
                {
                    BypassName("𒐫");
                }
                if (buttonsActive[22] == true)
                { OtherMods.NoTapCoolDown(true); }
                else
                { OtherMods.NoTapCoolDown(false); }
                if (buttonsActive[23] == true)
                {
                    BoxEsp boxesp = new BoxEsp();
                    boxesp.Update();
                }
                if (buttonsActive[24] == true)
                {
                    trapallmodders();
                }
                if (buttonsActive[25] == true)
                {
                    bigandsmall();
                }
                if (buttonsActive[26] == true)
                {
                    giveallwatch();
                }
                if (buttonsActive[27] == true)
                {
                    TeleportToRandomPlayer();
                }
                if (buttonsActive[28] == true)
                {
                    scaregun();
                }
                if (buttonsActive[29] == true)
                {
                    OtherMods.allCosmetics();
                }
                if (buttonsActive[30] == true)
                {
                    ProcessTagAll();
                }
                if (buttonsActive[31] == true)
                {
                    ProcessTagGun();
                }
                if (buttonsActive[32] == true)
                {
                    OtherMods.TagAura();
                }
                if (buttonsActive[33] == true)
                {
                    OtherMods.Nametags();
                }
                if (buttonsActive[34] == true)
                {
                    TPtoStump(true);
                }
                else
                {
                    TPtoStump(false);
                }
                if (buttonsActive[35] == true)
                {
                    OpMods.SwimInAir();
                }
                if (buttonsActive[36] == true)
                {
                    SlingshotAll();
                }
                if (buttonsActive[37] == true)
                {
                    giveslingshot();
                }
                if (buttonsActive[38] == true)
                {
                    OpMods.WaterSpam();
                }
                if (buttonsActive[39] == true)
                {
                    OpMods.WaterSpamGun();
                }
                if (buttonsActive[40] == true)
                {
                    
                }
                if (buttonsActive[41] == true)
                {
                    
                }
                if (buttonsActive[42] == true)
                {

                }
                if (buttonsActive[43] == true)
                {

                }
                if (buttonsActive[44] == true)
                { FpsCounter(true); }
                else
                { FpsCounter(false); }
                if (buttonsActive[45] == true)
                {
                    OpMods.watergun();
                }
                if (buttonsActive[46] == true)
                {
                    OpMods.WaterSpam();
                }
                if (buttonsActive[47] == true)
                {

                }
                if (buttonsActive[48] == true)
                {
                    SpinMonke();
                }
                if (buttonsActive[49] == true)
                {
                    HeavenMonke();
                }
                if (buttonsActive[50] == true)
                {
                    SpinMonkeV2();
                }
                if (buttonsActive[51] == true)
                {
                    RapeGun();
                }
                if (buttonsActive[52] == true)
                {
                    SpinMonkeAroundGun();
                }
                if (buttonsActive[53] == true)
                {
                    // Follow Movement Gun
                }
                if (buttonsActive[54] == true)
                {
                    OpMods.TrampolineSpeed();
                }
                if (buttonsActive[55] == true)
                {

                }
                if (buttonsActive[56] == true)
                {
                    OtherMods.NoTagOnJoin();
                }
                if (buttonsActive[57] == true)
                {
                    OtherMods.LowQuality(true);
                }
                else
                {
                    OtherMods.LowQuality(false);
                }
                if (buttonsActive[58] == true)
                {
                    OpMods.watergunANYWHERE();
                }







                if (btnCooldown > 0 && Time.frameCount > btnCooldown)
                {
                    btnCooldown = 0;
                    buttonsActive[7] = false;
                    UnityEngine.Object.Destroy(menu);
                    menu = null;
                    Draw();
                }
                if (soundCooldown > 0 && Time.frameCount > soundCooldown)
                {
                    soundCooldown = 0;
                    buttonsActive[14] = false;
                    UnityEngine.Object.Destroy(menu);
                    menu = null;
                    Draw();
                }
                if (btnTagSoundCooldown > 0 && Time.frameCount > btnTagSoundCooldown)
                {
                    btnTagSoundCooldown = 0;
                    buttonsActive[14] = false;
                    UnityEngine.Object.Destroy(menu);
                    menu = null;
                    Draw();
                }
                if (bigMonkeCooldown > 0 && Time.frameCount > bigMonkeCooldown)
                {
                    bigMonkeCooldown = 0;
                }
                if (ghostMonkeCooldown > 0 && Time.frameCount > ghostMonkeCooldown)
                {
                    ghostMonkeCooldown = 0;
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("KosmosModMenuError.log", ex.ToString());
            }
        }




        static bool retard = true;
        private static void RigUnderFloor()
        {
            if (retard)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.myVRRig.transform.position = GorillaLocomotion.Player.Instance.transform.position + new Vector3(0f, -10f, 0f);
                retard = false;
            }
            if (!retard)
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }




        private static void RapeGun()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, list);

            if (list.Count > 0)
            {
                list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
                list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            }

            if (flag2)
            {
                RaycastHit raycastHit;
                bool flag4 = Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit) && pointer == null;
                if (flag4)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Object.Destroy(pointer.GetComponent<Rigidbody>());
                    Object.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }

                pointer.transform.position = raycastHit.point;

                bool flag5 = flag && raycastHit.collider.GetComponentInParent<PhotonView>() != null;
                if (flag5)
                {
                    Photon.Realtime.Player targetPlayer = raycastHit.collider.GetComponentInParent<PhotonView>().Owner;
                    Vector3 rigPosition = FindVRRigForPlayer(targetPlayer).transform.position;
                    Vector3 forwardDirection = FindVRRigForPlayer(targetPlayer).transform.forward;
                    float distance = -1f;
                    Vector3 targetrigposthing = FindVRRigForPlayer(targetPlayer).transform.position;
                    GorillaTagger.Instance.myVRRig.enabled = false;
                    float lerpFactor = 0.1f; // Adjust the lerp factor as needed
                    Vector3 targetPosition = targetrigposthing + (maxDistance * forwardDirection);
                    Vector3 newPosition = Vector3.Lerp(GorillaTagger.Instance.myVRRig.transform.position, targetPosition, lerpFactor);
                    GorillaTagger.Instance.myVRRig.transform.position = newPosition;
                    GorillaTagger.Instance.myVRRig.transform.rotation = FindVRRigForPlayer(targetPlayer).transform.rotation;

                }
            }
            else
            {
                bool flag6 = pointer != null && PhotonNetwork.InRoom;
                if (flag6)
                {
                    GorillaTagger.Instance.myVRRig.enabled = true;
                    Object.Destroy(pointer);
                    pointer = null;
                }

                bool flag7 = pointer != null && !PhotonNetwork.InRoom;
                if (flag7)
                {
                    Object.Destroy(pointer);
                    pointer = null;
                }
            }
        }


        public static VRRig GetClosestVrrig()
        {
            VRRig result = null;
            float num = float.PositiveInfinity;
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
            {
                bool flag = player != PhotonNetwork.LocalPlayer;
                if (flag)
                {
                    VRRig vrrig = FindVRRigForPlayer(player);
                    Vector3 vector = vrrig.transform.position - GorillaTagger.Instance.myVRRig.transform.position;
                    bool flag2 = vector.magnitude < num;
                    if (flag2)
                    {
                        result = vrrig;
                        num = vector.magnitude;
                    }
                }
            }
            return result;
        }


        private static void NOTHING()
        {
            bool freeze;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out freeze);
            if (freeze)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;

                float rotationSpeed = 1f;
                float radius = 3f;

                Vector3 center = GorillaLocomotion.Player.Instance.bodyCollider.transform.position;
                float angle = rotationSpeed * Time.time;
                Vector3 newPosition = center + new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * radius;

                GorillaTagger.Instance.myVRRig.transform.position = newPosition;
                GorillaTagger.Instance.myVRRig.transform.rotation *= Quaternion.Euler(0f, 6f, 0f);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
            }
        }




        private static GorillaPlayerScoreboardLine FindPlayerScoreboardLine(Photon.Realtime.Player player)
        {
            GorillaPlayerScoreboardLine[] scoreboardLines = Object.FindObjectsOfType<GorillaPlayerScoreboardLine>();

            foreach (GorillaPlayerScoreboardLine scoreboardLine in scoreboardLines)
            {
                if (scoreboardLine.linePlayer == player)
                {
                    return scoreboardLine;
                }
            }

            return null;
        }








        private static void LagServer()
        {
            UnityEngine.Transform transform = FindVRRigForPlayer1(PhotonNetwork.LocalPlayer).mainCamera.transform;
            PhotonNetwork.Instantiate("gorillaprefabs/Gorilla Player Networked", transform.position, Quaternion.identity, 0, null);
        }

        private static void SlingshotAll()
        {
            VRRig vrrig = FindVRRigForPlayer(PhotonNetwork.LocalPlayer);
            bool flag = vrrig != null;
            if (flag)
            {
                UnityEngine.Transform transform = vrrig.mainCamera.transform;
                Vector3 position = vrrig.mainCamera.transform.position;
                PhotonNetwork.Instantiate("GorillaPrefabs/Gorilla Battle Manager", position, Quaternion.identity, 0, null);
            }
        }




        private static bool DoOnce;
        private static GorillaTagger GetGorillaTaggerInstance()
        {
            return GorillaTagger.Instance;
        }
        private static void giveslingshot()
        {
            VRRig offlineVRRig = GorillaTagger.Instance.offlineVRRig;
            bool flag195 = offlineVRRig != null && !Slingshot.IsSlingShotEnabled();
            if (flag195)
            {
                CosmeticsController instance = CosmeticsController.instance;
                CosmeticsController.CosmeticItem itemFromDict = instance.GetItemFromDict("Slingshot");
                instance.ApplyCosmeticItemToSet(offlineVRRig.cosmeticSet, itemFromDict, true, false);
            }
        }
















        private static void scaregun()
        {
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
                    Object.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                if (pointer != null)
                {
                    pointer.transform.position = raycastHit.point;
                }

                if (triggerPressed)
                {
                    GorillaTagger.Instance.myVRRig.enabled = false;
                    if (pointer != null)
                    {
                        GorillaTagger.Instance.myVRRig.transform.position = pointer.transform.position;
                        GorillaTagger.Instance.offlineVRRig.transform.position = pointer.transform.position;
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                Object.Destroy(pointer);
            }
        }




        private static bool teleportationPerformed = false;
        private static void TeleportToRandomPlayer()
        {
            bool triggerPressed = false;
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, devices);
            devices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

            if (triggerPressed && !teleportationPerformed)
            {
                teleportationPerformed = true;

                GameObject gorillaVRRigs = GameObject.Find("Global/GorillaParent/GorillaVRRigs");

                if (gorillaVRRigs != null)
                {
                    UnityEngine.Transform[] vrRigTransforms = gorillaVRRigs.GetComponentsInChildren<UnityEngine.Transform>(true);

                    if (vrRigTransforms.Length > 1)
                    {
                        int randomIndex = UnityEngine.Random.Range(1, vrRigTransforms.Length);
                        UnityEngine.Transform randomObjectTransform = vrRigTransforms[randomIndex];

                        GorillaLocomotion.Player.Instance.transform.position = randomObjectTransform.position;

                        randomObjectTransform.gameObject.SetActive(false);
                    }
                }
            }
            else if (!triggerPressed)
            {
                teleportationPerformed = false;
            }
        }

        private static bool teleportationPerformed2 = false;
        private static void TeleportRigToRandomPlayer()
        {
            bool triggerPressed = false;
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, devices);
            devices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

            if (triggerPressed && !teleportationPerformed2)
            {
                teleportationPerformed2 = true;

                GameObject gorillaVRRigs = GameObject.Find("Global/GorillaParent/GorillaVRRigs");

                if (gorillaVRRigs != null)
                {
                    UnityEngine.Transform[] vrRigTransforms = gorillaVRRigs.GetComponentsInChildren<UnityEngine.Transform>(true);

                    if (vrRigTransforms.Length > 1)
                    {
                        int randomIndex = UnityEngine.Random.Range(1, vrRigTransforms.Length);
                        UnityEngine.Transform randomObjectTransform = vrRigTransforms[randomIndex];

                        GorillaTagger.Instance.myVRRig.enabled = false;
                        GorillaLocomotion.Player.Instance.transform.position = randomObjectTransform.position;

                        randomObjectTransform.gameObject.SetActive(false);
                    }
                }
            }
            else if (!triggerPressed)
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                teleportationPerformed2 = false;
            }
        }

        private static void ProcessCrashGun()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            if (flag2)
            {
                RaycastHit raycastHit = new RaycastHit();
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.position, -GorillaLocomotion.Player.Instance.rightControllerTransform.position, out raycastHit);
                {
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(0);
                        Object.Destroy(pointer.GetComponent<Rigidbody>());
                        Object.Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }
                    pointer.transform.position = raycastHit.point;
                    new Color(0f, 0f, 0f);
                    PhotonView componentInParent = raycastHit.collider.GetComponentInParent<PhotonView>();
                    if (componentInParent != null && PhotonNetwork.LocalPlayer != componentInParent.Owner)
                    {
                        GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tagHapticStrength, GorillaTagger.Instance.tagHapticDuration);
                        GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength, GorillaTagger.Instance.tagHapticDuration);
                        Photon.Realtime.Player owner = componentInParent.Owner;
                        if (flag)
                        {
                            pointer.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                            return;
                        }
                        else if (flag2)
                        {
                            pointer.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                            PhotonNetwork.DestroyPlayerObjects(owner);
                            return;
                        }
                        else
                        {
                            pointer.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                            return;
                        }
                    }
                }
            }
        }


        private static void giveallwatch()
        {
            UnityEngine.Transform transform = FindVRRigForPlayer(PhotonNetwork.LocalPlayer).mainCamera.transform;
            PhotonView.Get(PhotonNetwork.Instantiate("Gorillaprefabs/Gorilla Hunt Manager", transform.position, Quaternion.identity, 0, null));
        }

        private static void UnableToJoinLobbyGun()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            if (flag2)
            {
                RaycastHit raycastHit;
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.position, -GorillaLocomotion.Player.Instance.rightControllerTransform.position, out raycastHit);
                if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                pointer.transform.position = raycastHit.point;
                new Color(0f, 0f, 0f);
                PhotonView componentInParent = raycastHit.collider.GetComponentInParent<PhotonView>();
                if (componentInParent != null && PhotonNetwork.LocalPlayer != componentInParent.Owner)
                {
                    GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tagHapticStrength, GorillaTagger.Instance.tagHapticDuration);
                    GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength, GorillaTagger.Instance.tagHapticDuration);
                    Photon.Realtime.Player owner = componentInParent.Owner;
                    if (flag)
                    {
                        pointer.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        GorillaGameManager.instance.photonView.RPC("JoinPubWithFreinds", owner, Array.Empty<object>());
                        bool flag3 = false;
                        byte maxPlayers = 0;
                        PhotonNetwork.CurrentRoom.IsOpen = flag3;
                        PhotonNetwork.CurrentRoom.IsVisible = flag3;
                        PhotonNetwork.CurrentRoom.MaxPlayers = maxPlayers;
                        return;
                    }
                    if (flag2)
                    {
                        bool flag4 = false;
                        byte maxPlayers2 = 0;
                        PhotonNetwork.CurrentRoom.IsOpen = flag4;
                        PhotonNetwork.CurrentRoom.IsVisible = flag4;
                        PhotonNetwork.CurrentRoom.MaxPlayers = maxPlayers2;
                        return;
                    }
                }
            }
            else
            {
                UnityEngine.Object.Destroy(pointer);
                pointer = null;
            }
        }

        public static void bigandsmall()
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            List<InputDevice> list = new List<InputDevice>();
            List<InputDevice> list2 = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, list2);

            if (list.Count > 0)
                list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);

            if (list2.Count > 0)
            {
                list2[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag2);
                list2[0].TryGetFeatureValue(CommonUsages.secondaryButton, out flag3);
            }

            if (flag)
            {
                monkeScale += 0.015f;
            }

            if (flag2 && monkeScale > 0.2)
            {
                monkeScale -= 0.015f;
            }

            if (flag3)
            {
                monkeScale = 1f;
            }

            Player.Instance.scale = monkeScale;
        }














        public static void ProcessTagAll()
        {
            bool flag = btnCooldown == 0;
            if (flag)
            {
                foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                {
                    {
                        GorillaTagger.Instance.myVRRig.enabled = false;
                        GorillaTagger.Instance.myVRRig.transform.position = FindVRRigForPlayer(player).transform.position;
                        PhotonView.Get(GorillaGameManager.instance.GetComponent<GorillaGameManager>()).RPC("ReportTagRPC", player, new object[]
                        {
                    player
                        });
                        GorillaTagger.Instance.myVRRig.enabled = true;
                    }
                }
            }
        }


        private static void UnableJoinLobbyPlusKick()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            if (!flag2)
            {
                UnityEngine.Object.Destroy(pointer);
                pointer = null;
                antiRepeat = false;
                return;
            }
            RaycastHit raycastHit;
            Physics.Raycast(GorillaLocomotion.Player.Instance.rightHandFollower.position - GorillaLocomotion.Player.Instance.rightHandFollower.position, -GorillaLocomotion.Player.Instance.rightHandFollower.position, out raycastHit);
            if (pointer == null)
            {
                pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
            pointer.transform.position = raycastHit.point;
            if (!flag)
            {
                antiRepeat = false;
                return;
            }
            if (!antiRepeat)
            {
                bool flag3 = false;
                byte maxPlayers = 0;
                PhotonNetwork.CurrentRoom.IsOpen = flag3;
                PhotonNetwork.CurrentRoom.IsVisible = flag3;
                PhotonNetwork.CurrentRoom.MaxPlayers = maxPlayers;
            }
        }

        private static void TagAll()
        {
            if (btnCooldown == 0)
            {
                btnCooldown = Time.frameCount + 30;
                foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                {
                    GorillaLocomotion.Player.Instance.bodyCollider.transform.position = GorillaParent.instance.vrrigParent.transform.position;
                }
                UnityEngine.Object.Destroy(menu);
                menu = null;
                Draw();
            }
        }

        private static void POPSpammer()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                GorillaGameManager.instance.FindVRRigForPlayer(player).RPC("PlayHandTap", 0, new object[]
                {
            84,
            true,
            9999f
                });
            }
        }

        private static void ThumbDisconnect()
        {
            bool flag = false;
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out flag);
            if (flag)
            {
                PhotonNetwork.Disconnect();
            }
        }

        public static void HelpLag()
        {
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
            GorillaGameManager.instance.NewVRRig(PhotonNetwork.LocalPlayer, PhotonNetwork.LocalPlayer.ActorNumber, false);
        }



        private static void ProcessHandTapSpam()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                PhotonView photonView = GorillaGameManager.instance.FindVRRigForPlayer(player);
                if (photonView != null)
                {
                    photonView.RPC("PlayHandTap", 0, new object[]
                    {
                2,
                true,
                99999f
                    });
                }
            }
        }

        private static void ProcessCrystalTapSoundSpam()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                GorillaGameManager.instance.FindVRRigForPlayer(player).RPC("PlayHandTap", 0, new object[]
                {
            28,
            true,
            999999f
                });
            }
        }

        private static void ProcessDuckSoundSpam()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                GorillaGameManager.instance.FindVRRigForPlayer(player).RPC("PlayHandTap", 0, new object[]
                {
            76,
            true,
            999999f
                });
            }
        }











        public static void RopeFreezeGun()
        {
            bool flag;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.gripButton, out flag);
            bool flag2;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out flag2);
            bool flag3 = !flag;
            if (flag3)
            {
                Object.Destroy(pointer);
                pointer = null;
            }
            bool flag4 = flag;
            if (flag4)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(GorillaLocomotion.Player.Instance.rightHandFollower.position - GorillaLocomotion.Player.Instance.rightHandFollower.position, -GorillaLocomotion.Player.Instance.rightHandFollower.position, out raycastHit))
                {
                    bool flag5 = pointer == null;
                    if (flag5)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Object.Destroy(pointer.GetComponent<Rigidbody>());
                        Object.Destroy(pointer.GetComponent<Collider>());
                        pointer.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                    }
                    pointer.transform.position = raycastHit.point;
                    GorillaRopeSwing componentInParent = raycastHit.collider.GetComponentInParent<GorillaRopeSwing>();
                    bool flag6 = flag2;
                    if (flag6)
                    {
                        componentInParent.SetVelocity_RPC(1, new Vector3(0f, 0f, 0f), true);
                    }
                }
            }
        }



        private static void masterbuddy()
        {
            foreach (GorillaNot gorillaNot in Object.FindObjectsOfType<GorillaNot>())
            {
                if (!PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                }
                if (!gorillaNot.photonView.IsMine)
                {
                    gorillaNot.photonView.RequestOwnership();
                }
                PhotonNetwork.Destroy(gorillaNot.photonView);
                Object.Destroy(gorillaNot.gameObject);
                PhotonNetwork.SendAllOutgoingCommands();
            }
        }

        private static void ProcessMetalTapSoundSpam()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                GorillaGameManager.instance.FindVRRigForPlayer(player).RPC("PlayHandTap", 0, new object[]
                {
26,
true,
999999f
                });
            }
        }



        public static void SnowBallGun()
        {
            bool flag = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right, list);

            if (list.Count > 0 && list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag) && flag)
            {
                GameObject snowballPrefab = GameObject.Find("Global/Local VRRig/Local Gorilla Player/Holdables/SnowballRightAnchor/LMACF.").GetComponent<SnowballThrowable>().projectilePrefab;
                GameObject snowball = ObjectPools.instance.Instantiate(snowballPrefab);

                int snowballHashCode = PoolUtils.GameObjHashCode(snowball);
                GorillaGameManager gorillaGameManager = GorillaGameManager.instance;

                int projectileCount = gorillaGameManager.IncrementLocalPlayerProjectileCount();
                Vector3 position = GorillaLocomotion.Player.Instance.rightHandFollower.position;
                Vector3 velocity = -GorillaLocomotion.Player.Instance.rightHandFollower.position * Time.deltaTime * 500f;

                string rpcMethodName = "LaunchSlingshotProjectile";
                gorillaGameManager.photonView.RPC(rpcMethodName, RpcTarget.All, position, velocity, snowballHashCode, -1, false, projectileCount);
            }
        }


        private static void ProcessPillowSoundSpam()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                GorillaGameManager.instance.FindVRRigForPlayer(player).RPC("PlayHandTap", 0, new object[]
                {
4,
true,
999999f
                });
            }
        }

        private static void ProcessSnowStepSoundSpam()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                GorillaGameManager.instance.FindVRRigForPlayer(player).RPC("PlayHandTap", 0, new object[]
                {
32,
true,
999999f
                });
            }
        }

        private static void ProcessUmbrellaCloseSoundSpam()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                GorillaGameManager.instance.FindVRRigForPlayer(player).RPC("PlayHandTap", 0, new object[]
                {
65,
true,
999999f
                });
            }
        }

        private static void ProcessUmbrellaOpenSoundSpam()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                GorillaGameManager.instance.FindVRRigForPlayer(player).RPC("PlayHandTap", 0, new object[]
                {
64,
true,
999999f
                });
            }
        }


        private static void TeleportToStump()
        {
            bool flag = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left, list);

            if (list.Count > 0 && list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag) && flag)
            {
                foreach (MeshCollider meshCollider in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    meshCollider.transform.localScale = meshCollider.transform.localScale / 10000f;
                }

                Player.Instance.transform.position = new Vector3(-66.732f, 12.551f, -82.9941f);
                return;
            }

            foreach (MeshCollider meshCollider2 in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                meshCollider2.transform.localScale = meshCollider2.transform.localScale * 10000f;
            }
        }








        private static void MakeRopesGoUpward2()
        {
            string hierarchyPath = "Level/canyon/Canyon/Gameplay-Dynamic/";

            GameObject rootObject = GameObject.Find(hierarchyPath);

            if (rootObject != null)
            {
                GorillaRopeSwing[] ropeSwings = rootObject.GetComponentsInChildren<GorillaRopeSwing>(true);

                foreach (GorillaRopeSwing ropeSwing in ropeSwings)
                {
                    int parameter1 = 123;
                    Vector3 parameter2 = new Vector3(0f, 20000f, 0f);
                    bool parameter3 = true;

                    ropeSwing.SetVelocity_RPC(parameter1, parameter2, parameter3);
                }
            }
        }
        private static void MakeRopesGoUpward()
        {
            string hierarchyPath = "Level/canyon/Canyon/Gameplay-Dynamic/";

            GameObject rootObject = GameObject.Find(hierarchyPath);

            if (rootObject != null)
            {
                GorillaRopeSwing[] ropeSwings = rootObject.GetComponentsInChildren<GorillaRopeSwing>(true);

                foreach (GorillaRopeSwing ropeSwing in ropeSwings)
                {
                    int parameter1 = 6;
                    Vector3 parameter2 = new Vector3(0f, 20000f, 0f);
                    bool parameter3 = true;

                    ropeSwing.SetVelocity_RPC(parameter1, parameter2, parameter3);
                }
            }
        }


        private static void MakeRopesGoUpwardTrigger()
        {
            bool flag = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            if (flag)
            {
                string hierarchyPath = "Level/canyon/Canyon/Gameplay-Dynamic/";

                GameObject rootObject = GameObject.Find(hierarchyPath);

                if (rootObject != null)
                {
                    GorillaRopeSwing[] ropeSwings = rootObject.GetComponentsInChildren<GorillaRopeSwing>(true);

                    foreach (GorillaRopeSwing ropeSwing in ropeSwings)
                    {
                        int parameter1 = 6;
                        Vector3 parameter2 = new Vector3(0f, 20000f, 0f);
                        bool parameter3 = true;

                        ropeSwing.SetVelocity_RPC(parameter1, parameter2, parameter3);
                    }
                }
            }
        }




        private static void GhostMonkeUpdated()
        {
            GorillaLocomotion.Player.Instance.enabled = false;
        }




        private static void FpsCounter(bool enable)
        {
            GameObject debugCanvas = GameObject.Find("Player VR Controller/GorillaPlayer/TurnParent/Main Camera/DebugCanvas/");

            if (debugCanvas != null)
            {
                Text debugText = debugCanvas.GetComponentInChildren<Text>();

                if (debugText != null)
                {
                    if (enable && !PhotonNetwork.InRoom)
                    {
                        debugCanvas.SetActive(true);
                        debugCanvas.transform.localPosition = new Vector3(0.1382f, 0.0745f, 0.4f);
                        debugCanvas.transform.localRotation = Quaternion.Euler((float)3.4165, (float)15.3476, 0f);
                        int fps = (int)(1f / Time.smoothDeltaTime);
                        debugText.text = "FPS: " + fps.ToString();
                    }
                    else if (enable && PhotonNetwork.InRoom)
                    {
                        debugCanvas.SetActive(true);
                        debugCanvas.transform.localPosition = new Vector3(0.1382f, 0.0745f, 0.4f);
                        debugCanvas.transform.localRotation = Quaternion.Euler(12, 0, 4);
                        int fps = (int)(1f / Time.smoothDeltaTime);
                        int ping = PhotonNetwork.GetPing();
                        debugText.text = "FPS: " + fps.ToString() + "\nPing: " + ping.ToString() + "ms";
                    }
                    else
                    {
                        debugCanvas.SetActive(false);
                    }
                }
            }
        }






        private static void FreezeBug(bool enable)
        {
            if (enable)
            {
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().maxNaturalSpeed = 0.1f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobingSpeed = 0.1f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobMagnintude = 0.1f;
            }
            else
            {
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().maxNaturalSpeed = 1f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobingSpeed = 3f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobMagnintude = 0.3f;
            }
        }
        private static void FastBug(bool enable)
        {
            if (enable)
            {
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().maxNaturalSpeed = 5f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobingSpeed = 6f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobMagnintude = 0.6f;
            }
            else
            {
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().maxNaturalSpeed = 1f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobingSpeed = 3f;
                GameObject.Find("Floating Bug Holdable").GetComponent<ThrowableBug>().bobMagnintude = 0.3f;
            }
        }






        public static void CheckVersion()
        {
            using (WebClient client = new WebClient())
            {
                if (!client.DownloadString("https://pastebin.com/raw/VUL9VSHX").Contains(version))
                {
                    Application.OpenURL("https://github.com/realkosmoss/Kosmos-Mod-Menu");
                    {
                        for (int i = 0; i < GorillaComputer.instance.levelScreens.Length; i++)
                        {
                            Material material = new Material(Shader.Find("Standard"));
                            material.color = Color.black;
                            string Versionyes = version;
                            string newText = $"Thanks For Picking Kosmos Mod Menu!\nStatus: Probably Detected!\nYou Are Using An Outdated Version!\nCurrent Version: {Versionyes} Latest Version: {client.DownloadString("https://pastebin.com/raw/VUL9VSHX")}\n Please Update For Your Accounts Safety!";
                            GorillaComputer.instance.levelScreens[i].goodMaterial = material;
                            bool activeSelf = GameObject.Find("Level/lower level").activeSelf;
                            if (activeSelf)
                            {
                                GameObject.Find("Level/lower level/mirror (1)").SetActive(true);
                                GameObject.Find("Level/lower level/StaticUnlit/motdscreen").GetComponent<Renderer>().material = material;
                                GameObject.Find("Level/lower level/UI/-- PhysicalComputer UI --/monitor").GetComponent<Renderer>().material = material;
                                GameObject.Find("Level/lower level/StaticUnlit/screen").GetComponent<Renderer>().material = material;
                                GameObject.Find("Level/lower level/UI/CodeOfConduct").GetComponent<Text>().text = "[<color=yellow>KOSMOS NEWS</color>]";
                                GameObject.Find("Level/lower level/UI/CodeOfConduct/COC Text").GetComponent<Text>().text = newText;
                            }
                        }
                    }
                }
            }
        }


        private static void GiveSlingshot()
        {
            WardrobeItemButton[] array = (from g in Object.FindObjectsOfType(typeof(WardrobeItemButton))
                                          select g as WardrobeItemButton).ToArray<WardrobeItemButton>();
            foreach (WardrobeItemButton wardrobeItemButton in array)
            {
                if (wardrobeItemButton.currentCosmeticItem.itemName == "Slingshot")
                {
                    wardrobeItemButton.controlledModel.gameObject.SetActive(false);
                }
                else
                {
                    wardrobeItemButton.controlledModel.gameObject.SetActive(true);
                }
            }
        }

        private static void SpinMonke()
        {
            bool freeze;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out freeze);
            if (freeze)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.myVRRig.transform.position = GorillaLocomotion.Player.Instance.bodyCollider.transform.position + new Vector3(0f, 0.3f, 0f);
                GorillaTagger.Instance.myVRRig.transform.rotation *= Quaternion.Euler(0f, 6f, 0f);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        private static void HeavenMonke()
        {
            bool freeze;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out freeze);
            if (freeze)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.myVRRig.transform.position += new Vector3(0f, 0.15f, 0f);
                GorillaTagger.Instance.myVRRig.transform.rotation *= Quaternion.Euler(0f, 4f, 0f);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        private static void SpinMonkeV2()
        {
            bool freeze;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out freeze);
            if (freeze)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                float rotationSpeed = 1f;
                float radius = 3f;

                Vector3 center = GorillaLocomotion.Player.Instance.bodyCollider.transform.position;
                float angle = rotationSpeed * Time.time;
                Vector3 newPosition = center + new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * radius;

                GorillaTagger.Instance.myVRRig.transform.position = newPosition;



                GorillaTagger.Instance.myVRRig.transform.rotation *= Quaternion.Euler(0f, 6f, 0f);
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                GorillaTagger.Instance.offlineVRRig.enabled = false;
            }
        }
        private static void SpinMonkeAroundGun()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, list);

            if (list.Count > 0)
            {
                list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
                list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            }

            if (flag2)
            {
                RaycastHit raycastHit;
                bool raycastSuccess = Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                if (raycastSuccess)
                {
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Object.Destroy(pointer.GetComponent<Rigidbody>());
                        Object.Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }
                    pointer.transform.position = raycastHit.point;

                    if (flag && flag2)
                    {
                        GorillaTagger.Instance.myVRRig.enabled = false;

                        float rotationSpeed = 1f;
                        float radius = 3f;

                        Vector3 center = GorillaLocomotion.Player.Instance.bodyCollider.transform.position;
                        float angle = rotationSpeed * Time.time;
                        Vector3 circle = pointer.transform.position + new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * radius;

                        GorillaTagger.Instance.myVRRig.transform.position = circle;
                        GorillaTagger.Instance.myVRRig.transform.rotation *= Quaternion.Euler(0f, 6f, 0f);
                    }
                }
            }
            else
            {
                // Destroy the pointer when grip button is released
                if (pointer != null)
                {
                    GorillaTagger.Instance.myVRRig.enabled = true;
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                    Object.Destroy(pointer);
                    pointer = null;
                }
            }
        }
        private static void SpinMonkeAroundGunOFFLINETEST()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, list);

            if (list.Count > 0)
            {
                list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
                list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            }

            if (flag2)
            {
                RaycastHit raycastHit;
                bool raycastSuccess = Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                if (raycastSuccess)
                {
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Object.Destroy(pointer.GetComponent<Rigidbody>());
                        Object.Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }
                    pointer.transform.position = raycastHit.point;

                    if (flag && flag2)
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = false;

                        float rotationSpeed = 1f;
                        float radius = 3f;

                        float angle = rotationSpeed * Time.time;
                        Vector3 circle = pointer.transform.position + new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * radius;

                        GorillaTagger.Instance.offlineVRRig.transform.position = circle;
                        GorillaTagger.Instance.offlineVRRig.transform.rotation *= Quaternion.Euler(0f, 6f, 0f);
                    }
                    else
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                    }
                }
            }
            else
            {
                // Destroy the pointer when grip button is released
                if (pointer != null)
                {
                    Object.Destroy(pointer);
                    pointer = null;
                }
            }
        }


        private static void StopRopes()
        {
            string[] hierarchyPaths = new string[]
            {
            "Level/canyon/Canyon/Gameplay-Dynamic/",
            };

            foreach (string hierarchyPath in hierarchyPaths)
            {
                GameObject rootObject = GameObject.Find(hierarchyPath);

                if (rootObject != null)
                {
                    GorillaRopeSwing[] ropeSwings = rootObject.GetComponentsInChildren<GorillaRopeSwing>(true);

                    foreach (GorillaRopeSwing ropeSwing in ropeSwings)
                    {
                        int parameter1 = 6;
                        Vector3 parameter2 = new Vector3(0f, 0f, 0f);
                        bool parameter3 = true;

                        ropeSwing.SetVelocity_RPC(parameter1, parameter2, parameter3);
                    }
                }
            }
        }



        private static void RopeFucker22()
        {
            bool flag = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            if (flag)
            {
                string hierarchyPath = "Level/canyon/Canyon/Gameplay-Dynamic/";

                GameObject rootObject = GameObject.Find(hierarchyPath);

                if (rootObject != null)
                {
                    GorillaRopeSwing[] ropeSwings = rootObject.GetComponentsInChildren<GorillaRopeSwing>(true);

                    foreach (GorillaRopeSwing ropeSwing in ropeSwings)
                    {
                        int parameter1 = 123;
                        Vector3 parameter2 = new Vector3(55.0f, 55.0f, 55.0f);
                        bool parameter3 = true;

                        ropeSwing.SetVelocity_RPC(parameter1, parameter2, parameter3);
                    }
                }
            }
        }
        private static void RopeFucker2()
        {
            bool flag = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            if (flag)
            {
                string hierarchyPath = "Level/canyon/Canyon/Gameplay-Dynamic/";

                GameObject rootObject = GameObject.Find(hierarchyPath);

                if (rootObject != null)
                {
                    GorillaRopeSwing[] ropeSwings = rootObject.GetComponentsInChildren<GorillaRopeSwing>(true);

                    foreach (GorillaRopeSwing ropeSwing in ropeSwings)
                    {
                        int parameter1 = 123;
                        Vector3 parameter2 = new Vector3(55.0f, 55.0f, 55.0f);
                        bool parameter3 = true;
                        ropeSwing.SetVelocity_RPC(parameter1, parameter2, parameter3);
                    }
                }
            }
        }



        private static void RopeFucker69()
        {
            string hierarchyPath = "Level/canyon/Canyon/Gameplay-Dynamic/";

            GameObject rootObject = GameObject.Find(hierarchyPath);

            if (rootObject != null)
            {
                GorillaRopeSwing[] ropeSwings = rootObject.GetComponentsInChildren<GorillaRopeSwing>(true);

                foreach (GorillaRopeSwing ropeSwing in ropeSwings)
                {
                    int parameter1 = 123;
                    Vector3 parameter2 = new Vector3(5.0f, 5.0f, 5.0f);
                    bool parameter3 = true;
                    ropeSwing.SetVelocity_RPC(parameter1, parameter2, parameter3);
                }
            }
        }
        private static void RopeFucker()
        {
            string hierarchyPath = "Level/canyon/Canyon/Gameplay-Dynamic/";

            GameObject rootObject = GameObject.Find(hierarchyPath);

            if (rootObject != null)
            {
                GorillaRopeSwing[] ropeSwings = rootObject.GetComponentsInChildren<GorillaRopeSwing>(true);

                foreach (GorillaRopeSwing ropeSwing in ropeSwings)
                {
                    int parameter1 = 123;
                    Vector3 parameter2 = new Vector3(5.0f, 5.0f, 5.0f);
                    bool parameter3 = true;
                    ropeSwing.SetVelocity_RPC(parameter1, parameter2, parameter3);
                }
            }
        }


        private static void BypassName(string name)
        {
            PhotonNetwork.LocalPlayer.NickName = name.ToLower();
            PlayerPrefs.SetString("playerName", name.ToLower());
            PlayerPrefs.Save();
            GorillaComputer.instance.offlineVRRigNametagText.text = name.ToLower();
        }

        public static void ProcessTagGun()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, list);

            if (list.Count > 0)
            {
                list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
                list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            }

            if (flag2)
            {
                RaycastHit raycastHit;
                bool raycastSuccess = Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                if (raycastSuccess)
                {
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Object.Destroy(pointer.GetComponent<Rigidbody>());
                        Object.Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }
                    pointer.transform.position = raycastHit.point;

                    bool flag5 = flag && raycastHit.collider.GetComponentInParent<PhotonView>() != null;
                    if (flag5)
                    {
                        GorillaTagger.Instance.myVRRig.enabled = false;
                        GorillaTagger.Instance.myVRRig.transform.position = FindVRRigForPlayer(raycastHit.collider.GetComponentInParent<PhotonView>().Owner).transform.position;
                        OtherMods.instatag();
                        PhotonView.Get(GorillaGameManager.instance.GetComponent<GorillaGameManager>()).RPC("ReportTagRPC", RpcTarget.All, raycastHit.collider.GetComponentInParent<PhotonView>().Owner);
                        GorillaTagger.Instance.myVRRig.enabled = true;
                    }
                }
            }
            else
            {
                // Destroy the pointer when grip button is released
                if (pointer != null)
                {
                    Object.Destroy(pointer);
                    pointer = null;
                }
            }
        }

        public static void DeleteGun()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, list);

            if (list.Count > 0)
            {
                list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
                list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            }

            if (flag2)
            {
                RaycastHit raycastHit;
                bool raycastSuccess = Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                if (raycastSuccess)
                {
                    if (pointer == null)
                    {
                        pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Object.Destroy(pointer.GetComponent<Rigidbody>());
                        Object.Destroy(pointer.GetComponent<SphereCollider>());
                        pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }
                    pointer.transform.position = raycastHit.point;

                    bool flag5 = flag && raycastHit.collider.GetComponentInParent<PhotonView>() != null;
                    if (flag5)
                    {
                        UnityEngine.Object.Destroy(FindVRRigForPlayer(raycastHit.collider.GetComponentInParent<PhotonView>().Owner));
                    }
                }
            }
            else
            {
                // Destroy the pointer when grip button is released
                if (pointer != null)
                {
                    Object.Destroy(pointer);
                    pointer = null;
                }
            }
        }

        private static void DestroyALL()
        {
            PhotonNetwork.Destroy(GameObject.Find("RigCache"));
        }

        public static void TPtoTutorial(bool enable)
        {
            if (enable)
            {
                Vector3 targetPosition = new Vector3(-144.6285f, 30.4832f, -70.1572f);

                foreach (MeshCollider meshCollider in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    meshCollider.enabled = false;
                }

                GorillaLocomotion.Player.Instance.transform.position = targetPosition;
            }
            else
            {
                foreach (MeshCollider meshCollider2 in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    meshCollider2.enabled = true;
                }
            }
        }

        public static void TPtoStump(bool enable)
        {
            if (enable)
            {
                foreach (MeshCollider meshCollider in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    meshCollider.enabled = false;
                }

                GorillaLocomotion.Player.Instance.transform.position = new Vector3(-66.7623f, 11.5f, -82.4813f);
            }
            else
            {
                foreach (MeshCollider meshCollider2 in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    meshCollider2.enabled = true;
                }
            }
        }

        private static void GrabAllIDs()
        {
            string text = "========== NEW ROOM! ==========";
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                text = string.Concat(new string[]
                {
            text,
            "\nPlayer Name: ",
            player.NickName,
            " , Player ID: ",
            player.UserId
                });
            }
            text += "\n====================\n";
            File.AppendAllText("PlayerIDs.txt", text);
        }


        private static void Tracers()
        {
            foreach (VRRig vrrig in (VRRig[])Object.FindObjectsOfType(typeof(VRRig)))
            {
                if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                {
                    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    Object.Destroy(gameObject.GetComponent<BoxCollider>());
                    Object.Destroy(gameObject.GetComponent<Rigidbody>());
                    Object.Destroy(gameObject.GetComponent<Collider>());
                    Object.Destroy(gameObject.GetComponent<MeshCollider>());
                    gameObject.transform.rotation = Quaternion.identity;
                    gameObject.transform.localScale = new Vector3(0.04f, 200f, 0.04f);
                    gameObject.transform.position = vrrig.transform.position;
                    gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    Object.Destroy(gameObject, Time.deltaTime);
                }
            }
        }


        public static void bigmonke(bool enable)
        {
            bool flag = false;
            if (enable)
            {
                flag = true;
                GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(2f, 2f, 2f);
            }
            else if (!flag)
            {
                flag = false;
                GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        public static void longarms(bool enable)
        {
            bool flag = false;
            if (enable)
            {
                flag = true;
                GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            else if (!flag && enable)
            {
                flag = false;
                GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }


        private static void headspin(bool enable)
        {

        }
        private static void antireport()
        {
            Object.Destroy(GameObject.Find("Global/Photon Manager/GorillaReporter"));
        }




        private static void fastmonke(bool enable)
        {
            if (enable)
            {
                GorillaLocomotion.Player.Instance.maxJumpSpeed = 100f;
                GorillaLocomotion.Player.Instance.jumpMultiplier = 1.15f;
            }
            else
            {
                GorillaLocomotion.Player.Instance.maxJumpSpeed = 5.15f;
                GorillaLocomotion.Player.Instance.jumpMultiplier = 1.15f;
            }
        }
        public static VRRig FindVRRigForPlayer(Photon.Realtime.Player player)
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (!vrrig.isOfflineVRRig && vrrig.GetComponent<PhotonView>().Owner == player)
                {
                    return vrrig;
                }
            }
            return null;
        }
        private static void ProcessNoClip()
        {
            bool flag = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            if (flag)
            {
                if (!flag2)
                {
                    foreach (MeshCollider meshCollider in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        meshCollider.transform.localScale = meshCollider.transform.localScale / 10000f;
                    }
                    flag2 = true;
                    flag1 = false;
                    return;
                }
            }
            else if (!flag1)
            {
                foreach (MeshCollider meshCollider2 in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    meshCollider2.transform.localScale = meshCollider2.transform.localScale * 10000f;
                }
                flag1 = true;
                flag2 = false;
            }
        }

        private static void TriggerToUpsideDownHead()
        {
            bool flag = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            if (flag)
            {
                GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 180f;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 0f;
            }
        }

        private static void BanGun()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            if (!flag2)
            {
                UnityEngine.Object.Destroy(pointer);
                pointer = null;
                return;
            }
            RaycastHit raycastHit;
            Physics.Raycast(GorillaLocomotion.Player.Instance.rightHandFollower.position - GorillaLocomotion.Player.Instance.rightHandFollower.position, -GorillaLocomotion.Player.Instance.rightHandFollower.position, out raycastHit);
            if (pointer == null)
            {
                pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
            pointer.transform.position = raycastHit.point;
            PhotonView componentInParent = raycastHit.collider.GetComponentInParent<PhotonView>();
            if (!(componentInParent != null) || PhotonNetwork.LocalPlayer == componentInParent.Owner)
            {
                pointer.GetComponent<Renderer>().material.color = Color.white;
                return;
            }
            GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tagHapticStrength, GorillaTagger.Instance.tagHapticDuration);
            GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength, GorillaTagger.Instance.tagHapticDuration);
            Photon.Realtime.Player owner = componentInParent.Owner;
            if (flag)
            {
                bool isMasterClient = PhotonNetwork.LocalPlayer.IsMasterClient;
                pointer.GetComponent<Renderer>().material.color = Color.green;
                PhotonNetwork.SetMasterClient(owner);
                PhotonNetwork.CloseConnection(owner);
                return;
            }
            pointer.GetComponent<Renderer>().material.color = Color.white;
        }



        private static int rNode = 5;
        private static int lNode = 4;

        private void OnEnable()
        {
            SizeManager sizeManager;
            if (GorillaLocomotion.Player.Instance.TryGetComponent<SizeManager>(out sizeManager))
            {
                InputDevices.GetDeviceAtXRNode((XRNode)rNode).TryGetFeatureValue(CommonUsages.primaryButton, out bool flag);
                InputDevices.GetDeviceAtXRNode((XRNode)lNode).TryGetFeatureValue(CommonUsages.primaryButton, out bool flag2);
                if (flag)
                {
                    sizeManager.enabled = false;
                    StartCoroutine(sizeup());
                }
                else
                {
                    sizeManager.enabled = true;
                }
                if (flag2)
                {
                    sizeManager.enabled = false;
                    StartCoroutine(sizedown());
                }
                else
                {
                    sizeManager.enabled = true;
                }
            }
        }

        private void StartCoroutine(IEnumerator enumerator)
        {
            throw new NotImplementedException();
        }

        private IEnumerator sizedown()
        {
            float scale = GorillaLocomotion.Player.Instance.scale;
            scale -= 0.25f;
            GorillaLocomotion.Player.Instance.scale = scale;
            yield return new WaitForSeconds(0.1f);
        }

        private IEnumerator sizeup()
        {
            float scale = GorillaLocomotion.Player.Instance.scale;
            scale += 0.25f;
            GorillaLocomotion.Player.Instance.scale = scale;
            yield return new WaitForSeconds(0.1f);
        }

        private void OnDisable()
        {
            GorillaLocomotion.Player.Instance.scale = 1f;
        }


        private static void ProcessBeacons()
        {
            foreach (VRRig vrrig in (VRRig[])UnityEngine.Object.FindObjectsOfType(typeof(VRRig)))
            {
                if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                {
                    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Collider>());
                    gameObject.transform.rotation = Quaternion.identity;
                    gameObject.transform.localScale = new Vector3(0.04f, 200f, 0.04f);
                    gameObject.transform.position = vrrig.transform.position;
                    gameObject.GetComponent<MeshRenderer>().material = vrrig.mainSkin.material;
                    UnityEngine.Object.Destroy(gameObject, Time.deltaTime);
                }
            }
        }

        private static void FireBallsGun()
        {
            bool flag = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right, list);

            if (list.Count > 0 && list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag) && flag)
            {
                GameObject slingshotPrefab = GameObject.Find("Global/Local VRRig/Local Gorilla Player/rig/body/Slingshot Chest Snap/DropZoneAnchor/Slingshot Anchor/HIGH TECH SLINGSHOT").GetComponent<Slingshot>().projectilePrefab;
                GameObject projectile = ObjectPools.instance.Instantiate(slingshotPrefab);

                int projectileHashCode = PoolUtils.GameObjHashCode(projectile);
                GorillaGameManager gorillaGameManager = GorillaGameManager.instance;

                int projectileCount = gorillaGameManager.IncrementLocalPlayerProjectileCount();
                Vector3 position = GorillaLocomotion.Player.Instance.rightHandFollower.position;
                Vector3 velocity = -GorillaLocomotion.Player.Instance.rightHandFollower.position * Time.deltaTime * 1000f;

                string rpcMethodName = "LaunchSlingshotProjectile";
                gorillaGameManager.photonView.RPC(rpcMethodName, RpcTarget.All, position, velocity, projectileHashCode, -1, false, projectileCount);
            }
        }



        private static void ProcessKickGun2()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            if (flag2)
            {
                RaycastHit raycastHit;
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightHandFollower.position - GorillaLocomotion.Player.Instance.rightHandFollower.position, -GorillaLocomotion.Player.Instance.rightHandFollower.position, out raycastHit);
                if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(KosmosModMenu.pointer.GetComponent<SphereCollider>());
                    KosmosModMenu.pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                KosmosModMenu.pointer.transform.position = raycastHit.point;
                PhotonView componentInParent = raycastHit.collider.GetComponentInParent<PhotonView>();
                if (componentInParent != null && PhotonNetwork.LocalPlayer != componentInParent.Owner)
                {
                    GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tagHapticStrength, GorillaTagger.Instance.tagHapticDuration);
                    GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength, GorillaTagger.Instance.tagHapticDuration);
                    Photon.Realtime.Player owner = componentInParent.Owner;
                    if (flag)
                    {
                        {
                            try
                            {
                                if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                                {
                                    GorillaGameManager.instance.currentMasterClient = PhotonNetwork.LocalPlayer;
                                    PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                                }
                                PhotonNetwork.CloseConnection(owner);
                            }
                            catch (Exception ex)
                            {
                                File.AppendAllText("KosmosKickGunError.log", string.Concat(new string[]
                                {
                            ex.ToString(),
                            Environment.NewLine,
                            ex.Message,
                            Environment.NewLine,
                            ex.StackTrace,
                            Environment.NewLine,
                            ex.Source
                                }));
                            }
                        }
                    }
                }
            }
            else
            {
                UnityEngine.Object.Destroy(KosmosModMenu.pointer);
                KosmosModMenu.pointer = null;
            }
        }


        private static bool toggleFlying = false;
        private static object gameObject;
        private static object raycastHit;

        private static void ProcessSuperMonke()
        {
            bool flying = toggleFlying;
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.primaryButton, out flag);
            list[0].TryGetFeatureValue(CommonUsages.secondaryButton, out flag2);
            if (flag)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.rightHandFollower.position * Time.deltaTime * 12f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                {
                    flying = true;
                }
            }
            else if (flying)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = GorillaLocomotion.Player.Instance.rightHandFollower.position * Time.deltaTime * 36f;
                flying = false;
            }
            if (flag2)
            {
                toggleFlying = !toggleFlying; // toggle the value of toggleFlying
                return;
            }
        }

        public static void trapallmodders()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                gameObject.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                gameObject.transform.rotation = vrrig.head.headTransform.rotation;
                object[] array = new object[]
                {
            vrrig.head.headTransform.position,
            vrrig.head.headTransform.rotation
                };
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                {
                    Receivers = 0
                };
                PhotonNetwork.RaiseEvent(69, array, raiseEventOptions, SendOptions.SendReliable);
                ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
                colorChanger.colors = new Gradient
                {
                    colorKeys = colorKeysPlatformMonke
                };
                colorChanger.Start();
            }
        }



        private static bool trapmoddergunonce;
        private static GameObject playertocrash = null;
        private static void ProcessTrapModderGun()
        {
            bool flag;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            bool flag2;
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            bool flag3;
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primaryButton, out flag3);
            RaycastHit raycastHit;
            bool flag4 = Physics.Raycast(GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position, GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.up * 20f, out raycastHit, float.PositiveInfinity, LayerMask.GetMask(new string[] { "Gorilla Tag Collider" }));
            RaycastHit raycastHit2;
            bool flag5 = Physics.Raycast(GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position, GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.up * 20f, out raycastHit2);
            if (flag2)
            {
                if (flag4)
                {
                    if (flag)
                    {
                        Player.Instance.enabled = false;
                        playertocrash = raycastHit.transform.gameObject;
                        Player.Instance.enabled = true;
                    }
                }
                else if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    pointer.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    Object.Destroy(pointer.GetComponent<Rigidbody>());
                    Object.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                pointer.transform.position = raycastHit2.point;
                if (playertocrash != null)
                {
                    object[] array = new object[]
                    {
                playertocrash.transform.position,
                playertocrash.transform.rotation
                    };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.MasterClient
                    };
                    if (!trapmoddergunonce)
                    {
                        PhotonNetwork.RaiseEvent(69, array, raiseEventOptions, SendOptions.SendReliable);
                        trapmoddergunonce = true;
                    }
                }
                if (!flag2)
                {
                    playertocrash = null;
                    trapmoddergunonce = false;
                    return;
                }
            }
            else
            {
                Object.Destroy(pointer);
                pointer = null;
                playertocrash = null;
                trapmoddergunonce = false;
            }
        }
        public static VRRig FindVRRigForPlayer1(Photon.Realtime.Player player)
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (!vrrig.isOfflineVRRig && vrrig.GetComponent<PhotonView>().Owner == player)
                {
                    return vrrig;
                }
            }
            return null;
        }

        private static Vector3 rigforplatforms;

        private static void ProcessCrashAllModders()
        {
            if (btnTagSoundCooldown == 0)
            {
                btnTagSoundCooldown = Time.frameCount + 1;
                if (!once_networking)
                {
                    PhotonNetwork.NetworkingClient.EventReceived += PlatformNetwork;
                    once_networking = true;
                }
                Photon.Realtime.Player[] playerList = PhotonNetwork.PlayerList;
                for (int i = 0; i < playerList.Length; i++)
                {
                    VRRig vrrig = FindVRRigForPlayer1(playerList[i]);
                    int num;
                    for (int j = 0; j < 5; j = num + 1)
                    {
                        rigforplatforms = vrrig.headMesh.transform.position;
                        object[] array = new object[] { rigforplatforms };
                        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                        {
                            Receivers = 0
                        };
                        PhotonNetwork.RaiseEvent(69, array, raiseEventOptions, SendOptions.SendReliable);
                        PhotonNetwork.RaiseEvent(72, array, raiseEventOptions, SendOptions.SendReliable);
                        num = j;
                    }
                }
            }
        }

       



        private static void TagGunV2()
        {
            Color purple = new Color(0.53f, 0.26f, 1f);
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            if (flag2)
            {
                RaycastHit raycastHit;
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(0);
                    Object.Destroy(pointer.GetComponent<Rigidbody>());
                    Object.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    pointer.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
                }
                pointer.transform.position = raycastHit.point;
                new Color(0f, 0f, 0f);
                PhotonView componentInParent = raycastHit.collider.GetComponentInParent<PhotonView>();
                if (componentInParent != null && PhotonNetwork.LocalPlayer != componentInParent.Owner)
                {
                    Photon.Realtime.Player owner = (Photon.Realtime.Player)componentInParent.Owner;
                    if (flag)
                    {
                        VRRig vrrig = FindVRRigForPlayer((Photon.Realtime.Player)owner);
                        foreach (GorillaTagManager gorillaTagManager in Object.FindObjectsOfType<GorillaTagManager>())
                        {
                            pointer.GetComponent<Renderer>().material.SetColor("_Color", purple);
                            GorillaTagger.Instance.myVRRig.enabled = false;
                            GorillaTagger.Instance.rightHandTransform.transform.position = vrrig.leftHandTransform.transform.position;
                            GorillaTagger.Instance.leftHandTransform.transform.position = vrrig.rightHandTransform.transform.position;
                        }
                    }
                }
                GorillaTagger.Instance.myVRRig.enabled = true;
                FindVRRigForPlayer(PhotonNetwork.LocalPlayer);
                pointer.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
        }

        public static void AllSlingshots()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                bool flag = vrrig != null;
                if (flag)
                {
                    CosmeticsController instance = CosmeticsController.instance;
                    CosmeticsController.CosmeticItem itemFromDict = instance.GetItemFromDict("Slingshot");
                    instance.ApplyCosmeticItemToSet(vrrig.cosmeticSet, itemFromDict, true, false);
                }
            }
        }

        private static void ProcessBigMonke(bool enable)
        {
            bool flag = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            bool flag2 = KosmosModMenu.bigMonkeCooldown == 0;
            if (flag2)
            {
                KosmosModMenu.bigMonkeCooldown = Time.frameCount + 30;
                bool flag3 = flag && !KosmosModMenu.bigMonkeyEnabled;
                if (flag3)
                {
                    GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(2f, 2f, 2f);
                    KosmosModMenu.bigMonkeyEnabled = true;
                }
                else
                {
                    bool flag4 = (flag && KosmosModMenu.bigMonkeyEnabled) || !enable;
                    if (flag4)
                    {
                        GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
                        KosmosModMenu.bigMonkeyEnabled = false;
                    }
                }
            }
        }
        private static void ProcessSmallMonke(bool enable)
        {
            bool flag = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            bool flag2 = KosmosModMenu.bigMonkeCooldown == 0;
            if (flag2)
            {
                KosmosModMenu.bigMonkeCooldown = Time.frameCount + 30;
                bool flag3 = flag && !KosmosModMenu.bigMonkeyEnabled;
                if (flag3)
                {
                    GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    KosmosModMenu.bigMonkeyEnabled = true;
                }
                else
                {
                    bool flag4 = (flag && KosmosModMenu.bigMonkeyEnabled) || !enable;
                    if (flag4)
                    {
                        GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
                        KosmosModMenu.bigMonkeyEnabled = false;
                    }
                }
            }
        }



        public static bool antiRepeat;
        private static void rigGun()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            if (!flag2)
            {
                UnityEngine.Object.Destroy(pointer);
                pointer = null;
                antiRepeat = false;
                return;
            }
            RaycastHit raycastHit;
            Physics.Raycast(GorillaLocomotion.Player.Instance.rightHandFollower.position - GorillaLocomotion.Player.Instance.rightHandFollower.up, -GorillaLocomotion.Player.Instance.rightHandFollower.up, out raycastHit);
            if (pointer == null)
            {
                pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                UnityEngine.Object.Destroy(pointer.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
            pointer.transform.position = raycastHit.point;
            if (!flag)
            {
                antiRepeat = false;
                return;
            }
            if (!antiRepeat)
            {
                UnityEngine.Transform transform = FindVRRigForPlayer(PhotonNetwork.LocalPlayer).mainCamera.transform;
                PhotonNetwork.Instantiate("gorillaprefabs/Gorilla Player Networked", raycastHit.point, Quaternion.identity, 0, null);
                antiRepeat = true;
            }
        }

        private static bool _buttonPressed = false;

        private static void ProcessGhostMonke()
        {
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.secondaryButton, out bool buttonPress);
            if (buttonPress)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.enabled = false;
            }
            else
            {
                GorillaTagger.Instance.myVRRig.enabled = true;
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }



        private static void ProcessInvisibility()
        {
            bool flag = false;
            bool flag2 = false;
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list);
            list[0].TryGetFeatureValue(CommonUsages.primaryButton, out flag);
            list[0].TryGetFeatureValue(CommonUsages.secondaryButton, out flag2);
            int num = 0;
            if (flag)
            {
                GorillaTagger.Instance.myVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                return;
            }
            if (num < 15)
            {
                num++;
                GorillaTagger.Instance.myVRRig.enabled = true;
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }


        private static void FakeRigGun()
        {
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
                bool raycastSuccess = Physics.Raycast(GorillaLocomotion.Player.Instance.rightHandFollower.position - GorillaLocomotion.Player.Instance.rightHandFollower.position, -GorillaLocomotion.Player.Instance.rightHandFollower.position, out raycastHit);
                if (raycastSuccess && pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Object.Destroy(pointer.GetComponent<Rigidbody>());
                    Object.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                if (pointer != null)
                {
                    pointer.transform.position = raycastHit.point;
                }

                if (triggerPressed)
                {
                    if (pointer != null)
                    {
                        GameObject rig = GameObject.Find("Global/Local VRRig/Local Gorilla Player/");
                        rig.transform.position = pointer.transform.position;
                    }
                }
            }
            else
            {
                Object.Destroy(pointer);
            }
        }

        public static void esp(bool enable)
        {
            Dictionary<VRRig, Material> originalMaterials = new Dictionary<VRRig, Material>();

            foreach (VRRig vrrig in (VRRig[])Object.FindObjectsOfType(typeof(VRRig)))
            {
                if (enable)
                {
                    if (!originalMaterials.ContainsKey(vrrig))
                    {
                        originalMaterials[vrrig] = vrrig.mainSkin.material; // Store the original material
                    }

                    Material newMaterial = new Material(Shader.Find("GUI/Text Shader"));
                    vrrig.mainSkin.material = newMaterial; // Assign the new material
                }
                else
                {
                    if (originalMaterials.ContainsKey(vrrig))
                    {
                        vrrig.mainSkin.material = originalMaterials[vrrig]; // Restore the original material
                        originalMaterials.Remove(vrrig); // Remove the stored material from the dictionary
                    }
                }
            }
        }
















        private static void ProcessCheckPoint()
        {
            List<InputDevice> list = new List<InputDevice>();
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            list = new List<InputDevice>();
            InputDevices.GetDevices(list);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.triggerButton, out flag);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out flag2);
            list[0].TryGetFeatureValue(CommonUsages.secondaryButton, out flag3);
            if (flag2)
            {
                flag3 = false;
                if (pointer == null)
                {
                    pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(KosmosModMenu.pointer.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(pointer.GetComponent<SphereCollider>());
                    pointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                pointer.transform.position = GorillaLocomotion.Player.Instance.rightHandFollower.position;
            }
        }

        private static void ProcessTeleportGun()
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
                    if (!teleportGunAntiRepeat)
                    {
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().isKinematic = true;
                        GorillaLocomotion.Player.Instance.transform.position = hitInfo.point;
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                        GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().isKinematic = false;
                        teleportGunAntiRepeat = true;
                    }
                }
                else
                {
                    teleportGunAntiRepeat = false;
                }
            }
            else
            {
                UnityEngine.Object.Destroy(pointer);
                pointer = null;
                teleportGunAntiRepeat = false;
            }
        }

        private static void BreakGamemodeInfection()
        {
            foreach (GorillaTagManager gorillaTagManager in UnityEngine.Object.FindObjectsOfType<GorillaTagManager>())
            {
                gorillaTagManager.currentInfected.Clear();
                gorillaTagManager.InfectionEnd();
                gorillaTagManager.ClearInfectionState();
                gorillaTagManager.infectedModeThreshold = 0;
                gorillaTagManager.currentInfectedArray = new int[0];
            }
        }


        private static void ProcessPlatformMonke()
        {
            colorKeysPlatformMonke[0].color = Color.red;
            colorKeysPlatformMonke[0].time = 0f;
            colorKeysPlatformMonke[1].color = Color.green;
            colorKeysPlatformMonke[1].time = 0.3f;
            colorKeysPlatformMonke[2].color = Color.blue;
            colorKeysPlatformMonke[2].time = 0.6f;
            colorKeysPlatformMonke[3].color = Color.red;
            colorKeysPlatformMonke[3].time = 1f;
            if (!once_networking)
            {
                PhotonNetwork.NetworkingClient.EventReceived += PlatformNetwork;
                once_networking = true;
            }
            List<InputDevice> list = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out gripDown_left);
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, list);
            list[0].TryGetFeatureValue(CommonUsages.gripButton, out gripDown_right);
            if (gripDown_right)
            {
                if (!once_right && jump_right_local == null)
                {
                    jump_right_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    jump_right_local.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    jump_right_local.transform.localScale = scale;
                    jump_right_local.transform.position = new Vector3(0f, -0.0075f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                    jump_right_local.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.transform.rotation;
                    object[] eventContent = new object[2]
                    {
                    new Vector3(0f, -0.0075f, 0f) + GorillaLocomotion.Player.Instance.rightControllerTransform.position,
                    GorillaLocomotion.Player.Instance.rightHandFollower.transform.rotation
                    };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others
                    };
                    PhotonNetwork.RaiseEvent(70, eventContent, raiseEventOptions, SendOptions.SendReliable);
                    once_right = true;
                    once_right_false = false;
                    ColorChanger colorChanger = jump_right_local.AddComponent<ColorChanger>();
                    Gradient gradient = new Gradient();
                    gradient.colorKeys = colorKeysPlatformMonke;
                    colorChanger.colors = gradient;
                    colorChanger.Start();
                }
            }
            else if (!once_right_false && jump_right_local != null)
            {
                UnityEngine.Object.Destroy(jump_right_local);
                jump_right_local = null;
                once_right = false;
                once_right_false = true;
                RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others
                };
                PhotonNetwork.RaiseEvent(72, null, raiseEventOptions2, SendOptions.SendReliable);
            }
            if (gripDown_left)
            {
                if (!once_left && jump_left_local == null)
                {
                    jump_left_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    jump_left_local.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    jump_left_local.transform.localScale = scale;
                    jump_left_local.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;

                    jump_left_local.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                    object[] eventContent2 = new object[2]
                    {
                    GorillaLocomotion.Player.Instance.leftControllerTransform.position,
                    GorillaLocomotion.Player.Instance.leftControllerTransform.rotation
                    };
                    RaiseEventOptions raiseEventOptions3 = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others
                    };
                    PhotonNetwork.RaiseEvent(69, eventContent2, raiseEventOptions3, SendOptions.SendReliable);
                    once_left = true;
                    once_left_false = false;
                    ColorChanger colorChanger2 = jump_left_local.AddComponent<ColorChanger>();
                    Gradient gradient2 = new Gradient();
                    gradient2.colorKeys = colorKeysPlatformMonke;
                    colorChanger2.colors = gradient2;
                    colorChanger2.Start();
                }
            }
            else if (!once_left_false && jump_left_local != null)
            {
                UnityEngine.Object.Destroy(jump_left_local);
                jump_left_local = null;
                once_left = false;
                once_left_false = true;
                RaiseEventOptions raiseEventOptions4 = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others
                };
                PhotonNetwork.RaiseEvent(71, null, raiseEventOptions4, SendOptions.SendReliable);
            }
            if (!PhotonNetwork.InRoom)
            {
                for (int i = 0; i < jump_right_network.Length; i++)
                {
                    UnityEngine.Object.Destroy(jump_right_network[i]);
                }
                for (int j = 0; j < jump_left_network.Length; j++)
                {
                    UnityEngine.Object.Destroy(jump_left_network[j]);
                }
            }
        }

        private static void PlatformNetwork(EventData eventData)
        {
            switch (eventData.Code)
            {
                case 69:
                    {
                        object[] array2 = (object[])eventData.CustomData;
                        jump_left_network[eventData.Sender] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        jump_left_network[eventData.Sender].GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                        jump_left_network[eventData.Sender].transform.localScale = scale;
                        jump_left_network[eventData.Sender].transform.position = (Vector3)array2[0];
                        jump_left_network[eventData.Sender].transform.rotation = (Quaternion)array2[1];
                        ColorChanger colorChanger2 = jump_left_network[eventData.Sender].AddComponent<ColorChanger>();
                        Gradient gradient2 = new Gradient();
                        gradient2.colorKeys = colorKeysPlatformMonke;
                        colorChanger2.colors = gradient2;
                        colorChanger2.Start();
                        break;
                    }
                case 70:
                    {
                        object[] array = (object[])eventData.CustomData;
                        jump_right_network[eventData.Sender] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        jump_right_network[eventData.Sender].GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                        jump_right_network[eventData.Sender].transform.localScale = scale;
                        jump_right_network[eventData.Sender].transform.position = (Vector3)array[0];
                        jump_right_network[eventData.Sender].transform.rotation = (Quaternion)array[1];
                        ColorChanger colorChanger = jump_right_network[eventData.Sender].AddComponent<ColorChanger>();
                        Gradient gradient = new Gradient();
                        gradient.colorKeys = colorKeysPlatformMonke;
                        colorChanger.colors = gradient;
                        colorChanger.Start();
                        break;
                    }
                case 71:
                    UnityEngine.Object.Destroy(jump_left_network[eventData.Sender]);
                    jump_left_network[eventData.Sender] = null;
                    break;
                case 72:
                    UnityEngine.Object.Destroy(jump_right_network[eventData.Sender]);
                    jump_right_network[eventData.Sender] = null;
                    break;
            }
        }



        private static void AddButton(float offset, string text)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
            gameObject.AddComponent<BtnCollider>().relatedText = text;
            int num = -1;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (text == buttons[i])
                {
                    num = i;
                    break;
                }
            }
            if (buttonsActive[num] == false)
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            }
            else if (buttonsActive[num] == true)
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            }
            GameObject gameObject2 = new GameObject();
            gameObject2.transform.parent = canvasObj.transform;
            Text text2 = gameObject2.AddComponent<Text>();
            text2.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text2.text = text;
            text2.fontSize = 1;
            text2.alignment = TextAnchor.MiddleCenter;
            text2.resizeTextForBestFit = true;
            text2.resizeTextMinSize = 0;
            RectTransform component = text2.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, 0f, 0.111f - offset / 2.55f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void Draw()
        {

            bool check = !changedboards;
            if (check)
            {
                // :)
                for (int i = 0; i < GorillaComputer.instance.levelScreens.Length; i++)
                {
                    Material material = new Material(Shader.Find("Standard"));
                    material.color = Color.black;
                    string newText = $"Thanks For Picking Kosmos Mod Menu!\nStatus: UNDETECTED!  Version: {version}\nDiscord: kosmos#0795";
                    GorillaComputer.instance.levelScreens[i].goodMaterial = material;
                    bool activeSelf = GameObject.Find("Level/lower level").activeSelf;
                    if (activeSelf)
                    {
                        GameObject.Find("Level/lower level/mirror (1)").SetActive(true);
                        GameObject.Find("Level/lower level/StaticUnlit/motdscreen").GetComponent<Renderer>().material = material;
                        GameObject.Find("Level/lower level/UI/-- PhysicalComputer UI --/monitor").GetComponent<Renderer>().material = material;
                        GameObject.Find("Level/lower level/StaticUnlit/screen").GetComponent<Renderer>().material = material;
                        GameObject.Find("Level/lower level/UI/CodeOfConduct").GetComponent<Text>().text = "[<color=yellow>KOSMOS NEWS</color>]";
                        GameObject.Find("Level/lower level/UI/CodeOfConduct/COC Text").GetComponent<Text>().text = newText;
                        // AntiBan
                        UnityEngine.Object.Destroy(GameObject.Find("Global/Photon Manager/GorillaReporter").GetComponent<GorillaNot>());
                        UnityEngine.Object.Destroy(GameObject.Find("Global/Photon Manager/GorillaReporter"));
                    }
                }
                changedboards = true;
                // Checks Menu Version!
                CheckVersion();
            }
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.4f);
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.1f, 1f, 1f);
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
            gameObject.transform.position = new Vector3(0.05f, 0f, 0f);
            GradientColorKey[] array = new GradientColorKey[3];
            array[0].color = Color.magenta;
            array[0].time = 0f;
            array[1].color = Color.red;
            array[1].time = 0.5f;
            array[2].color = Color.magenta;
            array[2].time = 1f;
            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
            Gradient gradient = new Gradient();
            gradient.colorKeys = array;
            colorChanger.colors = gradient;
            colorChanger.Start();
            canvasObj = new GameObject();
            canvasObj.transform.parent = menu.transform;
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;
            GameObject gameObject2 = new GameObject();
            gameObject2.transform.parent = canvasObj.transform;
            Text text = gameObject2.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text.text = "Kosmos Mod Menu";
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.05f);
            component.position = new Vector3(0.06f, 0f, 0.175f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            AddPageButtons();
            string[] array2 = buttons.Skip(pageNumber * pageSize).Take(pageSize).ToArray();
            for (int i = 0; i < array2.Length; i++)
            {
                AddButton((float)i * 0.13f + 0.26f, array2[i]);
            }
        }




        // spidermonke bullfucking shit \\

        public static GorillaNetworkJoinTrigger trigger = new GorillaNetworkJoinTrigger();
        public static GorillaFriendCollider collider = new GorillaFriendCollider();


        public static bool changedboards = false;
        public static bool lefttriggerpressed;
        public static bool triggerpressed;
        private static bool grip = false;
        private static bool flying = false;
        private static int speedPlusCooldown = 0;
        private static int speedMinusCooldown = 0;
        private static Color color = new Color(0f, 0f, 0f);
        private static float updateTimer = 0f;
        private static float updateRate = 0f;
        private static float timer = 0f;
        private static float hue = 0f;
        private static Harmony harmony = null;
        public static bool cangrapple = false;
        public static bool canleftgrapple = false;
        public static bool start = false;
        public static bool inAllowedRoom = false;
        public static float maxDistance = 0f;
        public static float maxDistanceRAPEGUN = 1f;
        public static float Spring = 0f;
        public static float Damper = 0f;
        public static SpringJoint leftjoint;
        public static ConfigEntry<float> dp;
        public static Color grapplecolor;
        public static SpringJoint joint;
        public static LineRenderer leftlr;
        public static LineRenderer lr;
        public static float MassScale;
        public static ConfigEntry<float> sp;
        public static Vector3 grapplePoint;
        public static Vector3 leftgrapplePoint;
        public static ConfigEntry<float> ms;
        public static ConfigEntry<Color> rc;
        public static PhotonMessageInfo info = default(PhotonMessageInfo);
        public static GameObject obj = new GameObject();
        public static Vector3 vctor = new Vector3(0f, 0f, 0f);




        private static void AddPageButtons()
        {
            int num = (buttons.Length + pageSize - 1) / pageSize;
            int num2 = pageNumber + 1;
            int num3 = pageNumber - 1;
            if (num2 > num - 1)
            {
                num2 = 0;
            }
            if (num3 < 0)
            {
                num3 = num - 1;
            }
            float num4 = 0f;
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - num4);
            gameObject.AddComponent<BtnCollider>().relatedText = "PreviousPage";
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            GameObject gameObject2 = new GameObject();
            gameObject2.transform.parent = canvasObj.transform;
            Text text = gameObject2.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text.text = "[" + num3 + "] << Prev";
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, 0f, 0.111f - num4 / 2.55f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            num4 = 0.13f;
            GameObject gameObject3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject3.GetComponent<Rigidbody>());
            gameObject3.GetComponent<BoxCollider>().isTrigger = true;
            gameObject3.transform.parent = menu.transform;
            gameObject3.transform.rotation = Quaternion.identity;
            gameObject3.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
            gameObject3.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - num4);
            gameObject3.AddComponent<BtnCollider>().relatedText = "NextPage";
            gameObject3.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            GameObject gameObject4 = new GameObject();
            gameObject4.transform.parent = canvasObj.transform;
            Text text2 = gameObject4.AddComponent<Text>();
            text2.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            text2.text = "Next >> [" + num2 + "]";
            text2.fontSize = 1;
            text2.alignment = TextAnchor.MiddleCenter;
            text2.resizeTextForBestFit = true;
            text2.resizeTextMinSize = 0;
            RectTransform component2 = text2.GetComponent<RectTransform>();
            component2.localPosition = Vector3.zero;
            component2.sizeDelta = new Vector2(0.2f, 0.03f);
            component2.localPosition = new Vector3(0.064f, 0f, 0.111f - num4 / 2.55f);
            component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void Toggle(string relatedText)
        {
            int num = (buttons.Length + pageSize - 1) / pageSize;
            if (relatedText == "NextPage")
            {
                if (pageNumber < num - 1)
                {
                    pageNumber++;
                }
                else
                {
                    pageNumber = 0;
                }
                UnityEngine.Object.Destroy(menu);
                menu = null;
                Draw();
                return;
            }
            if (relatedText == "PreviousPage")
            {
                if (pageNumber > 0)
                {
                    pageNumber--;
                }
                else
                {
                    pageNumber = num - 1;
                }
                UnityEngine.Object.Destroy(menu);
                menu = null;
                Draw();
                return;
            }
            int num2 = -1;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (relatedText == buttons[i])
                {
                    num2 = i;
                    break;
                }
            }
            if (buttonsActive[num2].HasValue)
            {
                buttonsActive[num2] = !buttonsActive[num2];
                UnityEngine.Object.Destroy(menu);
                menu = null;
                Draw();

            }
        }
    }
}