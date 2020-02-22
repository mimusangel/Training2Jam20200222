using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesRotate : States
{
	public float rotate;

	public StatesRotate(float rot = 90.0f)
	{
		rotate = rot;
	}

	public override IEnumerator Execute(Robot robot)
	{
		Quaternion origin = robot.transform.rotation;

		float t = (0.5f * Mathf.Abs(rotate)) / 90.0f;
		float percent = 0.0f;
		float time = Time.time;
		
		while (percent <= 1.0f)
		{
			float elapse = Time.time - time;
			percent = elapse / t;
			robot.transform.rotation = origin * Quaternion.AngleAxis(rotate * percent, Vector3.up);
			yield return null;
		}
	}
}

