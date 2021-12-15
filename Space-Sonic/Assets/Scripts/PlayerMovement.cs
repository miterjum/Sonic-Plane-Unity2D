using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour {
	public float speedMove;
	public float bonusTime;

	private bool toLeft = false;
	private bool toRight = false;
	
	public GameObject shield;
	public TMP_Text bonustimeText;

	private bool counting = false;
	private float counter;

	public Weapon[] addWeapons;

	public ShootScript[] shoot;
	public Sprite strongShip;
	public Sprite normalSprite;
	public Sprite shieldSprite;

	private SpriteRenderer sRender;

	void Start () {

		counter = bonusTime;
		sRender = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (counting) {
			counter -= Time.deltaTime;
            bonustimeText.text = counter.ToString("#0.0");
        }
	}


	void FixedUpdate()
	{
		if (toLeft) {
			MoveLeft();
		}

		if (toRight) {	
			MoveRight();
		}
	}
	public void Left(bool a)
    {
		toLeft = a;
    }
	public void Right(bool a)
    {
		toRight = a;
    }
	public void MoveLeft()
	{
		transform.Translate(Vector2.right * -speedMove* Time.deltaTime);
	}


	public void MoveRight()
	{
		transform.Translate(Vector2.right * speedMove * Time.deltaTime);
	}


	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "StrongMode") {
			Destroy (coll.gameObject);
			counting = true;
			StrongMode();
			Invoke ("Downgrade", bonusTime);
		}


		if (coll.gameObject.tag == "ShieldMode") {
			Destroy (coll.gameObject);
			counting = true;
			ShieldMode();
			Invoke("Downgrade", bonusTime);
		}

		if (coll.gameObject.tag == "Life") {
			SoundHelper.instanceSound.PickUpSound();
			Destroy(coll.gameObject);
			GameManager.Instance.playerHealth.AddHp();
		}

		if (coll.gameObject.tag == "Enemy") {

		}
	}

	void Downgrade()
	{
		SoundHelper.instanceSound.BonusDownSound ();
		counting = false;
        bonustimeText.text = "";
        counter = bonusTime;

		sRender.sprite = normalSprite;
		foreach (Weapon addWeapon in addWeapons) {
			addWeapon.gameObject.SetActive(false);
		}
		shield.SetActive (false);
		shoot[0].dammage = 1;
		shoot[1].dammage = 2;
	}


	void StrongMode()
	{
		SoundHelper.instanceSound.BonusUpSound ();
		sRender.sprite = strongShip;
		foreach (Weapon addWeapon in addWeapons) {
			addWeapon.gameObject.SetActive(true);
		}
	}


	void ShieldMode()
	{
		shoot[0].dammage = 0;
		shoot[1].dammage = 0;
		SoundHelper.instanceSound.BonusUpSound ();
		sRender.sprite = shieldSprite;
		shield.SetActive (true);
	}
}
