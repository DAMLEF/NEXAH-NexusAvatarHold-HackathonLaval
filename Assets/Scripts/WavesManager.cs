using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WavesManager : MonoBehaviour
{
    public float timeBetweenWave = 15;   // En seconde
    public float timeBetweenEnemy = 2;
    public float timeBeforeStart = 5;
    public float difficultyCoefficient = 1;
    public float difficultyUpscale = 0.01f;     // Augmentation du coefficient de difficulté à chaque vague
    public List<GameObject> allEnemiesType; // Liste des types d'ennemies qui peuvent apparaître

    private int waves = 0;
    private bool inWave = false;
    private float endWaveTime;
    private float startWaveTime;
    private float timeLastSpawn;

    private List<GameObject> waveEnemies;
    private int enemiesRemaining;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endWaveTime = Time.time + timeBeforeStart;
    }

    // Update is called once per frame
    void Update()
    {

        if (inWave)
        {
            if(enemiesRemaining > 0 && Time.time - timeLastSpawn >= timeBetweenEnemy)
            {

                // On spawn un ennemi
                // TODO
                enemiesRemaining--;
                timeLastSpawn = Time.time;
            }

            if(enemiesRemaining == 0 && waveEnemies.Count == 0 )
            {
                Debug.Log("fin de vague");
            }

        }
        else
        {
            if (Time.time - endWaveTime >= timeBetweenWave) {
                // Initialisation d'une vague
                waveInit();
            }
        }

    }


    void waveInit()
    {
        if(waves != 0)
        {
            difficultyCoefficient += difficultyUpscale;
        }

        waves++;
        enemiesRemaining = waves * 10;

        inWave = true;
        startWaveTime = Time.time;
        timeLastSpawn = Time.time;

    }

}
