using System;
using System.Collections;
using UnityEngine;

public class Crowd : MonoBehaviour
{

  public int lifeCount = 5;
  private float offsetPositionX = 0;

  public int goalPositionY = -104;
  public int goalWidth = 1029;

  private void Start()
  {

    StartCoroutine(MoveCrowd());

  }

  public void SetOffsetPositionX(float offset)
  {

    if (lifeCount <= 0) return;

    offsetPositionX = offset;

    lifeCount -= 1;

  }

  private IEnumerator MoveCrowd()
  {

    RectTransform rectTransform = GetComponent<RectTransform>();
    Vector3 initialPosition = rectTransform.anchoredPosition;
    Vector3 initialSizeDelta = rectTransform.sizeDelta;
    ElevatorRushMinigame elevatorRushMinigame = GameObject.FindGameObjectWithTag("Minigame").GetComponent<ElevatorRushMinigame>();

    while (lifeCount > 0 && DateTimeOffset.Now.ToUnixTimeMilliseconds() < new DateTimeOffset(elevatorRushMinigame.GetEndTime()).ToUnixTimeMilliseconds())
    {

      Vector2 goalPosition = new(initialPosition.x + offsetPositionX, goalPositionY);
      float timeRemaining = new DateTimeOffset(elevatorRushMinigame.GetEndTime()).ToUnixTimeMilliseconds() - DateTimeOffset.Now.ToUnixTimeMilliseconds();
      float totalTime = new DateTimeOffset(elevatorRushMinigame.GetEndTime()).ToUnixTimeMilliseconds() - new DateTimeOffset(elevatorRushMinigame.GetStartTime()).ToUnixTimeMilliseconds();
      float percentage = 1 - (timeRemaining / totalTime);

      rectTransform.anchoredPosition = Vector2.Lerp(initialPosition, goalPosition, percentage);

      Vector2 goalSizeDelta = new(goalWidth, initialSizeDelta.y);
      rectTransform.sizeDelta = Vector2.Lerp(initialSizeDelta, goalSizeDelta, percentage);

      yield return null;

    }

    if (lifeCount > 0)
    {

      rectTransform.anchoredPosition = new(initialPosition.x + offsetPositionX, goalPositionY);
      rectTransform.sizeDelta = new Vector2(goalWidth, initialSizeDelta.y);

    }
    else
    {

      GameObject.FindGameObjectWithTag("Buzzer").GetComponent<AudioSource>().Play();
      GetComponent<AudioSource>().Play();
      GetComponent<Canvas>().sortingOrder = 1;
      elevatorRushMinigame.isComplete = true;

    }

  }

}
