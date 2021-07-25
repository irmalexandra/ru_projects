using System.Collections;
using UnityEngine;
public class StaminaBar : MonoBehaviour
{
    //public Slider staminaBar;

    public float maxStamina = 100;
    private float currentStamina;
    
    public float regenRate = 100;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;
    
    public static StaminaBar instance;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        //staminaBar.maxValue = maxStamina;
        //staminaBar.value = maxStamina;
    }

    
    public bool UseStamina(float amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            //staminaBar.value = currentStamina;

            if (regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(RegenStamina());
            return true;
        }
        return false;
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);

        while (currentStamina < maxStamina)
        {
            
            currentStamina += maxStamina / regenRate;
            //staminaBar.value = currentStamina;
            yield return regenTick;
        }

        regen = null;
    }

    public float GetStamPercentage()
    {
        return currentStamina / maxStamina;
    }
    
}
