using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsMinigame : Minigame {

  public AudioSource music;

  protected new void Start() {

    base.Start();
    StartCoroutine(StartMinigame());

  }

  protected override IEnumerator StartMinigame() {

    float duration = 10f / player.speed;
    endTime = startTime.AddSeconds(duration);
    GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().SetStartTimeMilliseconds(new DateTimeOffset(startTime).ToUnixTimeMilliseconds());
    GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().SetEndTimeMilliseconds(new DateTimeOffset(endTime).ToUnixTimeMilliseconds());

    music.pitch = player.speed;
    Physics.gravity = new Vector3(0, -50f, 0) * player.speed;
    yield return new WaitForSeconds(duration);
    progressReport.MinigameEnded(this);

  }

}
