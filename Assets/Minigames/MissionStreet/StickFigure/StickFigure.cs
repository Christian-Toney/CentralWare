using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickFigure : MonoBehaviour {

  private bool isMoving = false;
  private bool gotHit = false;
  public Collider goalCollider;
  public MissionStreetMinigame minigame;

  void Update() {

    if (Input.GetButton("Up") && !isMoving) {

      isMoving = true;

    }

    if (isMoving && !gotHit) {

      transform.position -= new Vector3(0f, 0f, 40f * Time.deltaTime * minigame.player.speed);

    }

  }

  void OnTriggerEnter(Collider collider) {

    if (collider == goalCollider) {

      minigame.isComplete = true;

    } else if (!gotHit) {

      gotHit = true;

      Rigidbody rigidbody = GetComponent<Rigidbody>();
      rigidbody.AddExplosionForce(100f, transform.position, 5f, 1f, ForceMode.Impulse);

      AudioSource hitSound = GetComponent<AudioSource>();
      hitSound.Play();

    }

  }

}
