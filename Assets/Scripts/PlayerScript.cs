using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public static int resources = 22;
    public Text resourceText;

    public StructureScriptableObject[] structures;
    public int curStructure = 0;
    public GameObject GhostStructure;
    public float curRot = 0;

    private float ScrollLockout;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        SwitchStructure();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScrollLockout != -10)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                curStructure = (curStructure + 1) % 3;
                ScrollLockout = .25f;
                SwitchStructure();
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                curStructure = (curStructure - 1) % 3;
                if (curStructure == -1)
                {
                    curStructure = 2;
                }
                ScrollLockout = .25f;
                Debug.Log(curStructure);
                SwitchStructure();
            }
        }
        else
        {
            ScrollLockout -= Time.deltaTime;
            if (ScrollLockout <= 0)
            {
                ScrollLockout = -10;
            }
        }

		if (Input.GetKeyDown(KeyCode.R))
		{
            curRot = (curRot - 90);
            if(curRot < 0)
			{
                curRot = 270;
			}
            GhostStructure.transform.rotation = Quaternion.Euler(0, 0, curRot);
        }

        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePos = new Vector2(Mathf.RoundToInt(MousePos.x), Mathf.RoundToInt(MousePos.y));
        GhostStructure.transform.position = MousePos;

		if (Input.GetMouseButtonDown(0))
		{
            Debug.DrawRay(MousePos, Vector2.right * .1f,Color.blue,1);
            RaycastHit2D hit = Physics2D.Raycast(MousePos-Vector2.right, Vector2.right, 1.1f);
            bool canPlace = true;
			if (hit && hit.collider.CompareTag("Player"))
			{
                Debug.Log(hit.collider.gameObject.name);
                canPlace = false;
			}
            if (canPlace && resources >= structures[curStructure].cost)
			{
                resources -= structures[curStructure].cost;
                GhostStructure.transform.GetChild(0).gameObject.SetActive(true);
                GhostStructure = null;
                SwitchStructure();
                source.Play();
            }

            
		}else if (Input.GetMouseButtonDown(1))
		{
            RaycastHit2D hit = Physics2D.Raycast(MousePos - Vector2.right, Vector2.right, 1.1f);
            if (hit && hit.collider.CompareTag("Player"))
            {
                Debug.Log(hit.collider.transform.parent.name);
                Destroy(hit.collider.transform.parent.gameObject);
            }
        }

        resourceText.text = resources.ToString();

    }

    public void SwitchStructure()
	{
        if(GhostStructure != null)
		{
            Destroy(GhostStructure);
        }
        GhostStructure = Instantiate(structures[curStructure].Structure, Vector3.zero, Quaternion.Euler(0, 0, curRot));
    }
}
