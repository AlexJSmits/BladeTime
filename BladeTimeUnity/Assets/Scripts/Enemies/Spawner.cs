using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] wave1;
    public GameObject[] wave2;
    public GameObject[] wave3;
    public Transform[] spawnLocations;

    private GameObject[] enemies;
    private int waveNumber;
    
    bool[] checkArray;

    private void OnEnable()
    {
        checkArray = new bool[spawnLocations.Length];
    }

        private void Start()
    {
        SpawnWave();
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies == null)
        {
            SpawnWave();
            waveNumber += 1;
        }
    }

    void SpawnWave()
    {
        foreach (GameObject enemies in wave1)
        {
            Instantiate(enemies, spawnLocations[LocationRandomiser()].position, Quaternion.identity);
        }

        if (waveNumber > 0)
        {
            foreach (GameObject enemies in wave2)
            {
                Instantiate(enemies, spawnLocations[LocationRandomiser()].position, Quaternion.identity);
            }
        }

        if (waveNumber > 1)
        {
            foreach (GameObject enemies in wave3)
            {
                Instantiate(enemies, spawnLocations[LocationRandomiser()].position, Quaternion.identity);
            }
        }

        checkArray = new bool[spawnLocations.Length];
    }

    int LocationRandomiser()
    {
        int x = 0;
        do
        {
            x = Random.Range(0, spawnLocations.Length);

        } while (checkArray[x] == true);

        checkArray[x] = true;
        return x;
    }
}
