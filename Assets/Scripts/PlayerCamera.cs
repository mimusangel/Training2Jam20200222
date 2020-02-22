using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	public LayerMask interractLayer;

	Camera cam;

    void Start()
    {
		cam = GetComponent<Camera>();
    }
	
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100.0f, interractLayer))
			{
				Robot robot = hit.collider.gameObject.GetComponent<Robot>();
				if (robot)
				{
					UIRobotProg.Instance.Show(robot);
				}
			}
		}
    }
}
