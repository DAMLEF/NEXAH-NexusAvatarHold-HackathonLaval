using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class WavesManager : MonoBehaviour
{
    public float timeBetweenWave = 15;   // En seconde
    public float timeBetweenEnemy = 2;
    public float timeBeforeStart = 5;
    public float difficultyCoefficient = 1;
    public float difficultyUpscale = 0.01f;     // Augmentation du coefficient de difficulté à chaque vague
    public List<GameObject> allEnemiesType; // Liste des types d'ennemies qui peuvent apparaître

    public GameObject enemyStorage;

    public AudioSource endWave;

    private int waves = 0;
    private bool inWave = false;
    private float endWaveTime;
    private float startWaveTime;
    private float timeLastSpawn;

    private int enemiesRemaining;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endWaveTime = Time.time - timeBetweenWave + timeBeforeStart;
    }

    // Update is called once per frame
    void Update()
    {

        if (inWave)
        {
            Debug.Log("Vague en cours");

            if (enemiesRemaining > 0 && Time.time - timeLastSpawn >= timeBetweenEnemy)
            {
                Debug.Log("Spawn enemie");

                List<GameObject> lanes = GetComponent<GameManager>().getLanes();

                // On spawn un ennemi
                GameObject enemy = Instantiate(allEnemiesType[Random.Range(0, allEnemiesType.Count)], transform.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().prepareEnemy(lanes[Random.Range(0, lanes.Count)], difficultyCoefficient);
                enemy.transform.SetParent(enemyStorage.transform);  // On le range dans le storage

                enemiesRemaining--;
                timeLastSpawn = Time.time;
            }

            if(enemiesRemaining <= 0 && enemyStorage.transform.childCount <= 0 )
            {
                if (endWave) endWave.Play();
                Debug.Log("fin de vague");
                inWave = false;
                endWaveTime = Time.time;
            }

        }
        else
        {
            Debug.Log("Pas de vague en cours");
            if (Time.time - endWaveTime >= timeBetweenWave) {
                // Initialisation d'une vague
                waveInit();
            }
        }

    }

    void waveInit()
    {
        Debug.Log("Lancement de vague");
        if (waves != 0)
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
