using Levels;
using Storage;
using UnityEngine;

namespace Controllers
{
    public class LevelManager : MonoBehaviour
    {
        #region DELEGATE

        public delegate void LevelLoadHandler(Level levelData);

        public delegate void LevelStartHandler(Level levelData);

        public delegate void LevelStageCompleteHandler(Level levelData, int stageIndex = 0);

        public delegate void LevelCompleteHandler(Level levelData);

        public delegate void LevelFailHandler(Level levelData);

        #endregion

        #region EVENTS

        /// <summary>
        /// Event triggered after a level is loaded.
        /// </summary>
        public static LevelLoadHandler OnLevelLoad;

        /// <summary>
        /// Event triggered when playing a level starts.
        /// </summary>
        public static LevelStartHandler OnLevelStart;

        /// <summary>
        /// Event triggered after completing each stage in a level.
        /// </summary>
        public static LevelStageCompleteHandler OnLevelStageComplete;

        /// <summary>
        /// Event triggered when a level is successfully completed.
        /// </summary>
        public static LevelCompleteHandler OnLevelComplete;

        /// <summary>
        /// Event triggered when a level fails.
        /// </summary>
        public static LevelFailHandler OnLevelFail;

        #endregion

        #region PUBLIC FIELDS / PROPS

        public static LevelManager Instance { get; private set; }

        #endregion

        #region SERIALIZE PRIVATE FIELDS

        [SerializeField] private LevelSource levelSource; // Source of levels
        [SerializeField] private GameObject levelSpawnPoint; // Container for spawning levels
        [SerializeField] private int loopLevelsStartIndex = 1; // Starting index after reaching the maximum level
        [SerializeField] private bool loopLevelGetRandom = true; // Whether to load random levels after reaching the maximum

        #endregion

        #region PRIVATE FIELDS

        private GameObject _activeLevel;

        #endregion

        #region PRIVATE METHODS

        private void CheckRepeatLevelIndex()
        {
            if (loopLevelsStartIndex < levelSource.levelData.Length) return;
            loopLevelsStartIndex = 0;
        }

        private GameObject GetLevel()
        {
            if (PlayerPrefsController.GetLevelIndex() >= levelSource.levelData.Length)
            {
                if (loopLevelGetRandom)
                {
                    var levelIndex = Random.Range(loopLevelsStartIndex, levelSource.levelData.Length - 1);
                    PlayerPrefsController.SetLevelIndex(levelIndex);
                }
            }

            var level = levelSource.levelData[PlayerPrefsController.GetLevelIndex()];
            var levelData = level.GetComponent<Level>();

            levelData.levelIndex = PlayerPrefsController.GetLevelIndex();
            levelData.levelNumber = PlayerPrefsController.GetLevelNumber();

            return level;
        }

        #endregion

        #region PUBLIC METHODS

        public void LevelLoad()
        {
            _activeLevel = Instantiate(GetLevel(), levelSpawnPoint.transform, false);
            OnLevelLoad?.Invoke(_activeLevel.GetComponent<Level>());
        }

        public void LevelStart()
        {
            OnLevelStart?.Invoke(_activeLevel.GetComponent<Level>());
        }

        public void LevelStageComplete(int stageIndex = 0)
        {
            OnLevelStageComplete?.Invoke(_activeLevel.GetComponent<Level>(), stageIndex);
        }

        public void LevelComplete()
        {
            PlayerPrefsController.SetLevelIndex(PlayerPrefsController.GetLevelIndex() + 1);
            PlayerPrefsController.SetLevelNumber(PlayerPrefsController.GetLevelNumber() + 1);

            OnLevelComplete?.Invoke(_activeLevel.GetComponent<Level>());
        }

        public void LevelFail()
        {
            OnLevelFail?.Invoke(_activeLevel.GetComponent<Level>());
        }

        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            CheckRepeatLevelIndex();
            Instance = this;
        }

        private void Start() => LevelLoad();

        #endregion
    }
}
