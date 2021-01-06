using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AntiRobotContoller : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField]
	private GameObject bullet;  //declaring a variable of bullet object

	[SerializeField]
	private float bulletForce = 500f;  //force of the bullet 
	float nextFire;
	float fireRate;

	private int enemyHealthpoints = 15;

	private Rigidbody2D bulletRigidBody;
	private Transform bulletTransform;

	void Start()
	{
		fireRate = 1.2f;
		nextFire = Time.time;  //call the animation of the bullet
		bulletTransform = gameObject.transform;          //get the position of the bullet on the screen
	}

	void Update()
	{
		if (Time.time > nextFire)
		{
			ShootBullet();
			nextFire = Time.time + fireRate;
		}
	}

	public void ShootBullet()
	{
		GameObject b = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
		bulletRigidBody = b.GetComponent<Rigidbody2D>();
		Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), b.GetComponent<Collider2D>());
		//bulletRigidBody.AddForce(bulletTransform.right * bulletForce * bulletTransform.localScale.x);

	}

	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(1.2f);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag.Equals("Bullet"))
		{
			if (enemyHealthpoints == 0) {
				Destroy(gameObject);
			}
			else
				enemyHealthpoints -= 1;
			Debug.Log("Player Shooting  :)\n");
		}
	}
}
