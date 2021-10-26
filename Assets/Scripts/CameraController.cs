using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
	public CinemachineVirtualCamera cmVcam;
	public GameObject player;
	public Transform cameraPoint, cameraLookAt;
	public bool isCinemachine;

	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}
    public void CameraStartPosition()
	{
		cmVcam.LookAt = player.transform;
		cmVcam.Follow = player.transform;
		//if (isCinemachine)
		//{
		//	cmVcam.LookAt = player.transform;
		//	cmVcam.Follow = player.transform;
		//}
		//else
		//{
		//	Camera.main.transform.position = cameraPoint.position;
		//	Camera.main.transform.LookAt(cameraLookAt,Vector3.up);
		//}
		
	}

	public void CameraFollowBall()
	{
		if (isCinemachine)
		{
			GameObject cup = GameObject.Find("cup");
			cmVcam.LookAt = cup.transform;
			GameObject ball = GameObject.FindGameObjectWithTag("Ball");
			cmVcam.Follow = ball.transform;
		}
	}

	public void CameraWinPosition()
	{
		if (isCinemachine)
		{
			GameObject cup = GameObject.Find("cup");
			cmVcam.LookAt = cup.transform;
			cmVcam.Follow = cup.transform;
		}
	}

	public void CameraLoosePosition()
	{
		if (isCinemachine)
		{
			GameObject cup = GameObject.Find("cup");
			cmVcam.LookAt = cup.transform;
			cmVcam.Follow = cup.transform;
		}	
	}

	public void SetCameraOffset(Vector3 followOffset)
	{
		cmVcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = followOffset;
	}
}
