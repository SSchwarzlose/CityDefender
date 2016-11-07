using UnityEngine;
using System.Collections;

public class vehicleDestroy : MonoBehaviour 
{
    public int objectPoints = 40;

    void Update()
    {
        Invoke("DestroyAfterTime", 7);
    }

    void OnCollisionEnter(Collision expl)
    {
        Destroy(this.gameObject);
        if (expl.collider.tag == "explosion")
        {
            GameManager.IncreasePoints(objectPoints);
        }
    }

    void DestroyAfterTime()
    {
        Destroy(this.gameObject);
    }
}
