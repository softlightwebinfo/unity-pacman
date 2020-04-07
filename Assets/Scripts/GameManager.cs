using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    public bool gameStarted = false;
    public bool gamePaused = false;
    public AudioClip audioPause;
    public float invincibleTime = 0.0f;

    private void Awake()
    {
        if (!sharedInstance) sharedInstance = this;
        StartCoroutine("StartGame");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gamePaused = !this.gamePaused;
            if (this.gamePaused)
            {
                this.PlayPauseMusic();
            }
            else
            {
                this.StopPauseMusic();
            }
        }

        if (this.invincibleTime > 0)
        {
            this.invincibleTime -= Time.deltaTime;
        }
    }

    void PlayPauseMusic()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = this.audioPause;
        audio.loop = true;
        audio.Play();
    }

    void StopPauseMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

    public void MakeInvincibleFor(float numberOfSeconds)
    {
        this.invincibleTime += numberOfSeconds;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(4.0f);
        this.gameStarted = true;
    }
}
