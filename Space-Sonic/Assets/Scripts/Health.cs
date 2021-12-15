using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int hp;
	public bool isEnemy = true;			
	private GameObject gui;	
	
	
	void Start()
	{

	}
	
	void OnCollisionEnter2D( Collision2D coll)
	{
		ShootScript shot = coll.gameObject.GetComponent<ShootScript> ();
		if (shot != null) {
			if (isEnemy) {
				hp -= shot.dammage;
			}
			else
			{
			   hp -= shot.dammage;
				if (hp > 0)
				{
					for (int i = hp; i < 5; i++)
					{
						GameManager.Instance.textureHealth[i].SetActive(false);
					}
				}
				else
                {
					foreach(GameObject i in GameManager.Instance.textureHealth)
                    {
						i.SetActive(false);
                    }
                }
		    }
			
		}
	}
	
	void Dead()
	{
		hp = 0;
		gui.SendMessage("Dead");
	}
	
	public void AddHp()
	{
		hp = 5;
		foreach(GameObject i in GameManager.Instance.textureHealth)
        {
			i.SetActive(true);
        }
	}
	
	void Update()
	{
		if(hp <= 0)
		{
			if(!isEnemy)
			{
				ActionHelper.instanceAction.GameOver();
			}
			FxHelper.instanceFX.CrashFX(transform.position);
			SoundHelper.instanceSound.DeadSound();
			Destroy(gameObject);
		}
	}
}
