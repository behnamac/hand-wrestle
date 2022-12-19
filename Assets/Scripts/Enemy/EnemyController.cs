using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(RagdollController))]
    public class EnemyController : MonoBehaviour
    {
        public float power;
        
        private Animator anim;
        private RagdollController ragdoll;
        public Transform TargetHand { get; set; }
        
        
        private static readonly int WrestleForce = Animator.StringToHash("WrestleForce");
        private static readonly int Wrestle = Animator.StringToHash("Wrestle");

        private void Awake()
        {
            TryGetComponent(out anim);
            TryGetComponent(out ragdoll);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            if (TargetHand)
            {
                anim.SetIKPosition(AvatarIKGoal.RightHand, TargetHand.position);
                anim.SetIKRotation(AvatarIKGoal.RightHand, TargetHand.rotation);
            }
        }

        public void ActiveWrestle() => anim.SetBool(Wrestle, true);
        public void SetWrestleAnim(float value)
        {
            anim.SetFloat(WrestleForce, value);
        }

        public void ActiveRagdoll()
        {
            ragdoll.ActiveRagdoll();
        }
    }
}
