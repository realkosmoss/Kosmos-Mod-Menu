using System;
using BepInEx;
using GorillaLocomotion;
using UnityEngine;
using UnityEngine.XR;

namespace IronMonke
{
    [BepInPlugin("ass.shit.ass", "ironsthit", "2.2.2")]
    public class IronMonkePlugin : BaseUnityPlugin
    {
        private void OnGameInitialized(object sender, EventArgs e)
        {
            IronMonkePlugin.RB = Player.Instance.bodyCollider.attachedRigidbody;
            IronMonkePlugin.rHandT = Player.Instance.rightHandFollower;
            IronMonkePlugin.lHandT = Player.Instance.leftHandFollower;
        }

        public static void FixedUpdate(bool enable)
        {
            if (enable)
            {
                IronMonkePlugin.onehand = 13f;
                IronMonkePlugin.twohand = 15f;
                IronMonkePlugin.RB = Player.Instance.bodyCollider.attachedRigidbody;
                IronMonkePlugin.rHandT = Player.Instance.rightHandFollower;
                IronMonkePlugin.lHandT = Player.Instance.leftHandFollower;

                bool flag = false;
                InputDevices.GetDeviceAtXRNode((XRNode)IronMonkePlugin.rNode).TryGetFeatureValue(CommonUsages.primaryButton, out flag);

                bool flag3 = false;
                InputDevices.GetDeviceAtXRNode((XRNode)IronMonkePlugin.lNode).TryGetFeatureValue(CommonUsages.primaryButton, out flag3);

                if (flag)
                {
                    IronMonkePlugin.RB.AddForce(IronMonkePlugin.thrust * IronMonkePlugin.rHandT.right, ForceMode.Impulse);
                }

                if (flag3)
                {
                    IronMonkePlugin.RB.AddForce(-IronMonkePlugin.thrust * IronMonkePlugin.lHandT.right, ForceMode.Impulse);
                }

                if (flag3 && flag)
                {
                    IronMonkePlugin.thrust = IronMonkePlugin.twohand;
                }
                else
                {
                    IronMonkePlugin.thrust = IronMonkePlugin.onehand;
                }

                if (flag || flag3)
                {
                    IronMonkePlugin.RB.velocity = Vector3.ClampMagnitude(IronMonkePlugin.RB.velocity, IronMonkePlugin.maxSpeed);
                }
            }
        }

        private static bool inRoom;

        private static int rNode = 5;

        private static int lNode = 4;

        private static Rigidbody RB;

        private static Transform rHandT;

        private static Transform lHandT;

        private static float thrust;

        private static float maxSpeed = 50f;

        private static GameObject quitBox;

        private static Transform quitBoxT;

        private static float onehand;

        private static float twohand;
    }
}
