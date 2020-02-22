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
		Quaternion A = robot.transform.rotation;
		Quaternion B = A * Quaternion.AngleAxis(rotate, Vector3.up);
		float time = Time.time;
		while (robot.transform.rotation != B)
		{
			float elapse = Time.time - time;
			float percent = elapse / 0.5f;
			robot.transform.rotation = Quaternion.Lerp(A, B, percent);
			yield return null;
		}
	}
}

