using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{

	public List<GameObject> targets = new List<GameObject>();
	public float fireSpeed;
	public GameObject bullet;
	float fireTime;
	public int ammo;
	AudioSource source;

	private void Start()
	{
		source = transform.parent.GetComponent<AudioSource>();
	}

	private void Update()
	{
		if(targets.Count > 0 && targets[0] == null)
		{
			targets.RemoveAt(0);
		}
		if(fireTime <= 0 && ammo > 0 && targets.Count > 0)
		{
			Fire();
		}

		if(fireTime > 0)
		{
			fireTime -= Time.deltaTime;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			targets.Add(collision.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D other)		
	{
		if (other.CompareTag("Enemy"))
		{
			targets.Remove(other.gameObject);
		}
		
	}

	void Fire()
	{
		Vector3 t = targets[0].transform.position - transform.position;
		float angle = Mathf.Atan2(t.y, t.x)*Mathf.Rad2Deg;
		Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));
		fireTime = fireSpeed;
		ammo--;
		source.Play();
	}

	public void OnDestroy()
	{
		Destroy(transform.parent.gameObject);
	}
}
