using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
	public static DrawTrajectory instance;

	[SerializeField]
	private LineRenderer lineRenderer;

	[SerializeField]
	[Range(3, 30)]
	private int lineSegmentCount = 20;

	private List<Vector3> linePoints = new List<Vector3>();


	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}


	public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
	{
		Vector3 velocity = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;

		float flightDuration = (2 * velocity.y) / Physics.gravity.y;

		float stepTime = flightDuration / lineSegmentCount;

		linePoints.Clear();

		for(int i =0; i < lineSegmentCount; i++)
		{
			float stepTimePassed = stepTime * i;

			Vector3 movementVector = new Vector3(
				velocity.x * stepTimePassed,
				velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
				velocity.z * stepTimePassed);

			RaycastHit hit;
			if(Physics.Raycast(startingPoint,movementVector,out hit, movementVector.magnitude))
			{
				break;
			}
			linePoints.Add(movementVector + startingPoint);
		}

		lineRenderer.positionCount = linePoints.Count;
		lineRenderer.SetPositions(linePoints.ToArray());

	}

	public void HideLine()
	{
		lineRenderer.positionCount = 0;
	}
}
