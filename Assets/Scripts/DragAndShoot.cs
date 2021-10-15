using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class DragAndShoot : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    private Rigidbody rb;

    private bool isShoot;
	[SerializeField]
	private float forceMultiplier = 2;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void OnMouseDown()
	{
		mousePressDownPos = Input.mousePosition;
	}

	private void OnMouseDrag()
	{
		Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
		Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * forceMultiplier;

		if (!isShoot) DrawTrajectory.instance.UpdateTrajectory(forceV, rb, transform.position);
	}

	private void OnMouseUp()
	{
		DrawTrajectory.instance.HideLine();
		mouseReleasePos = Input.mousePosition;
		Shoot(mouseReleasePos - mousePressDownPos);
	}

	void Shoot(Vector3 force)
	{
		if (isShoot) return;
	}
}
