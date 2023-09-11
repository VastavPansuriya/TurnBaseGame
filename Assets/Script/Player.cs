using UnityEngine;

public class Player : MonoBehaviour {

   public PlayerType type;
   public Animator _animator;

   [SerializeField] private int healthAmount = 0;

   [SerializeField] private int damageAmount = 0;

   [SerializeField] private int healAmount = 0;
   
   private void Awake() {   
      type = new PlayerType(healthAmount, healAmount, damageAmount);
   }
   private void Start()
   {

      _animator = transform.GetChild(1).GetComponent<Animator>();

      type._animator = _animator;

      GameEvents.OnHealthChange?.Invoke();

   }
   public void TakeDamage(PlayerType playerType)
   {
      playerType.TakeDamage();

      GameEvents.OnHealthChange?.Invoke();

   }
   public void SelfHeal()
   {
      type.GetHeal();

      GameEvents.OnHealthChange?.Invoke();
   }
   public bool SpacialAbiliy(PlayerType playerType)
   {
      int ran = Random.Range(0, 10);

      if (ran <= 3)
      {

         type.GetHeal();

         playerType.TakeDamage();

         GameEvents.OnHealthChange?.Invoke();
         return true;
      }
      return false;
   }
}
