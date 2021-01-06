using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag.Equals("Bullet"))
		{
			Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>());
		}

	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
