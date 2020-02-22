using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesForward : States
{
	public float distance;

	public StatesForward(float dist = 1.0f)
	{
		distance = dist;
	}

	public override IEnumerator Execute(Robot robot)
	{
		Vector3 direction = robot.transform.forward * distance;
		Vector3 A = robot.transform.position;
		Vector3 B = A + direction;

		float mPerSec = 1.389f;
		float t = distance / mPerSec;
		float time = Time.time;

		while (robot.transform.position != B && !robot.isError)
		{
			float elapse = Time.time - time;
			float percent = elapse / t;
			robot.transform.position = Vector3.Lerp(A, B, percent);
			yield return null;
		}
	}
}

