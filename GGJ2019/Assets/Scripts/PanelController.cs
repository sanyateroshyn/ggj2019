using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour {



[SerializeField] Transform panel;
[SerializeField] Transform point1;
[SerializeField] Transform point2;
[SerializeField] bool horizontal;// horizontal= true; vertical = false;
[SerializeField] float transformSpeed;
				bool right;// true= transform forward(up),  false= transform back(down);
	private void Start() {
		right = true;
	}
 
 	 void FixedUpdate() 
	  {

		if(horizontal){
			if((Mathf.Abs(panel.position.x-point2.position.x)<=0.3)||(Mathf.Abs(panel.position.x-point1.position.x)<=0.3)) right = !right;		 
		}
		else
			if((Mathf.Abs(panel.position.y-point2.position.y)<=0.3)||(Mathf.Abs(panel.position.y-point1.position.y)<=0.3)) right = !right;
				 
		if(horizontal)
		{
			if(right)
			{
				panel.position = new Vector3(panel.position.x+transformSpeed*Time.deltaTime,panel.position.y, panel.position.z);
			}
			else
			{
				panel.position = new Vector3(panel.position.x-transformSpeed*Time.deltaTime,panel.position.y, panel.position.z);
			}
		}
		else
		{
			if(right)
			{
				panel.position = new Vector3(panel.position.x,panel.position.y+transformSpeed*Time.deltaTime, panel.position.z);
			}
			else
			{
				panel.position = new Vector3(panel.position.x,panel.position.y-transformSpeed*Time.deltaTime, panel.position.z);
			}
		}
	}
}
