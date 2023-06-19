using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using GorillaLocomotion;
using HarmonyLib;
using UnityEngine;
using UnityEngine.XR;
using Object = UnityEngine.Object;

namespace spidermonke_v2
{
    // Token: 0x02000003 RID: 3
    [BepInPlugin("com.rass1010.gorillatag.spidermonke_v2", "SpiderMonke_v2", "1.0.1")]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    public class Plugin : BaseUnityPlugin
    {
        // Token: 0x06000007 RID: 7 RVA: 0x000020F8 File Offset: 0x000002F8
        private void Awake()
        {
            ConfigFile configFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "web.cfg"), true);
            Plugin.sp = configFile.Bind<float>("Configuration", "Spring", 10f, "spring");
            Plugin.dp = configFile.Bind<float>("Configuration", "Damper", 30f, "damper");
            Plugin.ms = configFile.Bind<float>("Configuration", "MassScale", 12f, "massscale");
            Plugin.rc = configFile.Bind<Color>("Configuration", "webColor", Color.white, "webcolor hex code");
        }

        // Token: 0x06000008 RID: 8 RVA: 0x000021A0 File Offset: 0x000003A0
        private void Update()
        {
            this.inAllowedRoom = true;
            bool flag = !this.inAllowedRoom;
            if (flag)
            {
                bool flag2 = this.start;
                if (flag2)
                {
                    UnityEngine.Object.Destroy(this.joint);
                    Object.Destroy(this.leftjoint);
                    this.start = false;
                }
            }
            bool flag3 = this.inAllowedRoom;
            if (flag3)
            {
                this.start = true;
                List<InputDevice> list = new List<InputDevice>();
                InputDevices.GetDevices(list);
                for (int i = 0; i < list.Count; i++)
                {
                    bool flag4 = InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerpressed);
                    if (flag4)
                    {
                        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerpressed);
                    }
                    bool flag5 = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out lefttriggerpressed);
                    if (flag5)
                    {
                        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out lefttriggerpressed);
                    }
                }
                bool flag6 = !this.wackstart;
                if (flag6)
                {
                    GameObject gameObject = new GameObject();
                    this.Spring = Plugin.sp.Value;
                    this.Damper = Plugin.dp.Value;
                    this.MassScale = Plugin.ms.Value;
                    this.grapplecolor = Plugin.rc.Value;
                    this.lr = Player.Instance.gameObject.AddComponent<LineRenderer>();
                    this.lr.material = new Material(Shader.Find("Sprites/Default"));
                    this.lr.startColor = this.grapplecolor;
                    this.lr.endColor = this.grapplecolor;
                    this.lr.startWidth = 0.02f;
                    this.lr.endWidth = 0.02f;
                    this.leftlr = gameObject.AddComponent<LineRenderer>();
                    this.leftlr.material = new Material(Shader.Find("Sprites/Default"));
                    this.leftlr.startColor = this.grapplecolor;
                    this.leftlr.endColor = this.grapplecolor;
                    this.leftlr.startWidth = 0.02f;
                    this.leftlr.endWidth = 0.02f;
                    this.wackstart = true;
                }
                this.DrawRope(Player.Instance);
                this.LeftDrawRope(Player.Instance);
                bool flag7 = this.triggerpressed > 0.1f;
                if (flag7)
                {
                    bool flag8 = this.cangrapple;
                    if (flag8)
                    {
                        this.Spring = Plugin.sp.Value;
                        this.StartGrapple(Player.Instance);
                        this.cangrapple = false;
                    }
                }
                else
                {
                    this.StopGrapple(Player.Instance);
                }
                bool flag9 = this.triggerpressed > 0.1f && this.lefttriggerpressed > 0.1f;
                if (flag9)
                {
                    this.Spring /= 2f;
                }
                else
                {
                    this.Spring = Plugin.sp.Value;
                }
                bool flag10 = this.lefttriggerpressed > 0.1f;
                if (flag10)
                {
                    bool flag11 = this.canleftgrapple;
                    if (flag11)
                    {
                        this.Spring = Plugin.sp.Value;
                        this.LeftStartGrapple(Player.Instance);
                        this.canleftgrapple = false;
                    }
                }
                else
                {
                    this.LeftStopGrapple();
                }
            }
        }

        // Token: 0x06000009 RID: 9 RVA: 0x0000251C File Offset: 0x0000071C
        public void StartGrapple(Player __instance)
        {
            RaycastHit raycastHit;
            bool flag = Physics.Raycast(__instance.rightControllerTransform.position, __instance.rightControllerTransform.forward, out raycastHit, this.maxDistance);
            if (flag)
            {
                this.grapplePoint = raycastHit.point;
                this.joint = __instance.gameObject.AddComponent<SpringJoint>();
                this.joint.autoConfigureConnectedAnchor = false;
                this.joint.connectedAnchor = this.grapplePoint;
                float num = Vector3.Distance(__instance.bodyCollider.attachedRigidbody.position, this.grapplePoint);
                this.joint.maxDistance = num * 0.8f;
                this.joint.minDistance = num * 0.25f;
                this.joint.spring = this.Spring;
                this.joint.damper = this.Damper;
                this.joint.massScale = this.MassScale;
                this.lr.positionCount = 2;
            }
        }

        // Token: 0x0600000A RID: 10 RVA: 0x00002618 File Offset: 0x00000818
        public void DrawRope(Player __instance)
        {
            bool flag = !this.joint;
            if (!flag)
            {
                this.lr.SetPosition(0, __instance.rightControllerTransform.position);
                this.lr.SetPosition(1, this.grapplePoint);
            }
        }

        // Token: 0x0600000B RID: 11 RVA: 0x00002665 File Offset: 0x00000865
        public void StopGrapple(Player __instance)
        {
            this.lr.positionCount = 0;
            UnityEngine.Object.Destroy(this.joint);
            this.cangrapple = true;
        }

        // Token: 0x0600000C RID: 12 RVA: 0x00002688 File Offset: 0x00000888
        public void LeftStartGrapple(Player __instance)
        {
            RaycastHit raycastHit;
            bool flag = Physics.Raycast(__instance.leftControllerTransform.position, __instance.leftControllerTransform.forward, out raycastHit, this.maxDistance);
            if (flag)
            {
                this.leftgrapplePoint = raycastHit.point;
                this.leftjoint = __instance.gameObject.AddComponent<SpringJoint>();
                this.leftjoint.autoConfigureConnectedAnchor = false;
                this.leftjoint.connectedAnchor = this.leftgrapplePoint;
                float num = Vector3.Distance(__instance.bodyCollider.attachedRigidbody.position, this.leftgrapplePoint);
                this.leftjoint.maxDistance = num * 0.8f;
                this.leftjoint.minDistance = num * 0.25f;
                this.leftjoint.spring = this.Spring;
                this.leftjoint.damper = this.Damper;
                this.leftjoint.massScale = this.MassScale;
                this.leftlr.positionCount = 2;
            }
        }

        // Token: 0x0600000D RID: 13 RVA: 0x00002784 File Offset: 0x00000984
        public void LeftDrawRope(Player __instance)
        {
            bool flag = !this.leftjoint;
            if (!flag)
            {
                this.leftlr.SetPosition(0, __instance.leftControllerTransform.position);
                this.leftlr.SetPosition(1, this.leftgrapplePoint);
            }
        }

        // Token: 0x0600000E RID: 14 RVA: 0x000027D1 File Offset: 0x000009D1
        public void LeftStopGrapple()
        {
            this.leftlr.positionCount = 0;
            UnityEngine.Object.Destroy(this.leftjoint);
            this.canleftgrapple = true;
        }

        // Token: 0x04000004 RID: 4
        public float triggerpressed;

        // Token: 0x04000005 RID: 5
        public float lefttriggerpressed;

        // Token: 0x04000006 RID: 6
        public bool cangrapple = true;

        // Token: 0x04000007 RID: 7
        public bool canleftgrapple = true;

        // Token: 0x04000008 RID: 8
        public bool wackstart = false;

        // Token: 0x04000009 RID: 9
        public bool start = true;

        // Token: 0x0400000A RID: 10
        public bool inAllowedRoom = false;

        // Token: 0x0400000B RID: 11
        public float maxDistance = 100f;

        // Token: 0x0400000C RID: 12
        public float Spring;

        // Token: 0x0400000D RID: 13
        public float Damper;

        // Token: 0x0400000E RID: 14
        public float MassScale;

        // Token: 0x0400000F RID: 15
        public Vector3 grapplePoint;

        // Token: 0x04000010 RID: 16
        public Vector3 leftgrapplePoint;

        // Token: 0x04000011 RID: 17
        public SpringJoint joint;

        // Token: 0x04000012 RID: 18
        public SpringJoint leftjoint;

        // Token: 0x04000013 RID: 19
        public LineRenderer lr;

        // Token: 0x04000014 RID: 20
        public LineRenderer leftlr;

        // Token: 0x04000015 RID: 21
        public Color grapplecolor;

        // Token: 0x04000016 RID: 22
        public static ConfigEntry<float> sp;

        // Token: 0x04000017 RID: 23
        public static ConfigEntry<float> dp;

        // Token: 0x04000018 RID: 24
        public static ConfigEntry<float> ms;

        // Token: 0x04000019 RID: 25
        public static ConfigEntry<Color> rc;
    }
}
