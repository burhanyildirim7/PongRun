using UnityEngine;


// Added By SBI
public class DragPan : MonoBehaviour
{
    Rigidbody rb;
	private Vector3 tempPosition;
	public Swipe swipe;
	public float dragSpeed = 5f;


	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		tempPosition = transform.position;

	}

	private void OnMouseDrag()
	{
		if (swipe.SwipeLeft) {
			transform.position = new Vector3(transform.position.x - (dragSpeed * Time.deltaTime * 0.02f), transform.position.y, transform.position.z);
		}
		else if (swipe.SwipeRight)
		{
			transform.position = new Vector3(transform.position.x + (dragSpeed * Time.deltaTime*0.02f), transform.position.y, transform.position.z );
		}
	}

}
