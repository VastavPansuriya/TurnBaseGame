using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

   [SerializeField] private Image sliderImage;
   [SerializeField] private Player whichPlayer;

   private float startingHealth = 0;
   private float curruntHealth = 0;

   private void Awake()
   {

      sliderImage = GetComponent<Image>();

      startingHealth = whichPlayer.type.StartingHealth;

   }
   private void OnEnable()
   {
      GameEvents.OnHealthChange += Player_OnHealthChange;
   }

   private void Player_OnHealthChange()
   {
      curruntHealth = whichPlayer.type.GetHealth();

      if (curruntHealth <= 0)
      {

         transform.parent.gameObject.SetActive(false);

      }

      float sliderValue = curruntHealth / startingHealth;

      sliderImage.fillAmount = sliderValue;
   }
   private void OnDisable()
   {

      GameEvents.OnHealthChange -= Player_OnHealthChange;

   }
}
