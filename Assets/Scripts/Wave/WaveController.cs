using System.Collections.Generic;
using UnityEngine;

namespace Wave
{
    public class WaveController : MonoBehaviour
    {
        public static WaveController Instance;
        
        private List<WaveHolder> waveHolders;

        private void Awake()
        {
            Instance = this;
            
            waveHolders = new List<WaveHolder>();
        }

        public void AddWave(WaveHolder wave) => waveHolders.Add(wave);
        public WaveHolder GetWave(int index) => waveHolders[index];
        public int GetWaveCount() => waveHolders.Count;
    }
}
