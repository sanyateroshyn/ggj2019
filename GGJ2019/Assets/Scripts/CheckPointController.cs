using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {

[SerializeField] public bool active;

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
	
}
