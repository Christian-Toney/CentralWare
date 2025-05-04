using UnityEngine;

public class Stairs : MonoBehaviour {

  public StairsMinigame minigame;

  void OnCollisionEnter(Collision collision) {
    
    if (!minigame.isComplete) {

      minigame.isComplete = true;

      AudioSource cheerSound = GetComponent<AudioSource>();
      cheerSound.Play();

    }

  }

}
