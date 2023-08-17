using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    private bool isRunning;
    private bool isJumping;
    private float jumpDistance = 3;
    private float maxHeight = 3;
    private float minHeight = -3;
    private bool lowLife = false;
    private Rigidbody2D rigidBody;
    private Collider2D col2D;
    public LayerMask enemyLayer;
    public GameObject death;
    public GameObject hitParticles;

    public AudioSource[] sounds;

    bool isStriking = false;

    private Animator anim;

    private Vector2 targetPosition = new Vector2(0,-3);

    
	// Use this for initialization
	void Start () {
        sounds = GetComponents<AudioSource>();
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            jumpDistance = 1.5f;
        }
        anim = GetComponent<Animator>();
        enemyLayer = LayerMask.GetMask("Enemy");
        isRunning = false;
        rigidBody = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 75f * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > minHeight)
        {
            anim.SetTrigger("playerMoveDown");
            targetPosition = new Vector2(transform.position.x, transform.position.y - jumpDistance);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < maxHeight)
        {
            anim.SetTrigger("playerMoveUp");
            targetPosition = new Vector2(transform.position.x, transform.position.y + jumpDistance);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!isStriking)
            {
                if (Random.Range(0f, 1f) > 0.5f)
                {
                    anim.SetTrigger("playerStrike1");
                    sounds[0].Play();
                }
                else
                {
                    anim.SetTrigger("playerStrike2");
                    sounds[1].Play();
                }
                StartCoroutine(Strike());
            }

        }
	}

    public void Damage()
    {
        GameController.Instance.ScreenShake();
        sounds[2].Play();
        if(!lowLife)
        {
            StartCoroutine(Invincibility());
            GameController.Instance.LowerSpeedOnce();
            lowLife = true;
        }
        else
        {
            Destroy(gameObject);
            GameController.Instance.GameOver();
        }
        
    }

    IEnumerator Invincibility()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.7f);
        Physics2D.IgnoreLayerCollision(9, 10, true);

        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }


    IEnumerator Strike()
    {
        isStriking = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1.5f, enemyLayer);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Killable"))
            {
                Instantiate(hitParticles, transform.position, Quaternion.identity);
                hit.collider.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                hit.collider.gameObject.GetComponent<Collider2D>().enabled = false;
                hit.collider.gameObject.GetComponent<Animator>().SetTrigger("dead");
                GameController.Instance.ScreenShake();
                sounds[3].Play();
                Destroy(hit.collider.gameObject, 0.5f);
            }
        }
        yield return new WaitForSeconds(0.38f);
        isStriking = false;
    }

    


}
