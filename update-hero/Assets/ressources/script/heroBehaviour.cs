using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroBehaviour : MonoBehaviour
{

    public Rigidbody2D rb;                    // Rigidbody du hero
    public float vitesse;                     // Vitesse du hero
    public float upforce;                     // Distance de saut du hero
    private Animator anim;                    // Animation du hero
    public bool isfacing = true;              // Definit si le hero est tourne vers la droite


    private bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded)
        {
            rb.velocity = Vector2.zero;
            anim.ResetTrigger("Run");
            anim.ResetTrigger("Jump");
            if(Input.GetKey(KeyCode.RightArrow))
            {
                if(!(isfacing))
                {
                    Flip();
                }
                rb.velocity = new Vector2(vitesse, 0);
                anim.SetTrigger("Run");
            }
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                if(isfacing)
                {
                    Flip();
                }
                rb.velocity = new Vector2(-vitesse, 0);
                anim.SetTrigger("Run");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(!(col.gameObject.CompareTag("Ground")))
        {
            isGrounded = false;
        }
    }

    protected void Flip()
    {
        isfacing = !isfacing;
        Vector3 vecteur = transform.localScale;
        vecteur.x*=-1;
        transform.localScale = vecteur;
    }
}
