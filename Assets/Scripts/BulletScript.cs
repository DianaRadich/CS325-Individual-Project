using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float speed;
	Rigidbody2D rb2d;

	private void Start()
	{
		Destroy(gameObject, 5);
	}

	// Update is called once per frame
	void Update()
    {
		transform.Translate(Vector3.right * speed * Time.deltaTime);
    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			collision.GetComponent<HealthScript>().Damage(1);
			Destroy(gameObject);
		}
	}
}
