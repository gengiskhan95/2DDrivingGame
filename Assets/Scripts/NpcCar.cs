using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcCar : MonoBehaviour
{
    [Header("Speed Settings")]
    public float speedMin;
    public float speedMax;
    private float currentSpeed;
    private const float speedMultiplier = 50f;
    private float originalSpeed;

    [Header("Car Sprites")]
    public Sprite[] carSprites;

    [Header("Lane Settings")]
    public int laneMin;
    public int laneMax;
    public int distanceFromPlayer;

    [Header("Game Settings")]
    public int gameOverSceneIndex = 2;

    [Header("Destruction Settings")]
    public float collisionDespawnDelay = 3f;
    public float offscreenDespawnDelay = 3f;

    [Header("Detection Settings")]
    public float detectionRange = 5f;
    public LayerMask npcLayerMask;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        AssignRandomLane();
        AssignRandomSpeed();
        AssignRandomCarSprite();
        originalSpeed = currentSpeed;
    }

    void AssignRandomLane()
    {
        float[] lanePositions = { -1.37f, -0.475f, 0.47f, 1.37f };
        ShuffleArray(lanePositions);
        bool laneAssigned = false;

        for (int i = 0; i < lanePositions.Length; i++)
        {
            Vector3 checkPosition = new Vector3(transform.position.x + lanePositions[i], transform.position.y + distanceFromPlayer, 0);
            Collider2D hitCollider = Physics2D.OverlapCircle(checkPosition, 0.5f, npcLayerMask);

            //Debug.Log($"Checking lane position: {lanePositions[i]} at {checkPosition}, hit collider: {hitCollider}");

            if (hitCollider == null)
            {
                transform.position = checkPosition;
                laneAssigned = true;
                //Debug.Log($"Assigned to lane position: {lanePositions[i]} at {checkPosition}");
                break;
            }
        }

        if (!laneAssigned)
        {
            //Debug.LogWarning("No valid lane found for NPC car");
        }
    }


    void ShuffleArray(float[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            float temp = array[i];
            array[i] = array[rnd];
            array[rnd] = temp;
        }
    }


    void AssignRandomSpeed()
    {
        currentSpeed = Random.Range(speedMin, speedMax);
    }

    void AssignRandomCarSprite()
    {
        if (carSprites.Length > 0)
        {
            int spriteIndex = Random.Range(0, carSprites.Length);
            spriteRenderer.sprite = carSprites[spriteIndex];
        }
        else
        {
            //Debug.LogWarning("No car sprites assigned");
        }
    }

    void FixedUpdate()
    {
        DetectFrontCar();
        rb2d.velocity = new Vector2(rb2d.velocity.x, currentSpeed * speedMultiplier * Time.deltaTime);
    }

    void DetectFrontCar()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, detectionRange, npcLayerMask);
        if (hit.collider != null && hit.collider.CompareTag("NpcCar"))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speedMin, Time.deltaTime * 2);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, originalSpeed, Time.deltaTime * 2);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCar"))
        {
            //Debug.Log("Collision with player car. Game over.");
            ScoreManager.Instance.SaveHighScore();
            SceneManager.LoadScene(gameOverSceneIndex);
        }
        else
        {
            //Debug.Log("Collision detected. Starting despawn coroutine.");
            StartCoroutine(DespawnAfterDelay(collisionDespawnDelay));
        }
    }


    private IEnumerator DespawnAfterDelay(float delay)
    {
        //Debug.Log($"Car will despawn after {delay} seconds");
        yield return new WaitForSeconds(delay);
        //Debug.Log("Car despawned");
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Overtake"))
        {
            ScoreManager.Instance.IncreaseScore(25);
        }
    }
}