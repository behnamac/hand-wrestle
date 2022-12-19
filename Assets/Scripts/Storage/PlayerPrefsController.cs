using UnityEngine;

namespace Storage
{
    public static class PlayerPrefsController
    {
        #region SETTER

        public static void SetLevelIndex(int index) => PlayerPrefs.SetInt("level-index", index);
        public static void SetLevelNumber(int number) => PlayerPrefs.SetInt("level-number", number);
        public static void SetCurrency(int currency) => PlayerPrefs.SetInt("currency", currency);
        public static void SetPowerUpgradeLevel(int value) => PlayerPrefs.SetInt("PowerUpgradeLevel", value);
        public static void SetStaminaUpgradeLevel(int value) => PlayerPrefs.SetInt("StaminaUpgradeLevel", value);
        public static void SetIncomeLevel(int value) => PlayerPrefs.SetInt("Income", value);
        #endregion

        #region GETTER

        public static int GetLevelIndex() => PlayerPrefs.GetInt("level-index");
        public static int GetLevelNumber() => PlayerPrefs.GetInt("level-number");
        public static int GetTotalCurrency() => PlayerPrefs.GetInt("currency");
        public static int GetPowerUpgradeLevel() => PlayerPrefs.GetInt("PowerUpgradeLevel");
        public static int GetStaminaUpgradeLevel() => PlayerPrefs.GetInt("StaminaUpgradeLevel");
        public static int GetIncomeLevel() => PlayerPrefs.GetInt("Income", 1);

        #endregion
    }
}