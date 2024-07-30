using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcCarPrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private bool shouldSpawn = true;
    [SerializeField] private float spawnIntervalMultiplier;
    [SerializeField] private int maxCars = 15;
    [SerializeField] private float destroyDistance = 20f;

    private Coroutine spawnCoroutine;
    private int nextScoreThreshold = 100;
    public List<GameObject> spawnedCars = new List<GameObject>();
    private Transform playerCarTransform;

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("PlayerCar").transform;

        if (shouldSpawn)
        {
            StartSpawning();
        }

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged.AddListener(OnScoreChanged);
        }
    }

    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnCarsRoutine());
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnCarsRoutine()
    {
        while (shouldSpawn)
        {
            UpdateSpawnedCarsList();
            CheckCarDistances();

            if (spawnedCars.Count < maxCars)
            {
                GameObject npcCar = Instantiate(npcCarPrefab, transform.position, Quaternion.identity);
                spawnedCars.Add(npcCar);
                //Debug.Log($"Car spawned. Total cars: {spawnedCars.Count}");
            }
            else
            {
                //Debug.Log("Max cars reached. Waiting to spawn...");
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void UpdateSpawnedCarsList()
    {
        spawnedCars.Clear();

        GameObject[] allCars = GameObject.FindGameObjectsWithTag("NpcCar");

        foreach (GameObject car in allCars)
        {
            if (car.name.Contains("NpcCar(Clone)"))
            {
                spawnedCars.Add(car);
            }
        }

        //Debug.Log($"Updated spawned cars list. Total cars: {spawnedCars.Count}");
    }

    private void OnScoreChanged(float newScore)
    {
        if (newScore >= nextScoreThreshold)
        {
            if (spawnInterval > 0.5)
            {
                spawnIntervalMultiplier = 0.025f;
                spawnInterval -= spawnIntervalMultiplier;
            }
            else if (spawnInterval > 0.25)
            {
                spawnIntervalMultiplier = 0.01f;
                spawnInterval -= spawnIntervalMultiplier;
            }
            nextScoreThreshold += 100;
        }
    }

    void Update()
    {
        CheckCarDistances();
    }

    private void CheckCarDistances()
    {
        //Debug.Log("Checking car distances...");
        for (int i = spawnedCars.Count - 1; i >= 0; i--)
        {
            if (spawnedCars[i] != null && playerCarTransform != null)
            {
                float distance = Vector3.Distance(spawnedCars[i].transform.position, playerCarTransform.position);
                //Debug.Log($"Car {i} distance from player: {distance}");

                if (distance > destroyDistance)
                {
                    //Debug.Log($"Car {i} destroyed for being too far from the player");
                    Destroy(spawnedCars[i]);
                    spawnedCars.RemoveAt(i);
                    //Debug.Log($"Car {i} removed from the list. Remaining cars: {spawnedCars.Count}");
                }
            }
        }
    }
}