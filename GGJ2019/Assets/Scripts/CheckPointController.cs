using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {
[SerializeField] Sprite sprite1;
[SerializeField] Sprite sprite2;
[SerializeField] bool active;

    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            active = value;
        }
    }


    // Use this for initialization
    void Start () 
	{

	active = false;	
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag =="Player")
		{
			active = true;
			GetComponent<SpriteRenderer>().sprite = sprite2;

		}
	}
}
