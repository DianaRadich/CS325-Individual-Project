using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillScript : MonoBehaviour
{

    public float drillTime;
    public float t;
    public Conveyor[] conveyor = new Conveyor[4];
    public Vector2[] cDirs;
    public int curDir;
    public GameObject ammo;


    // Update is called once per frame
    void Update()
    {
        if (t <= 0)
        {
            SendAmmo();
            Debug.Log("sent");
            t = drillTime;
        }
        t -= Time.deltaTime;
    }

    public void SendAmmo()
    {
        int i;

        for (i = 0; i < 4; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, cDirs[i], 1);
            Debug.DrawRay(transform.position, cDirs[i], Color.blue, 1);
            if (hit && hit.collider.CompareTag("Player"))
            {
                Conveyor c = hit.collider.GetComponent<Conveyor>();
                if (c != null)
                {
                    conveyor[i] = c;
                }
            }
        }
        i = 0;
        do
        {
            curDir = (curDir + 1) % 4;
            i++;
            if (i == 5)
            {
                return;
            }
        } while (conveyor[curDir] == null);

        conveyor[curDir].GetAmmo(Instantiate<GameObject>(ammo, transform.position, Quaternion.identity));
    }

    public void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}

