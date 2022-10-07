using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    SpriteRenderer rend;
    public float speed;
    HealthScript attackObject;
    public float attackTime;
    float t;
    AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        float angle =(Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg) + 180 ;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if(transform.position.x > 0)
		{
            rend.flipY = true;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(attackObject == null)
		{
            transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
		else
		{
            t -= Time.deltaTime;
            if (t <= 0)
			{
                attackObject.Damage(1);
                t = attackTime;
			}
            
		}
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.isTrigger && collision.CompareTag("Player"))
		{
            attackObject = collision.GetComponent<HealthScript>();
            source.Play();
            t = attackTime;
		}
	}

	private void OnDestroy()
	{
        PlayerScript.resources += 1;
	}
}
