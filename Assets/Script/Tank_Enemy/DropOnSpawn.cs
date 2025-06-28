using System.Collections;
using UnityEngine;

public class DropOnSpawn : MonoBehaviour
{
    public float dropForce = 5f;
    public float upwardForce = 5f;
    public float torque = 10f;
    private float timeToStop= 2f;
    private float timeCur;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        Vector2 dir = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
        rb.AddForce(dir * dropForce + Vector2.up * upwardForce, ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-torque, torque), ForceMode2D.Impulse);
        StartCoroutine(StopAfterDelay());

    }
    IEnumerator StopAfterDelay()
    {
        yield return new WaitForSeconds(timeToStop);

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
