using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public int damage;
    public Enemy[] enemies;

    public GameObject deathEffect;

    private int halfHealth;
    private Animator anim;

    private Slider healthBar;

    private SceneTrasitions sceneTransition;


    private void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        sceneTransition = FindObjectOfType<SceneTrasitions>();
    }



    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;

        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            healthBar.gameObject.SetActive(false);
            sceneTransition.LoadScene("Win");
        }

        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }

        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }


}
