using UnityEngine;
using System.Collections;

public class destroyCity : MonoBehaviour 
{

	public GameObject explosion;
	
	
	void OnCollisionEnter(Collision stinger)
	{
        if (stinger.collider.tag == "stinger")
        {
            LevelManager.Instance.CityIsDestroyed();
            Destroy(this.gameObject);
            GameObject explosionClone = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
        }
	}
}
