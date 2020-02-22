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
		if (UIRobotProg.isOpen) return;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100.0f, interractLayer))
			{
				Robot robot = hit.collider.gameObject.GetComponent<Robot>();
				if (robot)
				{
					Robot.focusedRobot = robot;
					UIRobotProg.Instance.Show(robot);
				}
			}
		}
    }
}
