using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ElephantSDK;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;
	public GameObject TapToStartPanel, LoosePanel, GamePanel, WinPanel;
	public Text soundButtonText, levelNoText, scoreText, gemsText,
	totalScoreTextStartPanel, totalGemsTextStartPanel, totalScoreTextWinPanel, totalGemsTextGamePanel;

	
	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}

	private void Start()
	{
		StartUI();
		totalScoreTextStartPanel.text =PlayerPrefs.GetInt("totalscore").ToString();
		//totalGemsTextStartPanel.text = "Total Gems : " + PlayerPrefs.GetInt("totalgems").ToString();
		//totalGemsTextGamePanel.text = "Total Gems : " + PlayerPrefs.GetInt("totalgems").ToString();
		//totalScoreTextGamePanel.text = "Total Score : " + PlayerPrefs.GetInt("totalscore").ToString();
	}

	public void StartUI()
	{
		TapToStartPanel.SetActive(true);
		WinPanel.SetActive(false);
		LoosePanel.SetActive(false);
		GamePanel.SetActive(false);
	}

	public void SetLevelText(int levelNo)
	{
		levelNoText.text = "LEVEL " + levelNo.ToString();
	}

	// TAPTOSTART TU?UNA BASILDI?INDA  --- G?R?? EKRANINDA VE LEVEL BA?LARINDA
	public void TapToStartButtonClick()
	{
		Cannon.instance.Firlat();
		TapToStartPanel.SetActive(false);
		GamePanel.SetActive(true);
	}

	// RESTART TU?UNA BASILDI?INDA  --- LOOSE EKRANINDA
	public void RestartButtonClick()
	{
		TapToStartPanel.SetActive(true);
		LoosePanel.SetActive(false);
		TapToStartScreenEvent();
		LevelController.instance.RestartLevelEvents();
	}


	// NEXT LEVEL TU?UNA BASILDI?INDA  --- W?N EKRANINDA
	public void NextLevelButtonClick()
	{
		TapToStartPanel.SetActive(true);
		WinPanel.SetActive(false);
		GamePanel.SetActive(false);
		LevelController.instance.NextLevelEvents();
	}

	public void SetScoreText()
	{
		//scoreText.text = "Score : " + GameManager.instance.score.ToString();
	}

	public void SetGemsText()
	{
		//gemsText.text = "Gems : " + GameManager.instance.gems.ToString();
	}

	public void SetTotalScoreText()
	{
		totalScoreTextStartPanel.text = PlayerPrefs.GetInt("totalscore").ToString();
		totalScoreTextWinPanel.text = PlayerPrefs.GetInt("totalscore").ToString();
	}

	public void SetTotalGemsText()
	{
		totalGemsTextStartPanel.text = PlayerPrefs.GetInt("totalgems").ToString();
		totalGemsTextGamePanel.text =  PlayerPrefs.GetInt("totalgems").ToString();
	}

	public void WinScreenEvent()
	{
		totalScoreTextWinPanel.text = PlayerPrefs.GetInt("levelscore").ToString();
		WinPanel.SetActive(true);
		GamePanel.SetActive(false);
		
	}

	public void TapToStartScreenEvent()
	{
		WinPanel.SetActive(false);
		GamePanel.SetActive(false);
		TapToStartPanel.SetActive(true);
		totalScoreTextStartPanel.text = PlayerPrefs.GetInt("totalscore").ToString();
		scoreText.text = PlayerPrefs.GetInt("totalscore").ToString();

	}

	public void LooseScreenEvent()
	{
		LoosePanel.SetActive(true);
		GamePanel.SetActive(false);
	}
}
