// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RocketLauncherControl.cs" author="Sascha Schwarzlose">
//  
// </copyright>
// <summary>
//   This class controls three rocketlauncher in my first game, a clone of Atari's Missile Command, for Unity3D. 
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class RocketLauncherControl : MonoBehaviour 
{
	public AudioClip MissileLaunch;
	public float fireRate = 0.4f;
	float nextSidewinder = 0.0f;
	
	public Launcher[] Launcher;
	
	private bool isControlable = false;
	
	public bool IsControlable
	{
		set { isControlable = value; }
		get { return isControlable; }
	}
	
	
	void Update () 
	{
		Vector3 targetPoint = Utility.GetMouseWorldPosition();
		FiringLauncher(targetPoint);
	}
	
	public void  FiringLauncher(Vector3 targetPoint)
	{
		if(LevelManager.Instance.rockets >= 1 && IsControlable && !GameManager.IsGamePaused && 
			!LevelManager.Instance.IsNextLevel)
		{
			if (Input.GetKeyDown("1") && Time.time > nextSidewinder )
			{
				nextSidewinder = Time.time + fireRate;
				Launcher[1].FireSidewinder();
				PlayMissileSound();
				
			}
			
			if (Input.GetKeyDown("2") && Time.time > nextSidewinder)
			{
				nextSidewinder = Time.time + fireRate;
				Launcher[0].FireSidewinder();
				PlayMissileSound();
			}
			
			if (Input.GetKeyDown("3") && Time.time > nextSidewinder)
			{
				nextSidewinder = Time.time + fireRate;
				Launcher[2].FireSidewinder();
				PlayMissileSound();
			}
			
			if (Input.GetMouseButtonDown(0) && Time.time > nextSidewinder && IsControlable && !GameManager.IsGamePaused 
				&& !LevelManager.Instance.IsNextLevel)
			{
				float result1 = ((targetPoint - Launcher[0].transform.position).sqrMagnitude);
				float result2 = ((targetPoint - Launcher[1].transform.position).sqrMagnitude);
				float result3 = ((targetPoint - Launcher[2].transform.position).sqrMagnitude);
				
				if ( result1 <= result2 && result1 <= result3 )
				{
					nextSidewinder = Time.time + fireRate;
					Launcher[0].FireSidewinder();
					PlayMissileSound();
				}
				
				else
					if ( result2 < result1 && result2 < result3 )
					{
						nextSidewinder = Time.time + fireRate;
						Launcher[1].FireSidewinder();
						PlayMissileSound();
					}
				
				else
					if ( result3 < result1 && result3 < result2 )
					{
						nextSidewinder = Time.time + fireRate;
						Launcher[2].FireSidewinder();
						PlayMissileSound();
					}
			}
		}
	}
	
	void PlayMissileSound()
	{
		
			if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3"))
			{
			 	GetComponent<AudioSource>().PlayOneShot(MissileLaunch);
			}
		
	}
	
	
	
}
