using UnityEngine;

public class Cannon : MonoBehaviour {


    public static Cannon instance; // Add by SBI
    [SerializeField] private Projection _projection;
    public Ball top;
    public bool isStart = false;
    public Vector3 spawnPosition;

    // Add by SBI
    private void Awake()
	{
        if (instance == null) instance = this;
        else Destroy(instance);
	}

	private void Start()
	{
        spawnPosition = _ballSpawn.position;
        // Add by SBI
        //_projection.SimulateDiamondPosition(_ballPrefab, spawnPosition, _ballSpawn.forward * _force);
      
        _projection.nextPanPosition = 1;
      
	}

	private void Update() {
		if (isStart)
		{
            HandleControls();
            _projection.SimulateTrajectory(_ballPrefab, _ballSpawn.position, _ballSpawn.forward * _force);
        }

    }

    #region Handle Controls

    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private float _force = 20;
    [SerializeField] private Transform _ballSpawn;
    [SerializeField] private Transform _barrelPivot;
    [SerializeField] private float _rotateSpeed = 30;
    [SerializeField] private ParticleSystem _launchParticles;

    /// <summary>
    /// This is absolute spaghetti and should not be look upon for inspiration. I quickly smashed this together
    /// for the tutorial and didn't look back
    /// </summary>
    private void HandleControls() {
        if (Input.GetKey(KeyCode.S)) _barrelPivot.Rotate(Vector3.right * _rotateSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.W)) _barrelPivot.Rotate(Vector3.left * _rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.down * _rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            var spawned = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);
            top = spawned;
            spawned.Init(_ballSpawn.forward * _force, false);
            _launchParticles.Play();
        }
    }
    #endregion

    // Add by SBI
    public void DevamTop()
	{
        top.GetComponent<Rigidbody>().velocity = GameManager.instance.tempVelocity;
        top.GetComponent<Rigidbody>().useGravity = true;
	}

    public void Firlat()
	{
        // Original
        var spawned = Instantiate(_ballPrefab, _ballSpawn.position, _ballSpawn.rotation);
        top = spawned;
        spawned.Init(_ballSpawn.forward * _force, false);
        _launchParticles.Play();

        // Add by SBI
        //GameObject bardak = GameObject.Find("cup");
        //CameraController.instance.cmVcam.LookAt = bardak.transform;
        //_projection.panPositionList.Add(bardak.transform.position);
        CameraController.instance.CameraFollowBall();
        
        
    }

    public void StartScene()
	{
        StartCoroutine(_projection.DelayForScene(_ballPrefab, _ballSpawn.position, _ballSpawn.forward * _force));

    }


}


/*
 Yap?lacaklar...
-Level tasarlan?rken kullan?lacak olan m?kemmel yola 3er 4er elmas dizilecek
-Win - Lose Senaryolar? 
-Puanlama
-Win ekran?-Lose ekran?-TapToStart Ekran?
-Level Controller
-2 demo level
-Ekran?n sa? ve solu g?r?nmez duvar konup top tencereyi ?skalad???nda duvar?n i?inden ge?ti?i noktada efekt patlas?n 

 */