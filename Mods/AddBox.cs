using ModMenuPatch.HarmonyPatches;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace KosmosModMenu.Mods
{
    public class AddBox : MonoBehaviour
    {
        private void Start()
        {
            this.hollowBoxGO = new GameObject("HollowBox");
            this.hollowBoxGO.transform.SetParent(base.transform);
            this.topSide = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.topSide.transform.SetParent(this.hollowBoxGO.transform);
            this.topSide.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            this.topSide.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
            UnityEngine.Object.Destroy(this.topSide.GetComponent<MeshCollider>());
            this.hollowBoxGO.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            this.hollowBoxGO.transform.localRotation = Quaternion.identity;
            this.hollowBoxGO.AddComponent(typeof(Box));
        }

        public GameObject topSide;

        private GameObject hollowBoxGO;
    }
}
