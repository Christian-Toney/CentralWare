using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterHackerMinigame : Minigame
{

  protected new void Start()
  {

    base.Start();
    StartCoroutine(StartMinigame());

  }

  protected override IEnumerator StartMinigame()
  {
    
    yield return new WaitForSeconds(5 / player.speed);
    progressReport.MinigameEnded(this);
    
  }

}
