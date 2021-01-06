
/**This class is to control the movements of the bullet**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	[SerializeField]
	int damage = 5;

	void Start () {
		StartCoroutine ("Finish");
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag.Equals("Saw"))
		{
			Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>());
		}
		else if (other.gameObject.tag.Equals("SawBullet"))
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
		else {
			Debug.Log("Collision bullet with the Saw :(\n");
			IDamageable d = other.gameObject.GetComponent<IDamageable>();
			if (d != null)
			{
				d.Damage(damage);
			}
			Destroy(gameObject);
		}
   }

	private IEnumerator Finish(){
		yield return new WaitForSeconds (5);
		Destroy (gameObject);
	}
}
   
public interface IDamageable{
	void Damage (int damage);
}
