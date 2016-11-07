using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour 
{
	public Rigidbody sidewinderPrefab;
	private Transform spawn;
	private Transform lafette;
	private Transform drehring;
	
	
	private float speed = 1;
	
	void Start () 
	{
		spawn = transform.FindChild("Fuss/Drehscheibe/Beine/Lafette/Lafette/spawn");
		drehring = transform.FindChild("Fuss/Drehscheibe");
		lafette = transform.FindChild("Fuss/Drehscheibe/Beine/Lafette/Lafette");
		
	}
	
	void Update () 
	{
		Vector3 targetPoint = Utility.GetMouseWorldPosition();
		targetFunction(targetPoint);
		Lafette(targetPoint);
		
	}
	
	public void FireSidewinder () 
	{
		Rigidbody sidewinderClone = Instantiate(sidewinderPrefab, spawn.position, spawn.rotation) as Rigidbody;
		Sidewinder sidewinderScript = sidewinderClone.GetComponent<Sidewinder>();
		
//		sidewinderClone.AddForce(spawn.forward * sidewinderScript.speed, ForceMode.VelocityChange);
		

	}
	
	// Rotation des RocketLaunchers um die Y-Achse
	public void targetFunction(Vector3 targetPoint)
	{	
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
		targetRotation.x = 0;
		targetRotation.z = 0;
		drehring.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.time);
	}
	
	// Rotation der Lafette des RocketLaunchers um die X-Ache
	public void Lafette(Vector3 targetPoint)
	{
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
		lafette.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.time);
	}
	
}
