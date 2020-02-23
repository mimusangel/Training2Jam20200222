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

		bool moveWithBox = false;
		if (robot.box)
		{
			Vector3 bPos = robot.box.transform.position;
			bPos.y = A.y;
			float distance = Vector3.Distance(bPos, A);
			moveWithBox = distance <= 1.0f;
		}
		while (robot.transform.position != B && !robot.isError)
		{
			float elapse = Time.time - time;
			float percent = elapse / t;
			robot.transform.position = Vector3.Lerp(A, B, percent);
			if (robot.box && moveWithBox)
			{
				Vector3 boxPos = robot.transform.position + robot.transform.forward;
				boxPos.y = robot.box.transform.position.y;
				robot.box.transform.position = boxPos;
			}
			yield return new WaitForEndOfFrame();
		}
	}
}

