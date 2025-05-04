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

    music.pitch = player.speed;
    Physics.gravity = new Vector3(0, -50f, 0) * player.speed;
    yield return new WaitForSeconds(10 / player.speed);
    progressReport.MinigameEnded(this);

  }

}
