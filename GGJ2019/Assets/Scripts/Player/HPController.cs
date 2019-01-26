using CnControls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPController : MonoBehaviour {

	[SerializeField] int maxLifes;
	[SerializeField] int maxHp;
	[SerializeField] int lifes;
	[SerializeField] int hp;
 	[SerializeField] Transform lifesBar;
	[SerializeField] Image hpBarContent;
	Transform[] lifesImages;
	
	void Start () {

		lifesImages = lifesBar.GetComponentsInChildren<Transform>();
			//if(!PlayerPrefs.HasKey("playerLifes")&&!PlayerPrefs.HasKey("playerHp"))
			//{
				lifes = maxLifes;
				hp = maxHp;
			//}
			//else
			//{
			//	lifes = PlayerPrefs.GetInt("playerLifes");
			//	hp = PlayerPrefs.GetInt("playerHp");
			//}
	hpBarUpdate();	
	lifesBarController();

	}



	public void takeHp(int takeHp)
	{
		if(takeHp == 100)
		{
			takeLife();
		}
		else
		{
			if(hp+takeHp>maxHp)
			{
				hp=maxHp;
			}
			else
			{
				hp+=takeHp;
			}
			hpBarUpdate();	
		}
	}

	public void takeLife()
	{

		if(lifes!=maxLifes)
		{
			lifes++;
			lifesBarController();
		}
		else
		{
			hp=maxHp;
			hpBarUpdate();	
		}
		
	}

	public void takeDamage(int damage)
	{
		if(hp-damage<=0)
		{
			if(lifes>1) 
			{
				hp = maxHp;
				lifes--;

				lifesBarController();
				
				 
			}
			else
			{
				 SceneManager.LoadScene(0);
				 //TODO : revork die;

			}
		}
		else
		{
			hp-=damage;
		}

		hpBarUpdate();	
		
	}

	void hpBarUpdate()
	{
		hpBarContent.fillAmount= (hp*1f)/(maxHp*1f);
	  	
	}

	void lifesBarController()
	{

		for(int i = 1; i<lifesImages.Length;i++)
		{
			lifesImages[i].gameObject.SetActive(false);
		}
		for(int i = 1; i<=lifes;i++)
		{
			lifesImages[i].gameObject.SetActive(true);
		}

		
	}

}
