using UnityEngine;

public class Tank : MonoBehaviour
{

    public Animator trackLeft;
    public Animator trackRight;
    public Rigidbody2D rb;
    public BoxCollider2D bc;

    public string keyMoveForward;
    public string keyMoveReverse;
    public string keyRotateRight;
    public string keyRotateLeft;

    bool moveForward = false;
    bool moveReverse = false;
    float moveSpeed = 3f;

    bool rotateRight = false;
    bool rotateLeft = false;
    float rotateSpeed = 100f;
    public float doublerotate = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        rb.angularDamping = 10f;
        rb.gravityScale = 0;
        Vector2 size = bc.size;
        float directionX = Mathf.Sign(transform.localScale.x);
        bc.offset = new Vector2((Mathf.Abs(bc.offset.x) * directionX) -3, bc.offset.y);
        bc.size = new Vector2(2.5f, 4);
    }



    void Update()
    {
        moveForward = Input.GetKey(keyMoveForward) ? true : false;
        moveReverse = Input.GetKey(keyMoveReverse) ? true : false;
        rotateRight = Input.GetKey(keyRotateRight) ? true : false;
        rotateLeft = Input.GetKey(keyRotateLeft) ? true : false;
    }
    void FixedUpdate()
    {
        if (moveForward || moveReverse)
        {
            if (rotateRight)
            {
                if (moveForward)
                {
                    rb.MovePosition(rb.position + (Vector2)transform.up * moveSpeed * Time.fixedDeltaTime);
                    rb.MoveRotation(rb.rotation + rotateSpeed * -1f * Time.fixedDeltaTime);
                }
                else if (moveReverse)
                {
                    rb.MovePosition(rb.position + (Vector2)transform.up * -1 * moveSpeed * Time.fixedDeltaTime);
                    rb.MoveRotation(rb.rotation + rotateSpeed * Time.fixedDeltaTime);
                }
            }
            else if (rotateLeft)
            {
                if (moveForward)
                {
                    rb.MovePosition(rb.position + (Vector2)transform.up * moveSpeed * Time.fixedDeltaTime);
                    rb.AddTorque(rotateSpeed * doublerotate * Time.fixedDeltaTime);
                }
                else if (moveReverse)
                {
                    rb.MovePosition(rb.position + (Vector2)transform.up * -1 * moveSpeed * Time.fixedDeltaTime);
                    rb.AddTorque(rotateSpeed * doublerotate * -1 * Time.fixedDeltaTime);
                }
            }
            else
            {
                if (moveForward)
                {
                    rb.MovePosition(rb.position + (Vector2)transform.up * moveSpeed * Time.fixedDeltaTime);
                }
                else if (moveReverse)
                {
                    rb.MovePosition(rb.position + (Vector2)transform.up * -1 * moveSpeed * Time.fixedDeltaTime);
                }
            }
            trackStart();
        }
        else
        {
            trackStop();
        }
    }

    void trackStart()
    {
        trackLeft.SetBool("isMoving", true);
        trackRight.SetBool("isMoving", true);
    }

    void trackStop()
    {
        trackLeft.SetBool("isMoving", false);
        trackRight.SetBool("isMoving", false);
    }

}