using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Minigame : MonoBehaviour
{

  public string task;
  public bool isComplete = false;
  protected Player player;
  protected ProgressReport progressReport;

  protected void Start()
  {

    try {

      player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
      progressReport = GameObject.FindGameObjectWithTag("ProgressReport").GetComponent<ProgressReport>();

    } catch (NullReferenceException)
    {

      SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
      
    }

  }

  protected abstract IEnumerator StartMinigame();
  
}
