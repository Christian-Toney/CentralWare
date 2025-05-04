using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStreetMinigame : Minigame {

  public AudioSource music;
  public GameObject[] dependantObjects;

  protected new void Start() {

    base.Start();
    StartCoroutine(StartMinigame());

  }

  protected override IEnumerator StartMinigame() {

    float duration = 5f / player.speed;
    endTime = startTime.AddSeconds(duration);
    GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().SetStartTimeMilliseconds(new DateTimeOffset(startTime).ToUnixTimeMilliseconds());
    GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().SetEndTimeMilliseconds(new DateTimeOffset(endTime).ToUnixTimeMilliseconds());
    music.pitch = player.speed;

    for (int i = 0; i < dependantObjects.Length; i++)
    {
      dependantObjects[i].SetActive(true);
    }

    yield return new WaitForSeconds(duration);
    progressReport.MinigameEnded(this);

  }

}
