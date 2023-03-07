using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public int health;

    private Rigidbody2D rb;
    private Animator anim;
    public Animator hurtAnim;

    public Shooting shootingScript;

    private Vector2 moveAmount;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private SceneTrasitions sceneTransition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        shootingScript = GameObject.FindGameObjectWithTag("ShotPoint").GetComponent<Shooting>();
        sceneTransition = FindObjectOfType<SceneTrasitions>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        UpdateHealthUI(health);
        hurtAnim.SetTrigger("hurt");

        if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransition.LoadScene("Lose");
        }
    }

    public void Pickups(GameObject pickup)
    {
        if (pickup.name == "dmgUP(Clone)")
            shootingScript.DamageUP();
        else if (pickup.name == "hpUP(Clone)")
        {
            health++;
            UpdateHealthUI(health);
        }
        else if (pickup.name == "spdUP(Clone)")
            speed+=.1f;
    }

    void UpdateHealthUI(int currentHealth)
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
