using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float moveSpeed = 500f;

    Rigidbody2D rb;

    GameObject playerRobot;
    Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRobot = GameObject.FindGameObjectWithTag("Player");
        moveDirection = (playerRobot.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 15f);  
    }

    // Update is called once per frame
    //void OnTriggerEnter2D(Collider2D col) {
    //    Debug.Log("Hit");
    //    Destroy(gameObject);
    //}
}
