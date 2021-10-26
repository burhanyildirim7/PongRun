using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projection : MonoBehaviour {
    public static Projection instance;
    [SerializeField] private LineRenderer _line;
    public int _maxPhysicsFrameIterations = 100;
    [SerializeField] private Transform _obstaclesParent;
    [SerializeField]private List<Vector3> diamondPositionList = new List<Vector3>();
    public List<Vector3> panPositionList = new List<Vector3>();
    [SerializeField] private GameObject diamond;
    public int nextPanPosition;
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    [SerializeField]private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();
    private bool firstGame = true;
    public bool isDiamondTime = true;

	private void Awake()
	{
        if (instance == null) instance = this;
        else Destroy(this);
	}

	private void Start() {
        StartCoroutine(FindObstacleObject());
    }

    private void CreatePhysicsScene() {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();
       // _obstaclesParent = GameObject.Find("Obstacles").transform;
        foreach (Transform obj in _obstaclesParent) {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            if (!ghostObj.isStatic) _spawnedObjects.Add(obj, ghostObj.transform);
        }
    }

    public void CalculateSpawnObjects()
	{
        
		foreach (GameObject obj in _simulationScene.GetRootGameObjects())
		{
            Destroy(obj.gameObject);
		}
       // _obstaclesParent = GameObject.Find("Obstacles").transform;
        foreach (Transform obj in _obstaclesParent)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = true;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            if (!ghostObj.isStatic) _spawnedObjects.Add(obj, ghostObj.transform);
        }
    }

    private void Update() {
        if (Cannon.instance.isStart)
		{
            foreach (var item in _spawnedObjects)
            {
                item.Value.position = item.Key.position;
                item.Value.rotation = item.Key.rotation;
            }
        }   
    }

    public void SimulateTrajectory(Ball ballPrefab, Vector3 pos, Vector3 velocity) {
        
        var ghostObj = Instantiate(ballPrefab, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

        ghostObj.Init(velocity, true);
        
        _line.positionCount = _maxPhysicsFrameIterations;

		// Added By SBI... ÜRETÝM AÞAMASINDA ALTTAKÝ FOR DÖNGÜSÜ AÇIK OLACAK.. ALTTAKÝ KAPALI OLACAK
		if (GameManager.instance.isPrototyping)
		{
            for (var i = 0; i < _maxPhysicsFrameIterations; i++)
            {
                _physicsScene.Simulate(Time.fixedDeltaTime);
                _line.SetPosition(i, ghostObj.transform.position);
                if (isDiamondTime) diamondPositionList.Add(ghostObj.transform.position);
            }
            isDiamondTime = false;
        }
        else// Added By SBI... YAYINLANIRKEN ALTTAKÝ FOR DÖNGÜSÜ AÇIK OLACAK.. ÜSTTEKÝ if kaldýrýlabilir..
        {
            Vector3 lastGhost = new Vector3();
            for (var i = 0; i < _maxPhysicsFrameIterations; i++)
            {
                _physicsScene.Simulate(Time.fixedDeltaTime);
                //
                if ((ghostObj.transform.position.z <= panPositionList[nextPanPosition].z - .2f))
                {
                    _line.SetPosition(i, ghostObj.transform.position);
                    lastGhost = ghostObj.transform.position;
                }
                else
                {
                    _line.SetPosition(i, lastGhost);
                }
            }
            isDiamondTime = false;
        }
        Destroy(ghostObj.gameObject);
    }

	// Added By SBI Her level baþýnda bir defa çalýþtýracaðým.. Topun izlemesi gereken rotada 300 adet nokta belirliyorum..
	public void SimulateDiamondPosition(Ball ballPrefab, Vector3 pos, Vector3 velocity)
	{

		var ghostObj = Instantiate(ballPrefab, pos, Quaternion.identity);
		SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);
		diamondPositionList.Clear();
		ghostObj.InitForPanAndDiamond(velocity, true);

		for (var i = 0; i < _maxPhysicsFrameIterations; i++)
		{
			_physicsScene.Simulate(Time.fixedDeltaTime);
			diamondPositionList.Add(ghostObj.transform.position);
		}

		Destroy(ghostObj.gameObject);
	}


	// Added By SBI


	public void CreateDimonds()
	{
		GameObject bardak = GameObject.Find("cup");
		panPositionList.Add(bardak.transform.position);
		CameraController.instance.cmVcam.LookAt = bardak.transform;
		for (int i = 1; i < panPositionList.Count-1; i++)
        {
            int count = 0;
            for (int j = 0; j < diamondPositionList.Count; j+=2)
            {
                if (diamondPositionList[j].z > panPositionList[i].z + .2f && diamondPositionList[j].z < panPositionList[i+1].z -.6f && count < 5)
				{
                    Instantiate(diamond, diamondPositionList[j], Quaternion.identity);
                    count++;
                }            
			}
        }    
    }


    // level geçildiðinde çalýþtýrýlacak olan
    public IEnumerator FindObstacleObject()
	{	
        yield return new WaitForSeconds(.1f);
        _obstaclesParent = GameObject.Find("Obstacles").transform;
        _spawnedObjects.Clear();
        if (firstGame)
        {
            firstGame = false;
            CreatePhysicsScene();
        }
        else if (!firstGame)
        {
            CalculateSpawnObjects();
        }    
    }


    public IEnumerator DelayForScene(Ball ballPrefab, Vector3 pos, Vector3 velocity)
	{
        nextPanPosition = 1;
        yield return new WaitForSeconds(.2f);
        isDiamondTime = true;
        SimulateDiamondPosition(ballPrefab, pos, velocity);
        yield return new WaitForSeconds(.1f);
        Cannon.instance.isStart = true;
        CreateDimonds();
        if(!GameManager.instance.isPrototyping)GameManager.instance.ShuflePans();
    }


    // Baþarýlý bir sonuçtan sonra finale ulaþýnca
    public void ClearForNewScene()
	{
        _line.positionCount = 0;
        panPositionList.Clear();
        diamondPositionList.Clear();
        foreach (GameObject obj in _simulationScene.GetRootGameObjects())
        {
            Destroy(obj.gameObject);
        }

        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("collectible");
        for (int i = 0; i < collectibles.Length; i++)
        {
            Destroy(collectibles[i]);
        }
    }

    public void ClearOnlyLine()
	{
        _line.positionCount = 0;
    }
}