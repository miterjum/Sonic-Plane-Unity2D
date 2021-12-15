using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	private bool enemySpawn;			
	private Weapon[] theweapons;		
	public float speed;					
	private float posMin = -1.719578f;	
	private float posMax = 1.653888f;	
	public bool moveRight;				
	

	void Start()
	{	
		theweapons = GetComponentsInChildren<Weapon> ();
		foreach (Weapon weapon in theweapons) {
			weapon.enabled = false;
		}
		speed = Random.Range(speed, speed*2);
		float initialMove = Random.Range(1, 10);
		if (initialMove <= 5) {
			moveRight = true;		
		} 
		else if (initialMove > 5) {
			moveRight = false;	
		}
		enemySpawn = false;
		Vector2 pos = transform.position;
		pos.x = Random.Range (posMin, posMax);
		transform.position = pos;
		GetComponent<Collider2D>().isTrigger = true;
	}


	void Update()
	{
	}


	void FixedUpdate()
	{
		if (enemySpawn == true) {
			Spawn();	
		}
	}

	private void Spawn()
	{
		if (moveRight) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		} 
		if(!moveRight)
		{
			transform.Translate (Vector2.right * -speed * Time.deltaTime);;
		}

	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "BorderLeft") {
			moveRight = true;
		}
		if (coll.gameObject.tag == "BorderRight") {
			moveRight = false;
		}
		if (coll.gameObject.tag == "Player Laser")
        {
			GameManager.Instance.score.AddPoint();
		}	
	}
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "BorderTop") {
			transform.Translate(Vector2.up * -8 *Time.deltaTime);
			enemySpawn = true;
			GetComponent<Collider2D>().isTrigger = false;
			foreach (Weapon weapon in theweapons) {
				weapon.enabled = true;		
			}
		}
	}
}
