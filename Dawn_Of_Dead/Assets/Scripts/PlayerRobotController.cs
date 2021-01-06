/**This class is to control the movements of the player(Robot)**/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRobotController : MonoBehaviour, IDamageable {

	[SerializeField]
	private float playerSpeed = 500f;
	[SerializeField]
	private float playerJumpMultiplier = 100f;

	[SerializeField]
	private float playerStartX = -2300f;
	[SerializeField]
	private float playerEndX = 11000f;

	private float gravityScale = 200f;
	private float playerMass = 1f;

	private Rigidbody2D playerRigidBody = null;
	private Animator playerAnimator = null;
	private Vector2 playerCurrentPosition;
	private Transform playerTransform;
	private GameObject[] saws;

	private int playerHealth = 100;
	public bool deadFlag = false;
	public static int audioOn = 0;


	void Start () {
		playerAnimator = gameObject.GetComponent<Animator> ();
		playerRigidBody = gameObject.GetComponent<Rigidbody2D> ();
		playerTransform = gameObject.GetComponent<Transform> ();
		playerCurrentPosition = playerTransform.position;
		saws = GameObject.FindGameObjectsWithTag("SawTop");
		Debug.Log("Hii " + audioOn + " this is audioOn Value");

		Component[] AudSrcs;

		AudSrcs = gameObject.GetComponents(typeof(AudioSource));


		if (audioOn != 0)
		{
			((AudioSource)gameObject.GetComponents(typeof(AudioSource))[0]).Stop();
			((AudioSource)gameObject.GetComponents(typeof(AudioSource))[1]).Stop();
		}
		if (audioOn == 0)
		{
			((AudioSource)gameObject.GetComponents(typeof(AudioSource))[0]).Play();
			((AudioSource)gameObject.GetComponents(typeof(AudioSource))[1]).Play();
		}


		Debug.Log("Hii " + AudSrcs.Length + " this is audioOn Value");
}

	void FixedUpdate () {

		if (!deadFlag)   //if player is not dead then let him do other functions otherwise not
		{
			playerCurrentPosition = playerTransform.position;
			//To make player run
			float moveHorizontal = Input.GetAxis ("Horizontal");
			playerRigidBody.velocity = new Vector2 (moveHorizontal * playerSpeed, playerRigidBody.velocity.y);


			//To make player jump press space
			float playerJump = Input.GetAxis ("Jump");
			if (playerJump > 0 && IsPlayerGrounded ()) {
				playerRigidBody.AddForce (Vector2.up * playerJumpMultiplier);
			}

			playerAnimator.SetInteger ("velocity", (int)(Mathf.Abs (playerRigidBody.velocity.x * 100)));


			//To make player turn left and right
			if (playerRigidBody.velocity.x > 0) {
				gameObject.transform.localScale = new Vector3 (1, 1, 1);
			} else if (playerRigidBody.velocity.x < 0) {
				gameObject.transform.localScale = new Vector3 (-1, 1, 1);
			} 

			playerAnimator.SetBool ("jump", !IsPlayerGrounded ());

			//To make player shoot:
			//in PlayerBuletShoot Script



		}


		CheckPlayerBoundaries ();
		playerTransform.position = playerCurrentPosition;

	}

	void Update(){
			//Slide trigger
		if (Input.GetKeyDown (KeyCode.S)){
			//playerRigidBody.gravityScale = 0f;
			//playerRigidBody.mass = 0f;
			
			foreach (GameObject saw in saws)
			{
				saw.GetComponent<Collider2D>().enabled = false;
			}
			//gameObject.GetComponent<Collider2D>().enabled = false;
			playerAnimator.SetTrigger ("slideTrigger");
			playerCurrentPosition = playerTransform.position;
			//To make player move
			float moveHorizontal = Input.GetAxis("Horizontal");
			playerRigidBody.velocity = new Vector2(moveHorizontal * playerSpeed, playerRigidBody.velocity.y);
			StartCoroutine(waitForSec(2.0f));

		}

	}

	private bool IsPlayerGrounded(){
		SpriteRenderer playerSR = gameObject.GetComponent<SpriteRenderer> ();
		Vector2 playerPos = gameObject.transform.position;

		RaycastHit2D res = Physics2D.Linecast (new Vector2 (playerPos.x, playerPos.y - (playerSR.bounds.size.y / 2)),
			                                   new Vector2 (playerPos.x, playerPos.y - (playerSR.bounds.size.y / 2 + 0.2f)));

		Debug.DrawLine (new Vector2 (playerPos.x, playerPos.y - (playerSR.bounds.size.y / 2)),
			            new Vector2 (playerPos.x, playerPos.y - (playerSR.bounds.size.y / 2 + 0.2f)));

		return res != null && res.collider != null;
	}

	public void Damage(int damage){
		playerHealth -= damage;
		if (playerHealth <= 0) {
			deadFlag = true;
			playerAnimator.SetBool ("dead", deadFlag);
		}
	}

	private void CheckPlayerBoundaries(){        //check if player is going out of boundaries
		if (playerCurrentPosition.x > playerEndX) {     
			playerCurrentPosition.x = playerEndX;
		}

		else if (playerCurrentPosition.x < playerStartX) {     
			playerCurrentPosition.x = playerStartX;
		}
	}

	private IEnumerator waitForSec(float sec)
	{
		yield return new WaitForSeconds(sec);
		foreach (GameObject saw in saws)
		{
			saw.GetComponent<Collider2D>().enabled = true;
		}
		//gameObject.GetComponent<Collider2D>().enabled = true;
		//playerRigidBody.gravityScale = gravityScale;
		//playerRigidBody.mass = playerMass;
	}
}
