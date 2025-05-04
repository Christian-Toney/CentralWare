using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
  public MissionStreetMinigame minigame;

  // Start is called before the first frame update
  void Start() {

    Animator animator = GetComponent<Animator>();
    Debug.Log(minigame.player);
    animator.SetFloat("SpeedMultiplier", 1 + ((minigame.player.speed - 1) * 0.25f));

  }
}
