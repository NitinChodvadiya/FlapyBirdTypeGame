using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{

    public float velocity = 4f;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody.velocity = Vector2.left * velocity;
    }

    private void OnEnable()
    {
        rigidbody.velocity = Vector2.left * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RingDestroyer")
        {
            gameObject.SetActive(false);
        }
    }

}
