using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Start : MonoBehaviour
{
	public int soundOn = 0;
	public int modeEasy = 0;
	public void StartBtnClick()
	{
		SceneManager.LoadScene("Level1");
	}


	public void InstructBtnClick()
	{
		SceneManager.LoadScene("Instruction");

	}

	public void BackBtnClick()
	{
		SceneManager.LoadScene("MainMenu");

	}

	public void ExitbtnClick()
	{
		Application.Quit();
	}

	public void soundbtnClick()
	{
		if (GameObject.Find("Sound").GetComponentInChildren<Text>().text == "Sound: OFF")
		{
			GameObject.Find("Sound").GetComponentInChildren<Text>().text = "Sound: ON";
			soundOn = 0;
			PlayerRobotController.audioOn = 0;
		}
		else
		{
			GameObject.Find("Sound").GetComponentInChildren<Text>().text = "Sound: OFF";
			soundOn = 1;
			PlayerRobotController.audioOn = 1;
		}
	}

	public void modebtnClick()
	{

		if (GameObject.Find("Mode").GetComponentInChildren<Text>().text == "Mode: HARD")
		{
			GameObject.Find("Mode").GetComponentInChildren<Text>().text = "Mode: EASY";
			modeEasy = 0;
			PlayerPrefs.SetInt("modeEasy", 1);
			PlayerCollider.modeEasy = 0;
		}
		else
		{
			GameObject.Find("Mode").GetComponentInChildren<Text>().text = "Mode: HARD";
			modeEasy = 1;
			PlayerPrefs.SetInt("modeEasy", 1);
			PlayerCollider.modeEasy = 1;
		}
	}


}
