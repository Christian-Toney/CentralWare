using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorRushMinigame : Minigame
{
  public GameObject[] dependantObjects;

  protected new void Start()
  {
    
    base.Start();
    StartCoroutine(StartMinigame());

  }

  protected override IEnumerator StartMinigame()
  {

    GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().pitch = player.speed;

    endTime = startTime.AddSeconds(5 / player.speed);
    GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().SetStartTimeMilliseconds(new DateTimeOffset(startTime).ToUnixTimeMilliseconds());
    GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().SetEndTimeMilliseconds(new DateTimeOffset(endTime).ToUnixTimeMilliseconds());

    for (int i = 0; i < dependantObjects.Length; i++)
    {
      dependantObjects[i].SetActive(true);
    }
    yield return new WaitForSeconds(6 / player.speed);
    progressReport.MinigameEnded(this);
    
  }

  public DateTime GetStartTime()
  {
    return startTime;
  }

  public DateTime GetEndTime()
  {
    return endTime;
  }

}
