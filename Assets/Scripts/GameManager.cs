using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
	public List<GameObject> pans = new List<GameObject>();
	public Vector3 tempVelocity;
	public int gems,score;
	public bool isPrototyping;

	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}

	public void increaseScore(int add)
	{
		score = score + add;
		UIManager.instance.scoreText.text = "Score : " + score.ToString();
		PlayerPrefs.SetInt("totalscore", score);
	}

	public void FindPans()
	{
		pans.Clear();
		GameObject[] pansArray = GameObject.FindGameObjectsWithTag("pan");
		for (int i = 0; i < pansArray.Length; i++)
		{
			pans.Add(pansArray[i]);
		}
		pans = pans.OrderBy(x => x.name).ToList();
		
	}

	public void DestroyPans()
	{
		for (int i = 0; i < pans.Count; i++)
		{
			Destroy(pans[i].gameObject);
		}
	}

	// Her levele baþlarken tavalarýn konumlarýný ayarlamak için...
	public void ShuflePans()
	{
		for (int i = 1; i< pans.Count; i++)
		{
			float random = Random.Range(.1f,.3f);
			int rnd = Random.Range(0,2);
			if(rnd == 0)
			{
				pans[i].transform.position = new Vector3(pans[i].transform.position.x + random, 
					pans[i].transform.position.y, pans[i].transform.position.z);
			}
			else
			{
				pans[i].transform.position = new Vector3(pans[i].transform.position.x - random,
					pans[i].transform.position.y, pans[i].transform.position.z);
			}
		}
		Debug.Log(pans.Count);
	}
}
