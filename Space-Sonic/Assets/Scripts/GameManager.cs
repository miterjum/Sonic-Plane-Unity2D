using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Score score;
    public Health playerHealth;
    public GameObject[] textureHealth;
    public TMP_Text sound;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1;
        for(int i = 0; i < playerHealth.hp; i++)
        {
            textureHealth[i].SetActive(true);
        }
        score = GetComponent<Score>();
    }
    public void Setting()
    {
        if (AudioListener.volume == 1)
            sound.text = "Sound On";
        else if (AudioListener.volume == 0)
            sound.text = "Sound Off";
        if (AudioListener.volume == 0)
            AudioListener.volume = 1;
        else if (AudioListener.volume == 1)
            AudioListener.volume = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void exit()
    {
        Time.timeScale = 1;
        Application.LoadLevel("Menu");
    }
}
