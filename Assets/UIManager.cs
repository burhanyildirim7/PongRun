using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		totalScoreTextStartPanel.text = "Total Score : " + PlayerPrefs.GetInt("totalscore").ToString();
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
		levelNoText.text = "Level " + levelNo.ToString();
	}

	// TAPTOSTART TUÞUNA BASILDIÐINDA  --- GÝRÝÞ EKRANINDA VE LEVEL BAÞLARINDA
	public void TapToStartButtonClick()
	{
		Cannon.instance.Firlat();
		TapToStartPanel.SetActive(false);
		GamePanel.SetActive(true);
	}

	// RESTART TUÞUNA BASILDIÐINDA  --- LOOSE EKRANINDA
	public void RestartButtonClick()
	{
		TapToStartPanel.SetActive(true);
		LoosePanel.SetActive(false);
		TapToStartScreenEvent();
		LevelController.instance.RestartLevelEvents();
	}


	// NEXT LEVEL TUÞUNA BASILDIÐINDA  --- WÝN EKRANINDA
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
		totalScoreTextStartPanel.text = "Total Score : " + PlayerPrefs.GetInt("totalscore").ToString();
		totalScoreTextWinPanel.text = "Total Score : " + PlayerPrefs.GetInt("totalscore").ToString();
	}

	public void SetTotalGemsText()
	{
		totalGemsTextStartPanel.text = "Total Gems : " + PlayerPrefs.GetInt("totalgems").ToString();
		totalGemsTextGamePanel.text = "Total Gems : " + PlayerPrefs.GetInt("totalgems").ToString();
	}

	public void WinScreenEvent()
	{
		totalScoreTextWinPanel.text = "Total Score : " + PlayerPrefs.GetInt("totalscore").ToString();
		WinPanel.SetActive(true);
		GamePanel.SetActive(false);
	}

	public void TapToStartScreenEvent()
	{
		WinPanel.SetActive(false);
		GamePanel.SetActive(false);
		TapToStartPanel.SetActive(true);
		totalScoreTextStartPanel.text = "Total Score : " + PlayerPrefs.GetInt("totalscore").ToString();
	}

	public void LooseScreenEvent()
	{
		LoosePanel.SetActive(true);
		GamePanel.SetActive(false);
	}
}
