using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{

    public GameObject ammo;
    public float speed;
    public float t;
    Vector3 start = new Vector3(0,-.5f);
    Vector3 end = new Vector3(0,.5f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(ammo != null)
		{
            t += Time.deltaTime;
            ammo.transform.localPosition = Vector2.Lerp(start, end, t/speed);
            if(t/speed >= 1)
			{
                SendAmmo();
			}
		}        
    }

    public void GetAmmo(GameObject a)
	{
        if(ammo == null)
		{
            a.transform.parent = transform;
            ammo = a;
            t = 0;
		}
		else
		{
            Destroy(a);
		}

	}

    public void SendAmmo()
	{
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1);
        Debug.DrawRay(transform.position, transform.up, Color.blue, 1);
		if (hit && hit.collider.CompareTag("Player"))
		{
            Conveyor c = hit.collider.GetComponent<Conveyor>();
            if(c != null)
			{
                c.GetAmmo(ammo);
                ammo = null;
			}
			else
			{
                TowerScript t = hit.collider.GetComponent<TowerScript>();
                if(t != null)
				{
                    t.ammo += 1;
				}
                Destroy(ammo);
			}
		}
		else
		{
            Destroy(ammo);
		}
	}

	public void OnDestroy()
	{
        Destroy(transform.parent.gameObject);
	}
}
