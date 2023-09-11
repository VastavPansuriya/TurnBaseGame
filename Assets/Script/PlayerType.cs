using System;
using UnityEngine;

public interface ITakeDamage
{
   public void TakeDamage();
   public void GetHeal();
}


[System.Serializable]

public class PlayerType : ITakeDamage
{

   private int damageValue = 0;
   private int playerHealth = 0;
   private int healValue = 0;

   public int StartingHealth { get; private set; }

   public Animator _animator;

   public PlayerType(int healthAmount, int healAmount, int damageAmount)
   {

      playerHealth = healthAmount;

      StartingHealth = healthAmount;

      damageValue = damageAmount;

      healValue = healAmount;

   }
   public void TakeDamage()
   {
      playerHealth -= damageValue;
   }
   public int GetHealth()
   {

      return playerHealth;

   }
   public void GetHeal() {

      playerHealth += healValue;
      if (playerHealth >= StartingHealth)
      {
         playerHealth = StartingHealth;
      }
   }
}


public static class GameEvents
{
   public static Action OnHealthChange;
   public static Action<int> OnPlayerWin;
   public static Action OnPlayerMiss;
} 

