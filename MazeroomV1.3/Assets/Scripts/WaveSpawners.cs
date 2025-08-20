using System.Runtime.CompilerServices;
using UnityEngine;

public class WaveSpawners : MonoBehaviour
{
    [SerializeField] private float countdown;

    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            SpawnWave();
          
        }
    }

    private void SpawnWave()
    {
       
    }
}

[System.Serializable]

public class Wave
{
    
}
