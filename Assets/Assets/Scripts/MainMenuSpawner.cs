using System.Collections;
using UnityEngine;

public class MainMenuSpawner : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private bool shouldSpawn = true;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;

    private Coroutine spawnCoroutine;

    void Start()
    {
        if (shouldSpawn)
        {
            StartSpawning();
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
            GameObject car = Instantiate(carPrefab, spawnPoint.position, Quaternion.identity);
            StartCoroutine(DestroyCarAfterCrossing(car));
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator DestroyCarAfterCrossing(GameObject car)
    {
        while (car.transform.position.y < endPoint.position.y)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        Destroy(car);
    }
}
