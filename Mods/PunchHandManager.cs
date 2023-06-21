using System;
using UnityEngine;

namespace PunchMod
{
    // Token: 0x02000005 RID: 5
    public class PunchHandManager : MonoBehaviour
    {
        private void Start()
        {
            this.rb = GameObject.Find("GorillaPlayer").GetComponent<Rigidbody>();
        }

        private void Update()
        {
            this.currentPosition = base.transform.position;
            bool flag = Vector3.Distance(Camera.main.transform.position, base.transform.position) < this.maxDistance;
            bool flag2 = flag && !this.punch;
            if (flag2)
            {
                this.punch = true;
                this.rb.velocity = (this.currentPosition - this.lastPosition) * this.strength;
            }
            bool flag3 = !flag && this.punch;
            if (flag3)
            {
                this.punch = false;
            }
            this.lastPosition = this.currentPosition;
        }

        private Vector3 currentPosition;

        private Vector3 lastPosition;

        public float strength = 150f;

        private float maxDistance = 0.4f;

        private bool punch;

        private Rigidbody rb;
    }
}
