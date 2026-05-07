using UnityEngine;

public class SafePad : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float height = 2f;
    public float speed = 2f;

    private float timer;

    public Transform target;

    void Update()
    {
        Vector2 directionFace = target.position - transform.position;

        float angle = Mathf.Atan2(directionFace.x, directionFace.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        timer += Time.deltaTime * speed;

            float t = Mathf.PingPong(timer, 1f);

            Vector3 pos = Vector3.Lerp(pointA.position, pointB.position, t);

            Vector3 direction = (pointB.position - pointA.position).normalized;

            Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0);

            float arc = Mathf.Sin(t * Mathf.PI) * height;

            pos += perpendicular * arc;

            transform.position = pos;
     }

        private void OnDrawGizmos()
        {
            if (pointA == null || pointB == null)
                return;

            Gizmos.color = Color.green;

            Vector3 previousPoint = pointA.position;

            int segments = 30;

            for (int i = 1; i <= segments; i++)
            {
                float t = i / (float)segments;

                Vector3 pos = Vector3.Lerp(pointA.position, pointB.position, t);

                Vector3 direction = (pointB.position - pointA.position).normalized;

                Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0);

                float arc = Mathf.Sin(t * Mathf.PI) * height;

                pos += perpendicular * arc;

                Gizmos.DrawLine(previousPoint, pos);

                previousPoint = pos;
            }
        }
    }