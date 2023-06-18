using UnityEngine;

namespace ModMenuPatch.HarmonyPatches
{
    internal class BtnCollider : MonoBehaviour
    {
        public string relatedText;

        private void OnTriggerEnter(Collider collider)
        {
            if (Time.frameCount >= KosmosModMenu.framePressCooldown + 30)
            {
                KosmosModMenu.Toggle(relatedText);
                KosmosModMenu.framePressCooldown = Time.frameCount;
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, false, 0.25f);
            }
        }
    }
}
