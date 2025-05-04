using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{

  public long startTimeMilliseconds;
  public long endTimeMilliseconds;

  void Update()
  {

    long currentTimeMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    float timeRemaining = endTimeMilliseconds - currentTimeMilliseconds;
    UIDocument uiDocument = GetComponent<UIDocument>();
    VisualElement root = uiDocument.rootVisualElement;

    if (timeRemaining < 0)
    {
      
      root.Q<Label>("timer").text = $"12:00 AM";

    } else {

      float totalTime = endTimeMilliseconds - startTimeMilliseconds;
      float percentage = 1 - (timeRemaining / totalTime);
      int minute = (int) Math.Floor(percentage * 59);
      string minuteString = $"{minute}".PadLeft(2, '0');
      
      root.Q<Label>("timer").text = $"11:{minuteString} PM";

    }


  }

  public void SetStartTimeMilliseconds(long endTime)
  {

    startTimeMilliseconds = endTime;

  }

  public void SetEndTimeMilliseconds(long endTime)
  {

    endTimeMilliseconds = endTime;

  }
}
