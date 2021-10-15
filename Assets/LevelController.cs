using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public static LevelController instance;
	public int levelNo;
	public List<GameObject> levels = new List<GameObject>();
	public List<Vector3> playerVector = new List<Vector3>();
	private GameObject currentLevelObj;

	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this.gameObject);
	}

	private void Start()
	{
		PlayerPrefs.DeleteAll();
		levelNo = PlayerPrefs.GetInt("level");
		if (levelNo == 0) levelNo = 1;
		UIManager.instance.SetLevelText(levelNo);
		LevelStartingEvents();
	}

	public void IncreaseLevelNo()
	{
		levelNo++;
		PlayerPrefs.SetInt("level", levelNo);
		UIManager.instance.SetLevelText(levelNo);
	}

	// Bu fonksiyon oyun ilk açıldığında çalıştırılacak
	public void LevelStartingEvents()
	{
		UIManager.instance.TapToStartScreenEvent();
		CameraController.instance.CameraStartPosition();
		GameManager.instance.gems = 0;
		//GameManager.instance.MakeCollectibleList();
		currentLevelObj = Instantiate(levels[levelNo - 1], Vector3.zero, Quaternion.identity);
		GameManager.instance.FindPans();
		Cannon.instance.transform.rotation = Quaternion.Euler(playerVector[levelNo -1]);
		Cannon.instance.StartScene();
	}

	// next level tuşuna basıldığında UIManager scriptinden çalıştırılacak
	public void NextLevelEvents()
	{
		Destroy(currentLevelObj);
		IncreaseLevelNo();
		LevelStartingEvents();
		StartCoroutine(Projection.instance.FindObstacleObject());
	}

	// restart level tuşuna basıldığında UIManager scriptinden çalıştırılacak
	public void RestartLevelEvents()
	{
		// Deaktif edilen nesnelerin aktif edilmesi..
		Projection.instance.nextPanPosition = 1;
		Cannon.instance.isStart = true;
		CameraController.instance.CameraStartPosition();
		GameObject[] obstacles;
		obstacles = GameObject.FindGameObjectsWithTag("dur");
		for (int i = 0; i < obstacles.Length; i++)
		{
			obstacles[i].GetComponent<BoxCollider>().enabled = true;
		}
		GameObject[] collectibles;
		collectibles = GameObject.FindGameObjectsWithTag("collectible");
		for (int i = 0; i < collectibles.Length; i++)
		{
			collectibles[i].GetComponent<Renderer>().enabled = true;
			collectibles[i].GetComponent<Collider>().enabled = true;
		}
	}
}
