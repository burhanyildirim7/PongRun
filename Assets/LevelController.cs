using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElephantSDK;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public int levelNo, tempLevelNo;
    public int totalLevelNo;
    public List<GameObject> levels = new List<GameObject>();
    public List<Vector3> playerVector = new List<Vector3>();
    public List<float> force = new List<float>();
    public List<bool> isCinemachine = new List<bool>();
    public List<Vector3> cameraOffset = new List<Vector3>();
    private GameObject currentLevelObj;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        totalLevelNo = PlayerPrefs.GetInt("level");
        //totalLevelNo = 1;
        if (totalLevelNo == 0)
        {
            totalLevelNo = 1;
            levelNo = 1;
        }

        UIManager.instance.SetLevelText(levelNo);
        LevelStartingEvents();
    }

    public void IncreaseLevelNo()
    {
        tempLevelNo = levelNo;
        totalLevelNo++;
        PlayerPrefs.SetInt("level", totalLevelNo);
        UIManager.instance.SetLevelText(totalLevelNo);
    }

    // Bu fonksiyon oyun ilk açıldığında çalıştırılacak
    public void LevelStartingEvents()
    {

        if (totalLevelNo > levels.Count)
        {
            levelNo = Random.Range(1, levels.Count + 1);
            if (levelNo == tempLevelNo) levelNo = Random.Range(1, levels.Count + 1);
        }
        else
        {
            levelNo = totalLevelNo;
        }
        UIManager.instance.TapToStartScreenEvent();
        UIManager.instance.levelNoText.text = "LEVEL " + totalLevelNo;
        GameManager.instance.gems = 0;
        //GameManager.instance.MakeCollectibleList();
        currentLevelObj = Instantiate(levels[levelNo - 1], Vector3.zero, Quaternion.identity);
        GameManager.instance.FindPans();
        CameraController.instance.isCinemachine = isCinemachine[levelNo - 1];
        //if (!isCinemachine[levelNo - 1]) {
        //	CameraController.instance.cameraPoint = GameObject.Find("cameraPoint").transform;
        //	CameraController.instance.cameraLookAt = GameObject.Find("cameraLookAt").transform;
        //}
        CameraController.instance.SetCameraOffset(cameraOffset[levelNo - 1]);
        CameraController.instance.CameraStartPosition();
        Cannon.instance.transform.rotation = Quaternion.Euler(playerVector[levelNo - 1]);
        Cannon.instance._force = force[levelNo - 1];
        Cannon.instance.StartScene();
        Elephant.LevelStarted(totalLevelNo);

    }

    // next level tuşuna basıldığında UIManager scriptinden çalıştırılacak
    public void NextLevelEvents()
    {
        Elephant.LevelCompleted(totalLevelNo);
        Destroy(currentLevelObj);
        IncreaseLevelNo();
        LevelStartingEvents();
        StartCoroutine(Projection.instance.FindObstacleObject());
        //Elephant.LevelStarted(totalLevelNo);
    }

    // restart level tuşuna basıldığında UIManager scriptinden çalıştırılacak
    public void RestartLevelEvents()
    {
        // Deaktif edilen nesnelerin aktif edilmesi..
        Elephant.LevelFailed(totalLevelNo);
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
        Elephant.LevelStarted(totalLevelNo);
    }
}
