using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

  private int score = 0;
  private int lifeCount = 0;

  public UIDocument lifeContainerDocument;
  [SerializeField] private VisualTreeAsset lifeTemplateDocument;
  public float speed = 1.0f;

  private static Player currentPlayer;

  void Awake()
  {

    // Keep an instance across scenes and destroy any duplicate instances.
    if (currentPlayer != null) 
    {
      Destroy(gameObject);
      return;
    }

    currentPlayer = this;
    DontDestroyOnLoad(this);

  }

  public int GetScore()
  {
    return score;
  }

  public void SetScore(int newScore)
  {

    score = newScore;

    GameObject scoreGameObject = GameObject.FindGameObjectWithTag("Score");
    UIDocument scoreDocument = scoreGameObject.GetComponent<UIDocument>();
    VisualElement root = scoreDocument.rootVisualElement;
    root.Q<Label>("score").text = $"Score: {score}";

  }

  public int GetLifeCount()
  {
    return lifeCount;
  }

  public void SetLifeCount(int newLifeCount)
  {

    lifeCount = newLifeCount;

    VisualElement root = lifeContainerDocument.rootVisualElement;
    VisualElement lifeContainer = root.Q<VisualElement>("LifeContainer");
    lifeContainer.Clear();
    for (int i = 0; lifeCount > i; i++)
    {
      
      lifeContainer.Add(lifeTemplateDocument.CloneTree());

    }

  }

}
