using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class RagdollController : MonoBehaviour
    {
        private Collider[] allColliders;
        private Rigidbody[] allRigidbodys;

        private Animator anim;
        private void Awake()
        {
            TryGetComponent(out anim);
            allColliders = GetComponentsInChildren<Collider>();
            allRigidbodys = GetComponentsInChildren<Rigidbody>();

            for (int i = 0; i < allColliders.Length; i++)
            {
                allColliders[i].enabled = false;
            }

            for (int i = 0; i < allRigidbodys.Length; i++)
            {
                allRigidbodys[i].useGravity = false;
                allRigidbodys[i].isKinematic = true;
            }
        }

        public void ActiveRagdoll()
        {
            anim.enabled = false;
            for (int i = 0; i < allColliders.Length; i++)
            {
                allColliders[i].enabled = true;
            }

            for (int i = 0; i < allRigidbodys.Length; i++)
            {
                allRigidbodys[i].useGravity = true;
                allRigidbodys[i].isKinematic = false;
            }
        }
    }
}
