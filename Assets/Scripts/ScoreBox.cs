using UnityEngine;

public class ScoreBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ScoreSystem.instance.AddScore(1);

            Destroy(collision.gameObject);
        }
    }
}
