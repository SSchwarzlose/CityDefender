using UnityEngine;

public static class Utility 
{
	public static Vector3 GetMouseWorldPosition()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 892;
		return Camera.main.ScreenToWorldPoint(mousePos);		
	}	
	
}
