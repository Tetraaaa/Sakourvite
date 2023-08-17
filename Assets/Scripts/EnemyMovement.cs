using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    AudioSource killed;
    private Rigidbody2D rb;
    private float speed = 5f;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.MovePosition(new Vector2(transform.position.x - speed*Time.deltaTime*GameController.Instance.levelOneSpeedRatio, transform.position.y));
        if (transform.position.x < -15) Destroy(gameObject);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
       GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Damage();
       Destroy(gameObject);
    }
}
