using UnityEngine;
using UnityEngine.SceneManagement;

public class ThrowBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private LineRenderer lr;

    private Vector2 startPos;
    private Vector2 endPos;
    private bool isDragging = false;

    public float power = 5f;
    public float maxDragDistance = 3f;

    public int trajectoryPoints = 30;
    public float timeStep = 0.1f;

    public float minVelocity = 3f;
    public float maxVelocity = 15f;
    public bool hasCollided = false;
    public float maxSurfaceAngle = 45f;

    const int SIMPLE_MODE = 1;
    const int PRECISION_MODE = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

        rb.isKinematic = true;
        lr.positionCount = 0;

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            // Touch Start
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touchPos;
                isDragging = true;
            }

            // Dragging
            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 dragVector = startPos - touchPos;

                dragVector = Vector2.ClampMagnitude(
                    dragVector,
                    maxDragDistance
                );

                DrawTrajectory(dragVector * power);
            }

            // Release
            if ((touch.phase == TouchPhase.Ended ||
                 touch.phase == TouchPhase.Canceled) &&
                 isDragging)
            {
                endPos = touchPos;

                Vector2 dragVector = startPos - endPos;

                dragVector = Vector2.ClampMagnitude(
                    dragVector,
                    maxDragDistance
                );

                Shoot(dragVector);

                lr.positionCount = 0;

                isDragging = false;
            }
        }
        /*if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = startPos - currentPos;

            dragVector = Vector2.ClampMagnitude(dragVector, maxDragDistance);

            DrawTrajectory(dragVector * power);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 dragVector = startPos - endPos;

            // Limit drag distance
            dragVector = Vector2.ClampMagnitude(dragVector, maxDragDistance);

            Shoot(dragVector);
            lr.positionCount = 0;

            isDragging = false;
        }*/
    }

    void Shoot(Vector2 direction)
    {
        rb.isKinematic = false;
        rb.linearVelocity = direction * power;
    }

    void DrawTrajectory(Vector2 velocity)
    {
       
        lr.positionCount = trajectoryPoints;

        Vector2 startPosition = transform.position;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * timeStep;

            Vector2 point = startPosition + velocity * t + 0.5f * Physics2D.gravity * t * t;

            lr.SetPosition(i, point);
        }
        lr.material.mainTextureScale = new Vector2(23f, 1f);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasCollided)
            return;

        hasCollided = true;

        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == PRECISION_MODE)
        {
            HandlePrecisionMode(collision);
        }
        else
        {
            HandleNormalMode(collision);
        }


        this.enabled = false;
    }

    void HandleNormalMode(Collision2D collision)
    {
        Debug.Log("First Hit Only");
        ContactPoint2D contact = collision.contacts[0];
        Vector2 surfaceNormal = contact.normal;
        Vector2 velocityDir = rb.linearVelocity.normalized;

        // Angle between velocity and surface
        float angle = Vector2.Angle(-surfaceNormal, velocityDir);

        float speed = rb.linearVelocity.magnitude;

        if (collision.gameObject.CompareTag("Risky"))
        {
            ScoreSystem.instance.RemoveLife(1);

            GetComponent<BallShatter>().Shatter();
            this.enabled = false;
            return;
        }
        if (speed >= minVelocity && speed <= maxVelocity && angle <= maxSurfaceAngle)
        {
            Debug.Log("Follow Surface");

            rb.gravityScale = 1;

            Vector2 surfaceDirection = new Vector2(
                surfaceNormal.y,
                -surfaceNormal.x
            );

            rb.linearVelocity = surfaceDirection * speed;
        }
        else
        {
            ScoreSystem.instance.RemoveLife(1);

            GetComponent<BallShatter>().Shatter();

        }
    }

    void HandlePrecisionMode(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];

        Vector2 surfaceNormal = contact.normal;
        Vector2 velocityDir = rb.linearVelocity.normalized;

        float angle = Vector2.Angle(-surfaceNormal, velocityDir);

        float speed = rb.linearVelocity.magnitude;

        if (collision.gameObject.CompareTag("Risky"))
        {
            ScoreSystem.instance.RemoveLife(1);

            GetComponent<BallShatter>().Shatter();

            return;
        }

        if (collision.gameObject.CompareTag("Safe"))
        {
            
                Debug.Log("Precision Follow");

                Vector2 surfaceDirection = new Vector2(
                    surfaceNormal.y,
                    -surfaceNormal.x
                );

                rb.linearVelocity = surfaceDirection * speed;
            
           
        }
        else
        {
            ScoreSystem.instance.RemoveLife(1);

            GetComponent<BallShatter>().Shatter();
        }
    }
}