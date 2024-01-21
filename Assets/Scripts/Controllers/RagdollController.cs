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
            // Try to get the Animator component
            TryGetComponent(out anim);

            // Get all colliders and rigidbodies in the children
            allColliders = GetComponentsInChildren<Collider>();
            allRigidbodys = GetComponentsInChildren<Rigidbody>();

            // Disable colliders and set rigidbodies to be non-kinematic at the start
            DisableRagdoll();
        }

        // Disable the ragdoll physics and enable the animator
        public void DisableRagdoll()
        {
            anim.enabled = true;

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

        // Enable the ragdoll physics and disable the animator
        public void ActivateRagdoll()
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
