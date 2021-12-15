using UnityEngine;
using System.Collections;
// Action helper digunakan sebagai helper ketika player game over & finish level
public class ActionHelper : MonoBehaviour {

	private CameraFollow camFo;			
	private GameObject[] lasers;		
	private GameObject[] enemies;		
	public GameObject gameCompleteGui;	
	public GameObject gameoverGui;		
	private GameObject guiObj;			
	public string nextLevel;			
	public static ActionHelper instanceAction; 

	// Setiap objek Awake.
	void Awake()
	{
		instanceAction = this;
	}

	void Start()
	{
		camFo = GameObject.Find("Main Camera").GetComponent<CameraFollow> ();
		guiObj = GameObject.Find("GUI");
	}

	public void FinishLevel()
	{
		SoundHelper.instanceSound.FinishSound();
		lasers = GameObject.FindGameObjectsWithTag ("Player Laser");
		foreach (GameObject laser in lasers) {
			laser.GetComponent<Collider2D>().isTrigger = true;		
		}
		camFo.enabled = false;
		gameCompleteGui.SetActive (true);
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			enemy.SetActive(false);		
		}
		Score scoreScript = GameManager.Instance.GetComponent<Score> ();
		PlayerPrefs.SetInt ("Score", scoreScript.score);
		PlayerPrefs.SetString("Level", nextLevel);
	}
	
	public void GameOver()
	{
		PlayerPrefs.SetInt("Score", 0);
		lasers = GameObject.FindGameObjectsWithTag ("Enemy Laser");
		foreach (GameObject laser in lasers) {
			laser.GetComponent<Collider2D>().isTrigger = true;		
		}
		camFo.enabled = false;
		gameoverGui.SetActive (true);
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			enemy.SetActive(false);		
		}
	}

}
