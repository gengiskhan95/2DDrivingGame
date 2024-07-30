using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [Header("Road Settings")]
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private Vector3 roadSpawnOffset = new Vector3(0, 10, 0);

    [Header("Destruction Settings")]
    [SerializeField] private float destroyDelay = 4f;
    [SerializeField] private float destructionDistance = 10f;

    [Header("Score Settings")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private int scoreIncrement = 10;
    private bool isRoadGenerated = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerCar") && !isRoadGenerated)
        {
            GenerateNewRoad();
            UpdateScore();
            ScheduleDestruction();
        }
    }

    private void GenerateNewRoad()
    {
        Vector3 spawnLocation = transform.position + roadSpawnOffset;
        Instantiate(roadPrefab, spawnLocation, Quaternion.identity);
        isRoadGenerated = true;
    }

    private void UpdateScore()
    {
        if (scoreManager != null)
        {
            scoreManager.IncreaseScore(scoreIncrement);
        }
        else
        {
            //Debug.LogWarning("ScoreManager instance is not assigned.");
        }
    }

    private void ScheduleDestruction()
    {
        StartCoroutine(CheckAndDestroy());
    }

    private IEnumerator CheckAndDestroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        while (IsPlayerCarWithinDistance(destructionDistance))
        {
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }

    private bool IsPlayerCarWithinDistance(float distance)
    {
        GameObject playerCar = GameObject.FindGameObjectWithTag("PlayerCar");
        if (playerCar != null)
        {
            float currentDistance = Vector3.Distance(transform.position, playerCar.transform.position);
            return currentDistance <= distance;
        }
        return false;
    }
}
