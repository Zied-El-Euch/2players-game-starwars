using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public string Owner;
    public float speed = 5.0f;
    public float bullettime = 1.0f;
    public float tarchaktime = 0.0f; 

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (tarchaktime >= bullettime)
        {
            Destroy(gameObject);
            tarchaktime = 0;

        }
        else tarchaktime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Bot" && Owner == "Player")
        {
            collision.gameObject.GetComponent<AI>().isDead = true;
            collision.gameObject.GetComponent<Animator>().SetBool("dead", true);
            Destroy(gameObject);
        }
        if(collision.tag == "Player" && Owner == "Bot")
        {
            collision.gameObject.GetComponent<Example_Motion_Controller>().HealthDecrease();
            Destroy(gameObject);
            /*if(collision.gameObject.GetComponent<Example_Motion_Controller>().health <= 0)
            {
                Destroy(collision.gameObject);
            }*/
        }
        if ((collision.tag == "Player" && Owner == "Bot") || (collision.tag == "ground"))
            Destroy(gameObject);
        
    }
    public void Start()
    {

    }
}
