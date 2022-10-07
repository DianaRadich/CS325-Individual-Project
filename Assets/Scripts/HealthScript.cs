using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
	[SerializeField]
	private int health;
	public AudioClip destroyed;
	public void Damage(int amount)
	{
		health -= amount;
		if(health <= 0)
		{
			if(destroyed != null)
			{
				AudioSource.PlayClipAtPoint(destroyed, transform.position);
			}
			
			Destroy(gameObject);
		}
	}
}
