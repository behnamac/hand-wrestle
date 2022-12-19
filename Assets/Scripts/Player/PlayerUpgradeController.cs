using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Storage;
using UnityEngine;

namespace Player
{
    public class PlayerUpgradeController : MonoBehaviour
    {
        [Header("Power")]
        [SerializeField] private float addPowerValue;
        [SerializeField] private int firstPowerPrice;
        [SerializeField] private int addPowerPrice;

        [Header("Stamina")] 
        [SerializeField] private float addStaminaValue;
        [SerializeField] private int firstStaminaPrice;
        [SerializeField] private int addStaminaPrice;

        [Header("Income")] 
        [SerializeField] private int firstIncomePrice;
        [SerializeField] private int addIncomePrice;

        private int powerLevel;
        private int staminaLevel;
        private int incomeLevel;
        private int powerPrice;
        private int staminaPrice;
        private int incomePrice;

        private PlayerWrestleController playerWrestle;
        private void Awake()
        {
            TryGetComponent(out playerWrestle);
            
            powerLevel = PlayerPrefsController.GetPowerUpgradeLevel();
            staminaLevel = PlayerPrefsController.GetStaminaUpgradeLevel();
            incomeLevel = PlayerPrefsController.GetIncomeLevel();

            powerPrice = firstPowerPrice;
            staminaPrice = firstStaminaPrice;
            incomePrice = firstIncomePrice;
        }

        private void Start()
        {
            LoadData();
            UiController.Instance.powerUpgradeButton.onClick.AddListener(UpgradePower);
            UiController.Instance.staminaUpgradeButton.onClick.AddListener(UpgradeStamina);
            UiController.Instance.incomeUpgradeButton.onClick.AddListener(UpgradeIncome);
        }

        private void LoadData()
        {
            playerWrestle.UpgradePower(addPowerValue * powerLevel);
            playerWrestle.UpgradeStamina(addStaminaValue * staminaLevel);

            powerPrice += addPowerPrice * powerLevel;
            staminaPrice += addStaminaPrice * staminaLevel;
            incomePrice += addIncomePrice * incomeLevel;

            UiController.Instance.UpdatePowerTexts(powerLevel, powerPrice);
            UiController.Instance.UpdateStaminaTexts(staminaLevel, staminaPrice);
            UiController.Instance.UpdateIncomeTexts(incomeLevel, incomePrice);
        }

        private void UpgradePower()
        {
            if (PlayerPrefsController.GetTotalCurrency() < powerPrice)
            {
                return;
            }

            powerLevel++;
            PlayerPrefsController.SetPowerUpgradeLevel(powerLevel);
            UiController.Instance.AddCoin(-powerPrice);
            playerWrestle.UpgradePower(addPowerValue);
            powerPrice += addPowerPrice;
            UiController.Instance.UpdatePowerTexts(powerLevel, powerPrice);
        }

        private void UpgradeStamina()
        {
            if (PlayerPrefsController.GetTotalCurrency() < staminaPrice)
            {
                return;
            }

            staminaLevel++;
            PlayerPrefsController.SetStaminaUpgradeLevel(staminaLevel);
            UiController.Instance.AddCoin(-staminaPrice);
            playerWrestle.UpgradeStamina(addStaminaValue);
            staminaPrice += addStaminaPrice;
            UiController.Instance.UpdateStaminaTexts(staminaLevel, staminaPrice);
        }

        private void UpgradeIncome()
        {
            if (PlayerPrefsController.GetTotalCurrency() < incomePrice)
            {
                return;
            }

            UiController.Instance.AddCoin(-incomePrice);
            incomeLevel++;
            incomePrice += addIncomePrice;
            PlayerPrefsController.SetIncomeLevel(incomeLevel);
            UiController.Instance.UpdateIncomeTexts(incomeLevel, incomePrice);
        }
    }
}
