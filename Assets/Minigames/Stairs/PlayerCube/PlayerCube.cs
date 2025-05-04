using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : MonoBehaviour {

  private bool canJump = false;
  private bool canPound = false;
  public Camera playerCamera;
  public StairsMinigame minigame;

  void Update() {

    Rigidbody rigidbody = GetComponent<Rigidbody>();
    float jumpForce = 300f * minigame.player.speed;
    float moveSpeed = 20f * minigame.player.speed;

    if (Input.GetButton("Up") && canJump) {

      canJump = false;
      rigidbody.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);

    } else if (Input.GetButton("Down") && canPound) {

      canPound = false;
      rigidbody.AddForce(jumpForce * Vector3.down, ForceMode.Impulse);

    } else if (Input.GetButton("Left")) {

      transform.position += moveSpeed * Time.deltaTime * new Vector3(-1f, 0f, 0f);

    } else if (Input.GetButton("Right")) {

      transform.position += moveSpeed * Time.deltaTime * new Vector3(1f, 0f, 0f);

    }

    playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, -10f);

  }

  void OnTriggerEnter(Collider other) {
    
    canPound = false;
    canJump = true;

  }

  void OnTriggerExit(Collider other) {
    
    canPound = true;
    canJump = false;

  }

}
