using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
	

	public void Level1BtnClick()
	{
		SceneManager.LoadScene("Level1");
	}

	public void Level2BtnClick()
	{
		SceneManager.LoadScene("Level2");
	}

	public void Level3BtnClick()
	{
		SceneManager.LoadScene("Level3");
	}



}
