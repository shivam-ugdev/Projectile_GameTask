using UnityEngine;

public class BallShatter : MonoBehaviour
{
    public GameObject shatteredPrefab;

    public float force = 5f;

    public void Shatter()
    {
        GameObject shattered = Instantiate(
            shatteredPrefab,
            transform.position,
            transform.rotation
        );

        Rigidbody2D[] pieces = shattered.GetComponentsInChildren<Rigidbody2D>();

        foreach (Rigidbody2D piece in pieces)
        {
            Vector2 randomDir = Random.insideUnitCircle.normalized;

            piece.AddForce(randomDir * force, ForceMode2D.Impulse);
        }
    

        Destroy(shattered, 2f);

        Destroy(gameObject);
    }
}