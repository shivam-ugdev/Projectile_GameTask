using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    public GameObject ballPrefab;
    public float spawnDelay = 0.5f;

    bool isSpawning = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ball"))
            return;

        if (isSpawning)
            return;

        //StartCoroutine(SpawnBall());
        isSpawning = true;

        Invoke(nameof(SpawnBall), spawnDelay);
    }
    void SpawnBall()
    {
        Instantiate(ballPrefab, transform.position, Quaternion.identity);

        isSpawning = false;
    }

    /* IEnumerator SpawnBall()
     {
         isSpawning = true;

         yield return new WaitForSeconds(spawnDelay);

         Instantiate(ballPrefab, transform.position, Quaternion.identity);

         isSpawning = false;
     }*/
}
