using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
{
    public GameObject player;                                                 // represente le joueur
    public Rigidbody2D rb;                                                    // represente le rigidbody de l'ennemi
    public Animator anim;                                                     // represente l'animator de l'ennemi
    public Collider2D colBoss;                                                // represente le collider de l'ennemi
    public Collider2D colPlayer;                                              // represente le collider du joueur
    public float posPlayer;                                                   // represente la position du joueur
    public float posBoss;                                                     // represente la position du boss
    public float vitesse;                                                     // represente la vitesse de l'ennemi
    public bool isfacing;                                                     // vrai si l'ennemi regarde du cote droit
    public bool running = false;                                              // Vrai si le boss doit courir
    public bool isGrounded;                                                   // vrai si le boss se trouve se trouve au sol

    // Start is called before the first frame update
    void Start()
    {
        posPlayer = player.transform.position.x;
        posBoss = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
        colBoss = GetComponent<Collider2D>();
        colPlayer = player.GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        posPlayer = player.transform.position.x;
        posBoss = transform.position.x;
        running = isGrounded && (((posPlayer - 1.0f) > posBoss) || ((posPlayer + 1.0f) < posBoss));
        if(running)
        {
            anim.SetBool("Run", true);
            Follow();
        }
        else
        {
            anim.SetBool("Run", false);
        }
        if(colBoss.IsTouching(colPlayer))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    // Follow permet a l'ennemi de suivre le joueur
    void Follow()
    {
        if(posPlayer > posBoss)
        {
            if(!isfacing)
            {
                Flip();
            }
            rb.velocity = new Vector2(vitesse, 0);
        }
        else
        {
            if(isfacing)
            {
                Flip();
            }
            rb.velocity = new Vector2(-vitesse, 0);
        }
    }

    // permet a l'ennemi de se retourner
    void Flip()
    {
        isfacing = !isfacing;
        Vector3 vect = transform.localScale;
        vect = new Vector2(-vect.x, vect.y);
        transform.localScale = vect;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
