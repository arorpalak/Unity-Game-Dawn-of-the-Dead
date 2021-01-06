
/**This class is to control what happens when player collides with coins and zombies**/
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour {

	[SerializeField]
	CanvasController canvasController;

	[SerializeField]
	ZombieController zombieController;

	[SerializeField]
	TimerController timerController;

	private AudioSource coinScoreSound;
	public static int modeEasy = 0;


	void Start () {
		coinScoreSound = gameObject.GetComponent<AudioSource> ();
	}
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals ("Coin")) 
		{
			Debug.Log ("Collision detected with the coin :)\n");
			if (coinScoreSound != null) {
				coinScoreSound.Play ();}
			Destroy (other.gameObject);
			canvasController.Score += 10;
		}

		if(other.gameObject.tag.Equals ("LevelEndCollider"))
		{
			Debug.Log ("Congratulations Level 1 Completed:)\n");
			canvasController.LevelOneCompleted ();
			timerController.FinishTime ();
			StartCoroutine ("LoadLevel2");

		}

		if(other.gameObject.tag.Equals ("Level2EndCollider"))
		{
			Debug.Log ("Congratulations Level 2 Completed:)\n");
			canvasController.LevelTwoCompleted();
			Debug.Log("Test1:)\n");
			//timerController.FinishTime ();
			Debug.Log("Tet2)\n");
			StartCoroutine("LoadLevel3");
		}

		if (other.gameObject.tag.Equals("Level3EndCollider"))
		{
			Debug.Log("Congratulations Level 3 Completed:)\n");
			canvasController.LevelThreeCompleted();
			//timerController.FinishTime();
			StartCoroutine("GameOver");
		}


	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag.Equals ("Enemy")) {
			Debug.Log ("Collision detected with the enemy :(\n");
			if (zombieController.zombieCollideSound != null) {
				zombieController.zombieCollideSound.Play ();}
			if (modeEasy == 0) { canvasController.RobotHealth -= 10; }
			else
				canvasController.RobotHealth -= 40;

			StartCoroutine("Blink");
		}

		else if (other.gameObject.tag.Equals("Saw"))
		{
			Debug.Log("Collision detected with the Saw :(\n");
			canvasController.RobotHealth -= 20;
			StartCoroutine("Blink");
		}
		else if (other.gameObject.tag.Equals("SawBullet"))
		{
			Debug.Log("Collision detected with the SawBullet :(\n");
			canvasController.RobotHealth -= 20;
			Destroy(other.gameObject);
			StartCoroutine("Blink");
		}
		else if (other.gameObject.tag.Equals("SawTop"))
		{
			Debug.Log("Collision detected with the Saw :(\n");
			canvasController.RobotHealth -= 20;
			StartCoroutine("Blink");
		}

		else if (other.gameObject.tag.Equals ("Bullet")) {
			Debug.Log ("Player Shooting  :)\n");
		}

		else if(other.gameObject.tag.Equals("KillZone")){
			Debug.Log ("Player fell :(10% health lost!!\n");
			canvasController.RobotHealth -= 10;
			//timerController.Start();
		}
	}

	private IEnumerator Blink()
	{
		Color robotColor;
		Renderer rend = gameObject.GetComponent<Renderer> ();
		for (int blinkTime = 0; blinkTime < 2; blinkTime++) 
		{
			for (float f = 1f; f >= 0; f -= 0.1f) {
				robotColor = rend.material.color;
				robotColor.a = f;
				rend.material.color = robotColor;
				yield return new WaitForSeconds (0.01f);
			}
			for (float f = 0f; f <= 1; f += 0.1f) {
				robotColor = rend.material.color;
				robotColor.a = f;
				rend.material.color = robotColor;
				yield return new WaitForSeconds (0.01f);
			}
		}
	}

	IEnumerator LoadLevel2()
	{
		yield return new WaitForSeconds(3.0f);
		SceneManager.LoadScene ("Level2");
	}

	IEnumerator LoadLevel3()
	{
		yield return new WaitForSeconds(3.0f);
		SceneManager.LoadScene("Level3");
	}

	IEnumerator GameOver()
	{
		yield return new WaitForSeconds(3.0f);
		SceneManager.LoadScene("GameComplete");
	}
}


