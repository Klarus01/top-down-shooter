using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerEnemy : Enemy
{
    [SerializeField]
    private GameObject batPref;

    private float summonTime;
    public float timeBetweenSummons;


    public float attackSpeed;
    public float stopDistance;
    private float attackTime;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    Vector2 targetPosition;

    public override void Start()
    {
        base.Start();
        targetPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private void Update()
    {
        if (player != null)
        {
            if(Time.time >= summonTime)
            {
                summonTime = Time.time + timeBetweenSummons;
                anim.SetTrigger("summon");
            }
            else if(Vector2.Distance(transform.position, targetPosition) > .1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                targetPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            }

            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                if (Time.time >= attackTime)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    public void Summon()
    {
        if(player != null)
        {
            Instantiate(batPref, transform.position, transform.rotation);
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
