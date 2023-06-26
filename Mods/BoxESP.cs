using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace ModsKosmosModMenuBoxESP
{
    public class BoxEsp : MonoBehaviour
    {

        public void Update()
        {
            if (PhotonNetwork.InRoom)
            {
                ThrowableBug[] throwableBugs = Object.FindObjectsOfType<ThrowableBug>();
                foreach (ThrowableBug throwableBug in throwableBugs)
                {
                    GameObject gameObject = throwableBug.GetComponentInParent<Transform>().gameObject;
                    if (!gameObject.gameObject.GetComponent<AddBox>())
                    {
                        gameObject.gameObject.AddComponent<AddBox>();
                    }
                    else
                    {
                        AddBox component = gameObject.GetComponent<AddBox>();
                        Camera main = Camera.main;
                        Matrix4x4 projectionMatrix = main.projectionMatrix;
                        Vector3 position = gameObject.transform.position;
                        float distance = Vector3.Distance(position, main.transform.position);
                        Matrix4x4 worldToCameraMatrix = main.worldToCameraMatrix;
                        main.WorldToViewportPoint(position);
                        Vector4 vector = projectionMatrix * worldToCameraMatrix * new Vector4(position.x, position.y, position.z, 1f);
                        vector /= vector.w;
                        float objectScale = distance / vector.w;
                        float minScale = 2f;
                        float maxScale = 8.5f;
                        objectScale = Mathf.Clamp(objectScale, minScale, maxScale);
                        component.gameObject.transform.localScale = new Vector3(objectScale / 40f, objectScale / 40f, objectScale / 40f);
                        component.gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f, 0.4f);
                    }
                }

                VRRig[] vrrigs = GameObject.Find("GorillaVRRigs").GetComponentsInChildren<VRRig>();
                foreach (VRRig vrrig in vrrigs)
                {
                    if (!vrrig.isOfflineVRRig && !vrrig.isMyPlayer && !vrrig.photonView.IsMine)
                    {
                        if (!vrrig.gameObject.GetComponent<AddBox>())
                        {
                            vrrig.gameObject.AddComponent<AddBox>();
                        }
                        else
                        {
                            AddBox component2 = vrrig.GetComponent<AddBox>();
                            Camera main2 = Camera.main;
                            Matrix4x4 projectionMatrix2 = main2.projectionMatrix;
                            Vector3 position2 = vrrig.transform.position;
                            float distance2 = Vector3.Distance(position2, main2.transform.position);
                            Matrix4x4 worldToCameraMatrix2 = main2.worldToCameraMatrix;
                            main2.WorldToViewportPoint(position2);
                            Vector4 vector2 = projectionMatrix2 * worldToCameraMatrix2 * new Vector4(position2.x, position2.y, position2.z, 1f);
                            vector2 /= vector2.w;
                            float objectScale2 = distance2 / vector2.w;
                            float minScale2 = 2f;
                            float maxScale2 = 8.5f;
                            objectScale2 = Mathf.Clamp(objectScale2, minScale2, maxScale2);
                            component2.gameObject.transform.localScale = new Vector3(objectScale2 / 40f, objectScale2 / 40f, objectScale2 / 40f);

                            if (!GorillaGameManager.instance.gameObject.GetComponent<GorillaTagManager>().isCasual)
                            {
                                if (Extensions.Contains(GorillaGameManager.instance.gameObject.GetComponent<GorillaTagManager>().currentInfectedArray, vrrig.photonView.Owner.ActorNumber))
                                {
                                    component2.gameObject.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.4f);
                                }
                                else
                                {
                                    component2.gameObject.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 0.4f);
                                }
                            }
                            else
                            {
                                component2.gameObject.GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f, 0.4f);
                            }
                        }
                    }
                }
            }
        }
    }

    public class AddBox : MonoBehaviour
    {
        public GameObject topSide;
    }

    public class ThrowableBug : MonoBehaviour { }

    public class VRRig : MonoBehaviour
    {
        public bool isOfflineVRRig;
        public bool isMyPlayer;
        public PhotonView photonView;
    }

    public static class Extensions
    {
        public static bool Contains(int[] array, int item)
        {
            foreach (int i in array)
            {
                if (i == item)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class GorillaGameManager
    {
        public static GorillaGameManager instance;
        public GameObject gameObject;
    }

    public class GorillaTagManager
    {
        public int[] currentInfectedArray;
        public bool isCasual;
    }
}
