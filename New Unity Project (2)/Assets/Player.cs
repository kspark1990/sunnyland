using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Rigidbody2D playerRigidbody;
	
	public float moveSpeed = 3f;
	public float jumpPower = 3f;
	//
	//public float jumpGauge = 0;
	bool isJump = false;
	bool isOnGround = true;


	void Start () {
		playerRigidbody = GetComponent<Rigidbody2D>();
	}

	private void move()
	{
		Vector3 moveVelocity = Vector3.zero;

		if(Input.GetAxisRaw("Horizontal") < 0)
		{
			moveVelocity = Vector3.left;
		}
		else if (Input.GetAxisRaw("Horizontal") > 0)
		{
			moveVelocity = Vector3.right;
		}

		transform.position += moveVelocity * moveSpeed * Time.deltaTime;

	}

	void jump()
	{
		if (!isJump)
			return;

		playerRigidbody.velocity = Vector2.zero;

		Vector2 jumpVelocity = new Vector2(0, jumpPower);
		playerRigidbody.AddForce(jumpVelocity, ForceMode2D.Impulse);
		isJump = false;
		isOnGround = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//그라운드 외에 다른 장애물에 대한 판정 추가 필요
		if (collision.tag == "ground")
		{
			isOnGround = true;

		}

		//적 밟아 죽이는 판정 추가
		if(collision.tag == "enemy")
		{
			float killVelocity = jumpPower / 2;

			//jump more
			playerRigidbody.velocity = Vector2.zero;
			if(Input.GetKey(KeyCode.Space))
			{
				killVelocity = jumpPower * 1.2f;
			}
			else
			{
				killVelocity = jumpPower / 2;
			}

			playerRigidbody.AddForce(new Vector2(0,killVelocity), ForceMode2D.Impulse);


			/* TODO: jumpgauge implement
			 
			if (Input.GetKey(KeyCode.Space))
			{
				jump
				Debug.Log();

			}*/


			//jump timing!
			//enemy dead situation111
		}
	}

	private void FixedUpdate()
	{
		move();
		jump();

	}

	


	void Update () {



		if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
		{
			isJump = true;
		}
		
	}
}
