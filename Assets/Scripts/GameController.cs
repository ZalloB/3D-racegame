using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject pauseMenu;
    public float actualTime = 0;
    public float lastTime = 0;
    public float actualLap = 0;
    public float maxLap = 3;
    public float firstLapTime = 0;
    public float secondLapTime = 0;
    public float threeLapTime = 0;
    public bool isStarted;
    public Text textTime;
    public Text textLap;
    public Text countdown;
    public Text TimeLaps;
    public GameObject car;

    public AudioClip[] startClips;
    public AudioClip pause;

    public int maxCheckpoints = 5;
    private int generalCountChecks = 0;
    public int[] checkpointsNumbers;

    public GameObject winingPanel;

    public int goldTime = 10;
    public Sprite goldMedal;
    public int silverTime = 14;
    public Sprite silverMedal;
    public int bronzeTime = 16;
    public Sprite bronzeMedal;

    public Image medalImage;
    public Text timeWinText;

    void Start()
    {
        checkpointsNumbers = new int[maxCheckpoints];

        isStarted = false;
        StartCoroutine("StartGame");
    }

    void Update()
    {
        CheckPauseGame();
        if (isStarted)
            UpdateTimeUI();
    }

    IEnumerator StartGame()
    {
        int count = 4;
        foreach (AudioClip clip in startClips)
        {
            if (count == 4)
            {
                countdown.text = "READY";
            }
            else if (count == 0)
            {
                countdown.text = "GO";
            }
            else
            {
                countdown.text = count.ToString();
            }

            SoundManager.instance.RandomizeSfx(clip);
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdown.text = "";
        car.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = true;
        
        isStarted = true;
        GameObject.Find("GameController").GetComponent<GhostController>().isStartGame = true;
    }

    void CheckWin()
    {
        bool isWin = true;
        for (int i = 1; i <= maxCheckpoints; i++)
        {
            if (checkpointsNumbers[i - 1] != i)
            {
                isWin = !isWin;
                break;
            }
        }
        
        if (isWin)
        {
            TimeLaps.text = "";

            actualLap = actualLap + 1f;
            if (actualLap == maxLap)
            {
                Time.timeScale = 0;
                threeLapTime = actualTime;
                winingPanel.SetActive(true);
                //countdown.text = "Finish: ";

                for (int i = 0; i < maxLap; i++)
                {
                    if (i == 0 && i < actualLap)
                        timeWinText.text = timeWinText.text + "Lap " + (i + 1) + ": " + string.Format("{0:D2}:{1:D2}  \n ", TimeSpan.FromSeconds(firstLapTime).Minutes, TimeSpan.FromSeconds(firstLapTime).Seconds) + "\n";
                    if (i == 1 && i < actualLap)
                        timeWinText.text = timeWinText.text + "Lap " + (i + 1) + ": " + string.Format("{0:D2}:{1:D2}  \n ", TimeSpan.FromSeconds(secondLapTime).Minutes, TimeSpan.FromSeconds(secondLapTime).Seconds) + "\n";
                    if (i == 2 && i < actualLap)
                        timeWinText.text = timeWinText.text + "Lap " + (i + 1) + ": " + string.Format("{0:D2}:{1:D2}  \n ", TimeSpan.FromSeconds(threeLapTime).Minutes, TimeSpan.FromSeconds(threeLapTime).Seconds) + "\n";
                }

                //todo car sounds stop
            }
            else
            {
                //implica una vuelta
                if (lastTime == 0)
                {
                    lastTime = actualTime;
                }
                GameObject.Find("GameController").GetComponent<GhostController>().isNewLap = true;
                lastTime = actualTime;
                if (actualLap == 1)
                    firstLapTime = actualTime;
                else if (actualLap == 2)
                    secondLapTime = actualTime;
                if (actualLap == 3)
                    threeLapTime = actualTime;

                
                for (int i = 0; i < maxLap; i++)
                {
                    if (i == 0 && i < actualLap)
                        TimeLaps.text = TimeLaps.text + "Lap " + (i + 1) + ": " + string.Format("{0:D2}:{1:D2}  \n ", TimeSpan.FromSeconds(firstLapTime).Minutes, TimeSpan.FromSeconds(firstLapTime).Seconds) +"\n";
                    if (i == 1 && i < actualLap)
                        TimeLaps.text = TimeLaps.text + "Lap " + (i + 1) + ": " + string.Format("{0:D2}:{1:D2}  \n ", TimeSpan.FromSeconds(secondLapTime).Minutes, TimeSpan.FromSeconds(secondLapTime).Seconds) + "\n";
                    if (i == 2 && i < actualLap)
                        TimeLaps.text = TimeLaps.text + "Lap " + (i + 1) + ": " + string.Format("{0:D2}:{1:D2}  \n ", TimeSpan.FromSeconds(threeLapTime).Minutes, TimeSpan.FromSeconds(threeLapTime).Seconds) + "\n";
                }
                textLap.text = "LAP: "+ (actualLap+1) +"/3";
               
                actualTime = 0;
            }

            


        }

        checkpointsNumbers = new int[maxCheckpoints];
        generalCountChecks = 0;

    }

    void AddCheckpoint(int number)
    {
        //Debug.Log("Add checkpoint " + number + " at point " + generalCountChecks);
        checkpointsNumbers[generalCountChecks] = number;
        generalCountChecks++;
    }

    void CheckPauseGame()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !pauseMenu.activeSelf)
        {

            SoundManager.instance.RandomizeSfx(pause);
            SoundManager.instance.musicSource.Pause();
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void UpdateTimeUI()
    {
        actualTime += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(actualTime);
        if (timeSpan.Hours > 0)
            textTime.text = string.Format("TIME: {0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        else
            textTime.text = string.Format("TIME: {0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
