using System;
using BepInEx;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

namespace PunchMod
{
    [BepInPlugin("com.faggots.gorillatag.LMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeRLMoDChEcKeR", "L Mod Checker", "6.9.9")]
    public class Plugin : BaseUnityPlugin
    {
        private void Update()
        {
            Keyboard current = Keyboard.current;
            bool wasPressedThisFrame = current.zKey.wasPressedThisFrame;
            if (wasPressedThisFrame)
            {
                this.stop = !this.stop;
                bool flag = this.stop;
                if (flag)
                {
                    foreach (PunchHandManager punchHandManager in UnityEngine.Object.FindObjectsOfType<PunchHandManager>())
                    {
                        UnityEngine.Object.Destroy(punchHandManager);
                    }
                }
            }
            bool flag2 = !this.stop;
            if (flag2)
            {
                foreach (TwoBoneIKConstraint twoBoneIKConstraint in GameObject.Find("GorillaVRRigs").GetComponentsInChildren<TwoBoneIKConstraint>())
                {
                    bool flag3 = !twoBoneIKConstraint.GetComponentInParent<PhotonView>().IsMine;
                    if (flag3)
                    {
                        bool flag4 = !twoBoneIKConstraint.gameObject.GetComponentInChildren<PunchHandManager>();
                        if (flag4)
                        {
                            twoBoneIKConstraint.gameObject.GetComponentInChildren<AudioSource>().gameObject.AddComponent<PunchHandManager>();
                        }
                        bool flag5 = twoBoneIKConstraint.gameObject.GetComponentInChildren<PunchHandManager>().strength != this.strengths;
                        if (flag5)
                        {
                            twoBoneIKConstraint.gameObject.GetComponentInChildren<PunchHandManager>().strength = this.strengths;
                        }
                    }
                }
            }
        }


        public Plugin()
        {
        }

        public bool stop;

        public float strengths = 100f;
    }
}
