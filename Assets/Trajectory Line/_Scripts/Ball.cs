using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameObject _poofPrefab;
    [SerializeField] private GameObject diamondParticlePrefab;
    private bool _isGhost;

    public void Init(Vector3 velocity, bool isGhost) {
        _isGhost = isGhost;
        _rb.AddForce(velocity, ForceMode.Impulse);
    }


	// Add By SBI
	public void InitForPanAndDiamond(Vector3 velocity, bool isGhost)
	{
        _isGhost = isGhost;
		_rb.AddForce(velocity, ForceMode.Impulse);
	}


	// Add By SBI


	public void OnCollisionEnter(Collision col) {
        // ghost ise tavalarýn koordinatlarýný alabilirim...
        if (col.transform.CompareTag("pan") && _isGhost && Projection.instance.isDiamondTime) {
            if(!Projection.instance.panPositionList.Contains(transform.position))
			{
                Projection.instance.panPositionList.Add(transform.position);               
            }
            return;
        } else if (_isGhost)
		{
            return;
		}

		if (col.transform.CompareTag("duvar"))
		{
            CameraController.instance.CameraLoosePosition();
            Destroy(gameObject);
            UIManager.instance.LooseScreenEvent();
            Cannon.instance.isStart = false;
        }
        Instantiate(_poofPrefab, col.contacts[0].point, Quaternion.Euler(col.contacts[0].normal));
    }


    // Add By SBI
    private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("collectible"))
		{
            GameManager.instance.gems++;
            Instantiate(diamondParticlePrefab, other.transform.position, Quaternion.identity);
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<Collider>().enabled = false;
            GameManager.instance.increaseScore(100);
		}
        else if (other.CompareTag("final"))
		{
            // Final iþlemleri... kamera iþlemleri... animasyon iþlemleri... efekt iþlemleri v.s. v.s.
            other.GetComponent<Animator>().SetTrigger("cup");
            other.transform.GetChild(0).gameObject.SetActive(true);
            CameraController.instance.CameraWinPosition();
            Destroy(gameObject);
            UIManager.instance.WinScreenEvent();
            Cannon.instance.isStart = false;
            Projection.instance.ClearForNewScene();
            GameManager.instance.DestroyPans();
            GameManager.instance.increaseScore(200);
		}else if (other.CompareTag("duvar"))
		{
            CameraController.instance.CameraLoosePosition();
            Destroy(gameObject);
            UIManager.instance.LooseScreenEvent();
            Cannon.instance.isStart = false;
        }
	}

}