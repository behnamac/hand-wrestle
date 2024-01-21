using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance { get; private set; }

        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField, Range(0f, 1f)] private float followSpeed = 0.1f;
        [SerializeField] private bool xPositionLock;
        [SerializeField] private bool isTargetLook;

        [Tooltip("This parameter needs to AbstractPlayerMoveController on target component!")]
        [SerializeField] private bool setPlayerFollowSpeed;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            InitializeOffset();
        }

        private void LateUpdate()
        {
            PerformSmoothFollow();
        }

        private void InitializeOffset()
        {
            offset = transform.position - target.position;
        }

        private void PerformSmoothFollow()
        {
            var targetPosition = target.position + offset;
            if (xPositionLock) targetPosition.x = transform.position.x;

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed);
            if (isTargetLook) transform.LookAt(target);
        }
    }
}
