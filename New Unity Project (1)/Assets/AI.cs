using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Transform player;
    public bool isDead = false;
    public GameObject BulletPrefab;
    public float AttackRange = 5.0f;
    public float AttackingTime = 0.0f;
    public Vector2 Direc;
    public float AttackRequiredTime = 1f;

    private void Start()
    {
        //player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (!isDead && player != null && Vector2.Distance(player.position, transform.position) <= AttackRange)
        {
            if (AttackingTime >= AttackRequiredTime)
            {
                GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                Vector2 dir = (transform.position - player.transform.position).normalized;
                if (dir.x > 0)
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                else
                    transform.rotation = Quaternion.identity;
                bullet.GetComponent<Bullet>().direction = -dir;
                bullet.GetComponent<Bullet>().Owner = "Bot";
                AttackingTime = 0;
            }
            else
            {
                AttackingTime += Time.deltaTime;
            }
        }
        else AttackingTime = 0;
    }

    public void DisparaiteMelDenya()
    {
        Destroy(gameObject);
    }
}
