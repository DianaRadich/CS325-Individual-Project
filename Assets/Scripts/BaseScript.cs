using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseScript : MonoBehaviour
{

    public static BaseScript Base;

    private void Awake()
	{
		if(Base == null)
		{
            Base = this;
		}
		else
		{
            Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnDestroy()
	{
		SceneManager.LoadScene("LostScene");
	}
}
