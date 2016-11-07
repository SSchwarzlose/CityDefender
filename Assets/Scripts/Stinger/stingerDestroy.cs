using UnityEngine;
using System.Collections;

public class stingerDestroy : MonoBehaviour
{
    public int objectPoints = 20;
	delegate void StingerDestroyedDelegate();
	StingerDestroyedDelegate stingerDestroyed;

	void Start()
	{
		stingerDestroyed = LevelManager.Instance.DecreaseStinger;
	}



    void OnCollisionEnter(Collision exp)
    {
        Destroy(this.gameObject);
        //LevelManager.Instance.DecreaseStinger();
		stingerDestroyed();
		LevelManager.Instance.DestroyedStingers += 1;

        if (exp.collider.tag == "explosion")
        {
            GameManager.IncreasePoints(objectPoints);
        }

    }
}