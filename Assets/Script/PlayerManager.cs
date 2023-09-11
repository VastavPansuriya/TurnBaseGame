using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   [SerializeField] private List<Player> allPlayer = new List<Player>();
   [SerializeField] private int playerChoise = 0;
   [SerializeField] private bool isApplyedinput = false;
   [SerializeField] private GameObject highLightedObj;


   //vfx
   [SerializeField] private GameObject healParticle;
   [SerializeField] private GameObject targetParticle;
   [SerializeField] private GameObject hitParticle;

   private int whichPlayerTurn = 0;
   private int counter = 0;
   private int whoWins = 0;

   public bool canPlayGame = false;
   private void Start()
   {
      canPlayGame = true;
      InitializeValue();
      whichPlayerTurn = 0;
      SetHighLightPos(true);
   }

   private void InitializeValue()
   {
      isApplyedinput = false;
   }
   private void Update()
   {
      GameInput();
   }

   private void GameInput()
   {
      if (canPlayGame)
      {
         if (Input.GetKeyDown(KeyCode.A))
         {
            isApplyedinput = true;
            playerChoise = 1;
         }
         else if (Input.GetKeyDown(KeyCode.S))
         {
            isApplyedinput = true;
            playerChoise = 2;
         }
         else if (Input.GetKeyDown(KeyCode.D))
         {
            isApplyedinput = true;
            playerChoise = 3;
         }
         PlayerTurn(playerChoise);
      }
   }

   private void PlayerTurn(int playerChoise)
   {
      if (isApplyedinput)
      {
         if (playerChoise == 1)
         {
            whichPlayerTurn = counter % allPlayer.Count;
            allPlayer[whichPlayerTurn].TakeDamage(allPlayer[( counter + 1 ) % allPlayer.Count].type);
            allPlayer[whichPlayerTurn]._animator.SetTrigger("Attack");
            allPlayer[( counter + 1 ) % allPlayer.Count]._animator.SetTrigger("Damage");

            SetHighLightPos(false);
            SetParticle(0);

         }
         else if (playerChoise == 2)
         {
            whichPlayerTurn = counter % allPlayer.Count;
            allPlayer[whichPlayerTurn].SelfHeal();

            SetHighLightPos(false);
            SetParticle(1);

         }
         else if (playerChoise == 3)
         {
            whichPlayerTurn = counter % allPlayer.Count;
            bool isGetChanse = allPlayer[whichPlayerTurn].SpacialAbiliy(allPlayer[( counter + 1 ) % allPlayer.Count].type);
            SetParticle(2, isGetChanse);
            SetHighLightPos(false);

         }
         if (IfPlayerDied())
         {
            return;
         }
         
         counter++;
         if (counter >= allPlayer.Count)
         {
            counter = 0;
         }
         this.playerChoise = 0;
         isApplyedinput = false;
      }
   }
   private void SetHighLightPos(bool isFirst)
   {
      if (isFirst)
      {
         highLightedObj.transform.position = allPlayer[counter % allPlayer.Count].transform.position;
         targetParticle.transform.position = allPlayer[(counter +1)% allPlayer.Count].transform.position;
      }
      else
      {
         highLightedObj.transform.position = allPlayer[( counter + 1 ) % allPlayer.Count].transform.position;
         targetParticle.transform.position = allPlayer[( counter + 2 ) % allPlayer.Count].transform.position;
      }
   }

   private void SetParticle(int typeOf)
   {
      if (typeOf == 0)
      {
         GameObject hit = Instantiate(hitParticle, allPlayer[( counter + 1 ) % allPlayer.Count].transform.position, Quaternion.identity);
         Destroy(hit, 1f);
      }
      if (typeOf == 1)
      {
         GameObject heal = Instantiate(healParticle, allPlayer[whichPlayerTurn].transform.position, Quaternion.identity);
         Destroy(heal, 1f);
      }


   }
   private void SetParticle(int typeOf, bool isGetChase)
   {
      if (typeOf == 2 && isGetChase)
      {
         GameObject heal = Instantiate(healParticle, allPlayer[whichPlayerTurn].transform.position, Quaternion.identity);
         Destroy(heal, 1f);
         GameObject hit = Instantiate(hitParticle, allPlayer[( counter + 1 ) % allPlayer.Count].transform.position, Quaternion.identity);
         Destroy(hit, 1f);
      }
      else
      {
         GameEvents.OnPlayerMiss?.Invoke();
      }
   }
   private bool CheckDied()
   {
      if (allPlayer[( counter + 1 ) % allPlayer.Count].type.GetHealth() <= 0)
      {
         return true;
      }
      return false;
   }

   private bool IfPlayerDied()
   {
      if (CheckDied())
      {
         canPlayGame = false;
         // due to array
         whoWins = whichPlayerTurn + 1;
         GameEvents.OnPlayerWin?.Invoke(whoWins);
         return true;
      }

      return false;
   }
}
