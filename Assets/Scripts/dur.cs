using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dur : MonoBehaviour
{

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Ball"))
		{
			Cannon.instance.isBallMoving = false;
			GetComponent<BoxCollider>().enabled = false;
			GetComponent<Renderer>().enabled = false;
			GameManager.instance.tempVelocity = other.GetComponent<Rigidbody>().velocity;
			other.GetComponent<Rigidbody>().velocity = Vector3.zero;
			other.GetComponent<Rigidbody>().useGravity = false;
			if(Projection.instance.nextPanPosition < Projection.instance.panPositionList.Count)Projection.instance.nextPanPosition++;		
		}
	}

}
