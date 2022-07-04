using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 2.4f;
    private Rigidbody2D rigidbody;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !rigidbody.isKinematic)
        {
            rigidbody.velocity = Vector2.up * velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered with : " + collision.gameObject.name);

        if (collision.gameObject.tag == "RingInside")
        {
            GameManager.score++;
            GameManager.inst.updateScore();
        }
        else if (collision.gameObject.tag == "Land")
        {
            playerDie();
        }
    }

    public void disablePlayer(bool disable)
    {       
        rigidbody.bodyType = disable ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        gameObject.transform.localPosition = new Vector3(0, 0, -2);
    }

    public void playerDie()
    {
        GameManager.inst.gameOver();
    }
}