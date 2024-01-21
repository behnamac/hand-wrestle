using System;
using System.Collections.Generic;
using Controllers;
using Storage;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(RagdollController))]
    public class PlayerWrestleController : MonoBehaviour
    {
        [SerializeField] private float power;
        [SerializeField] private float speedDownWrestle;
        [SerializeField] private float maxStamina;
        [SerializeField] private float speedUpStamina;
        [SerializeField] private float speedDownStamina;
        [SerializeField] private Transform enemyTargetHand;
        
        private PlayerMoveController playerMove;
        private Animator anim;
        private RagdollController ragdoll;
        private bool canWrestle;
        private float wrestleAnimValue;
        private float currentMaxStamina;
        private float currentPower;
        private float currentStamina;
        private float minStamina;
        private List<float> staminaWave;


        private static readonly int Wrestle = Animator.StringToHash("Wrestle");
        private static readonly int WrestleForce = Animator.StringToHash("WrestleForce");

        private void Awake()
        {
            TryGetComponent(out anim);
            TryGetComponent(out playerMove);
            TryGetComponent(out ragdoll);

            staminaWave = new List<float>();
            currentPower = power;
            currentMaxStamina = maxStamina;
        }

        private void Start()
        {
            int basrTotalSplit=5;
            var staminaWaveValue = currentMaxStamina / basrTotalSplit;
            for (int i = 0; i < basrTotalSplit; i++)
            {
                staminaWave.Add(staminaWaveValue * i);
            }
        }

        private void Update()
        {
            if (canWrestle)
            {
                WrestleController();
            }
        }

        private void WrestleController()
        {
            var enemyPower = playerMove.Wave.enemy.power;
            if (Input.GetMouseButton(0))
            {
                wrestleAnimValue += (currentPower - enemyPower) * Time.deltaTime;
                currentStamina += speedUpStamina * Time.deltaTime;
            }
            else
            {
                wrestleAnimValue -= speedDownWrestle * Time.deltaTime;
                currentStamina -= speedDownStamina * Time.deltaTime;
            }
            SetWrestleAnimation();

            for (int i = 0; i < staminaWave.Count; i++)
            {
                if (minStamina < staminaWave[i] && currentStamina > staminaWave[i])
                    minStamina = staminaWave[i];
            }
            
            currentStamina = Mathf.Clamp(currentStamina, minStamina, currentMaxStamina);
            UiController.Instance.UpdateStaminaBar(currentStamina / currentMaxStamina);

            if (currentStamina >= currentMaxStamina)
            {
                LevelManager.Instance.LevelFail();
                ragdoll.ActivateRagdoll();
                canWrestle = false;
            }

            if (wrestleAnimValue >= 1)
            {
                playerMove.Wave.enemy.TargetHand = null;
                playerMove.Wave.enemy.ActiveRagdoll();
                UiController.Instance.AddCoin(5 + PlayerPrefsController.GetIncomeLevel());
                playerMove.GoToNextPoint();
                wrestleAnimValue = 0;
                anim.SetFloat(WrestleForce, wrestleAnimValue);
                canWrestle = false;
            }
        }

        private void SetWrestleAnimation()
        {
            wrestleAnimValue = Mathf.Clamp(wrestleAnimValue, 0, 1);
            anim.SetFloat(WrestleForce, wrestleAnimValue);
            playerMove.Wave.enemy.SetWrestleAnim(-wrestleAnimValue);
        }

        public void ActiveWrestle()
        {
            playerMove.Wave.enemy.TargetHand = enemyTargetHand;
            playerMove.Wave.enemy.ActiveWrestle();
            anim.SetBool(Wrestle, true);
            canWrestle = true;
        }

        public void UpgradePower(float value)
        {
            currentPower += value;
        }

        public void UpgradeStamina(float value)
        {
            currentMaxStamina += value;
        }
    }
}
