using System.Collections;
using UnityEngine;
using Controllers;
using Levels;
using Wave;

namespace Player
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private float speedMove;

        private PlayerWrestleController playerWrestle;
        private Animator anim;
        private int indexWave;
        public WaveHolder Wave { get; private set; }

        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Wrestle = Animator.StringToHash("Wrestle");

        #region Unity Functions
        private void Awake()
        {
            TryGetComponent(out anim);
            TryGetComponent(out playerWrestle);

            LevelManager.OnLevelStart += OnLevelStart;
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= OnLevelStart;
        }

        #endregion

        #region Public Functions
        
        public void GoToNextPoint()
        {
            indexWave++;
            if (indexWave >= WaveController.Instance.GetWaveCount())
            {
                LevelManager.Instance.LevelComplete();
                return;
            }
            Wave = WaveController.Instance.GetWave(indexWave);
            StartCoroutine(GoToNextPointCo(Wave.point));
        }

        #endregion

        #region Unity Functions

        private void OnLevelStart(Level level)
        {
            Wave = WaveController.Instance.GetWave(indexWave);
            transform.position = Wave.point.position;
            playerWrestle.ActiveWrestle();
        }

        #endregion

        #region IEnumerators

        private IEnumerator GoToNextPointCo(Transform point)
        {
            anim.SetBool(Wrestle, false);
            yield return new WaitForSeconds(0.3f);
            var thisTransform = transform;
            anim.SetBool(Move, true);
            while (thisTransform.position != point.position)
            {
                var thisPosition = thisTransform.position;
                thisTransform.position =
                    Vector3.MoveTowards(thisPosition, point.position, speedMove * Time.deltaTime);
                yield return null;
            }
            anim.SetBool(Move, false);
            playerWrestle.ActiveWrestle();
        }

        #endregion
    }
}
