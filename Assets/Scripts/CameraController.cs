using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
	public CinemachineVirtualCamera cmVcam;
	public GameObject player;

	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}
    public void CameraStartPosition()
	{
		cmVcam.LookAt = player.transform;
		cmVcam.Follow = player.transform;
	}

	public void CameraFollowBall()
	{
		GameObject cup = GameObject.Find("cup");
		cmVcam.LookAt = cup.transform;
		GameObject ball = GameObject.FindGameObjectWithTag("Ball");
		cmVcam.Follow = ball.transform;
	}

	public void CameraWinPosition()
	{
		GameObject cup = GameObject.Find("cup");
		cmVcam.LookAt = cup.transform;
		cmVcam.Follow = cup.transform;
	}

	public void CameraLoosePosition()
	{
		GameObject cup = GameObject.Find("cup");
		cmVcam.LookAt = cup.transform;
		cmVcam.Follow = cup.transform;
	}
}
