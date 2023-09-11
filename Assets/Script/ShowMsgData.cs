using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowMsgData : MonoBehaviour
{
   [SerializeField] private GameObject youMissObj;

   [SerializeField] private GameObject winPanel;
   [SerializeField] private Text winText;
   [SerializeField] private Button replayButton;

   private void Awake()
   {
      winPanel.SetActive(false);
   }

   private void OnEnable()
   {
      GameEvents.OnPlayerMiss += OnPlayerMissAttack;
      GameEvents.OnPlayerWin += OnShowWin;
   }

   private void OnPlayerMissAttack()
   {
      GameObject miss = Instantiate(youMissObj, transform);
      Destroy(miss,2f);
   }

   private void OnShowWin(int whichPlayer)
   {
      winPanel.SetActive(true);
      winText.text = "Player " + whichPlayer + " is win";
      StartCoroutine(DelayToOpenButton());
   }

   private IEnumerator DelayToOpenButton()
   {
      yield return new WaitForSeconds(2);
      replayButton.gameObject.SetActive(true);
      AddListenerToReplayButton();
   }

   public void AddListenerToReplayButton()
   {
      replayButton.onClick.AddListener (()=> {
         SceneManager.LoadScene(0);
      });
   }

   private void OnDisable()
   {
      GameEvents.OnPlayerMiss -= OnPlayerMissAttack;
      GameEvents.OnPlayerWin -= OnShowWin;
   }
}
