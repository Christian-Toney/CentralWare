using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsMinigame : Minigame
{

  protected new void Start()
  {

    base.Start();
    StartCoroutine(StartMinigame());

  }

  protected override IEnumerator StartMinigame()
  {
    
    Physics.gravity = new Vector3(0, -50f, 0) * player.speed;
    yield return new WaitForSeconds(10 / player.speed);
    progressReport.MinigameEnded(this);
    
  }

}
