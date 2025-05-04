using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagonist : MonoBehaviour
{

  private string buttonName;
  private float offsetPositionX = 0;
  public int goalPositionY = -116;
  public int goalWidth = 600;
  public int goalHeight = 600;

  void Start()
  {

    StartCoroutine(MovePlayer());

  }

  // Update is called once per frame
  void Update()
  {

    Crowd crowd = GameObject.FindGameObjectWithTag("Crowd").GetComponent<Crowd>();

    if (Input.GetButton("Left") && buttonName != "Left")
    {
      
      // Set the button name to "Left" to indicate that the left button was pressed.
      // This prevents the player from moving left again until the button is released.
      buttonName = "Left";

      // Move the player by 200 pixels to the left.
      offsetPositionX = Math.Max(offsetPositionX - 200, -200);
      crowd.SetOffsetPositionX(offsetPositionX);

      // Play a sound effect when the button is pressed just for fun. :)
      AudioSource squeakSound = GetComponent<AudioSource>();
      squeakSound.Play();

    }
    else if (Input.GetButton("Right") && buttonName != "Right")
    {

      // Set the button name to "Right" to indicate that the right button was pressed.
      // This prevents the player from moving right again until the button is released.
      buttonName = "Right";

      // Tween the player by 200 pixels to the right.
      offsetPositionX = Math.Min(offsetPositionX + 200, 200);
      crowd.SetOffsetPositionX(offsetPositionX);

      // Play a sound effect when the button is pressed just for fun. :)
      AudioSource squeakSound = GetComponent<AudioSource>();
      squeakSound.Play();

    }
    else if (!Input.GetButton("Left") && !Input.GetButton("Right"))
    {

      buttonName = null;

    }

  }

  IEnumerator MovePlayer()
  {

    RectTransform rectTransform = GetComponent<RectTransform>();
    Vector3 initialPosition = rectTransform.anchoredPosition;
    Vector3 initialSizeDelta = rectTransform.sizeDelta;
    ElevatorRushMinigame elevatorRushMinigame = GameObject.FindGameObjectWithTag("Minigame").GetComponent<ElevatorRushMinigame>();

    while (DateTimeOffset.Now.ToUnixTimeMilliseconds() < new DateTimeOffset(elevatorRushMinigame.GetEndTime()).ToUnixTimeMilliseconds())
    {

      Vector2 goalPosition = new(initialPosition.x + offsetPositionX, goalPositionY);
      float timeRemaining = new DateTimeOffset(elevatorRushMinigame.GetEndTime()).ToUnixTimeMilliseconds() - DateTimeOffset.Now.ToUnixTimeMilliseconds();
      float totalTime = new DateTimeOffset(elevatorRushMinigame.GetEndTime()).ToUnixTimeMilliseconds() - new DateTimeOffset(elevatorRushMinigame.GetStartTime()).ToUnixTimeMilliseconds();
      float percentage = 1 - (timeRemaining / totalTime);

      rectTransform.anchoredPosition = Vector2.Lerp(initialPosition, goalPosition, percentage);
      
      Vector2 goalSizeDelta = new(goalWidth, goalHeight);
      rectTransform.sizeDelta = Vector2.Lerp(initialSizeDelta, goalSizeDelta, percentage);

      yield return null;

    }

    // Ensure the final position and size are set after the loop ends.
    rectTransform.anchoredPosition = new(initialPosition.x + offsetPositionX, goalPositionY);
    rectTransform.sizeDelta = new Vector2(goalWidth, goalHeight);

  }

}
