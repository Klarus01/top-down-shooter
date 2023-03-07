using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject[] projectiles;
    [SerializeField] Sprite[] body;

    [SerializeField] Transform shotPoint;

    public int countProjectile = 0;
    public float timeBetweenShots;
    private float shotTime;

    private int basicDMG = 2;

    Animator cameraAnim;

    private void Start()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].GetComponent<Projectile>().damage = basicDMG;
        }
        cameraAnim = Camera.main.GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButton(0))
        {
            if(Time.time >= shotTime)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (countProjectile == i)
                    {
                        Instantiate(projectiles[i], shotPoint.position, transform.rotation);
                        cameraAnim.SetTrigger("shake");
                        GameObject.FindGameObjectWithTag("Body").GetComponent<SpriteRenderer>().sprite = body[i];
                        shotTime = Time.time + timeBetweenShots;
                        if (countProjectile > 1)
                            countProjectile = 0;
                        else
                            countProjectile++;

                        break;
                    }
                }
            }
        }
    }

    public void DamageUP()
    {
        projectiles[countProjectile].GetComponent<Projectile>().damage++;
    }


}
