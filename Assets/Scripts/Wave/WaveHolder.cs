using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

namespace Wave
{
    public class WaveHolder : MonoBehaviour
    {
        public EnemyController enemy;
        public Transform point;

        private void Start()
        {
            WaveController.Instance.AddWave(this);
        }
    }
}
