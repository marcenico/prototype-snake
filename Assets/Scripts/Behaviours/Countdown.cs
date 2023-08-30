using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
  [SerializeField] private float countdownDuration = 3f; // Duration of the countdown in seconds
  [SerializeField] private TextMeshProUGUI countdownText; // Reference to the UI text that displays the countdown
  private readonly string startGamePhrase = "Goo!";

  private void Start()
  {
    StartCoroutine(StartCountdown());
  }

  private IEnumerator StartCountdown()
  {
    int countdownValue = (int)countdownDuration;
    countdownText.gameObject.SetActive(true);

    while (countdownValue > 0)
    {
      countdownText.text = countdownValue.ToString();
      yield return new WaitForSeconds(1f);
      countdownValue--;
    }

    countdownText.text = startGamePhrase;
    yield return new WaitForSeconds(1f);

    countdownText.gameObject.SetActive(false);

    GameManager.Instance.StartGame();
  }
}
