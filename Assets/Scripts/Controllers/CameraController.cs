using System;
using System.ComponentModel;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance { get; private set; }


        #region SERIALIZE FIELDS

        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float followSpeed = 0.1f;
        [SerializeField] private bool xPositionLock;
        [SerializeField] private bool isTargetLook;

        [Tooltip("This parameter needs to AbstractPlayerMoveController on target component!")]
        [SerializeField] private bool setPlayerFollowSpeed;

        #endregion

        #region PRIVATE METHODS

        private void Initialize()
        {
            // SET DEFAULT OFFSET
            offset = transform.position - target.position;

        }

        private void SmoothFollow()
        {
            var targetPos = target.position + offset;

            if (xPositionLock)
            {
                targetPos.x = transform.position.x;
            }

            var smoothFollow = Vector3.Lerp(transform.position, targetPos, followSpeed);

            transform.position = smoothFollow;

            if (!isTargetLook) return;

            transform.LookAt(target);
        }

        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        private void Start() => Initialize();

        private void LateUpdate() => SmoothFollow();

        #endregion
    }
}