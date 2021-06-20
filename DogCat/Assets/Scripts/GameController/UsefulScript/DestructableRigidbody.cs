using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableRigidbody : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_vForceDirection;

    [SerializeField]
    private float torque;

    private new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        float randTorque = Random.Range(-50, 50);
        float randDirectionX = Random.Range(m_vForceDirection.x - 50, m_vForceDirection.x + 50);
        float randDirectionY= Random.Range(m_vForceDirection.y - 50, m_vForceDirection.y + 50);

        m_vForceDirection.x = randDirectionX;
        m_vForceDirection.y = randDirectionY;

        rigidbody2D = GetComponent<Rigidbody2D>();
        if(rigidbody2D)
        {
            rigidbody2D.AddForce(m_vForceDirection);
            rigidbody2D.AddTorque(randTorque);
        }

        Invoke("_DestroySelft", Random.Range(2.5f, 4f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Remove Collider2D to make fragments drop under ground
        if(collision.collider.CompareTag("GroundEdge"))
        {
            _RemoveCollider2D();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
    private void _DestroySelft()
    {
        Destroy(gameObject);
    }

    private void _RemoveCollider2D()
    {
        Collider2D collider2D = GetComponent<Collider2D>();
        if(collider2D)
        {
            Destroy(collider2D);
        }
    }
}
