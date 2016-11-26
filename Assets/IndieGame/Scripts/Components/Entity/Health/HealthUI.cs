using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Tooltip("The health component that this UI displays")]
    [SerializeField] protected Health healthComponent;

    [Tooltip("The image that displays the health percentage")]
    [SerializeField] protected Image healthImage;

    [Tooltip("The text field that displays the amount of health")]
    [SerializeField] protected Text healthText;

    [Tooltip("The standby time after which the UI fades out. This should be greater than fade out time")]
    [SerializeField] protected Timer fadeOutTimer;

    [Tooltip("The time it takes the UI to fade out")]
    [SerializeField] protected float fadeOutTime;

    [Tooltip("The time it takes the UI to fade in")]
    [SerializeField] protected float fadeInTime;

    protected float previousHealth; //Store the last health. Update the UI only when health changes

    protected virtual void Awake ()
    {
        if (healthComponent == null)
        {
            Debug.LogWarning("Health component on Health UI " + name + " is not set!");
        }
        else if(fadeOutTimer.GetValue() == 0)
        {
            fadeOutTimer.SetValue(1);
            Debug.LogWarning("Fade Out Timer on Health UI " + name + " cannot be run every 0 seconds. Settings the timer value to 1");
        }

        //Fade the UI out so that it can fade in on enable
        FadeUI(0, 0);
    }

    protected virtual void OnEnable ()
    {
        FadeUI(1, fadeInTime);
    }

    protected virtual void Update ()
    {
        //Make sure there is a health component to display
        if (healthComponent != null)
        {
            //If the health has changed, update the UI
            if (HasHealthChanged())
            {
                UpdateHealthImage();
                UpdateHealthText();
                UpdatePreviousHealth();
            }
            else if(fadeOutTimer.IsReady())
            {
                FadeUI(0f, fadeOutTime);
            }
        }
    }

    protected virtual void UpdateHealthImage ()
    {
        if(healthImage != null)
        {
            //Get the normalized value of the current health
            float normalizedHealth = healthComponent.GetCurrentHealthNormalized();

            //Set the image fill amount to the normalized health
            healthImage.fillAmount = normalizedHealth;
        }
    }

    protected virtual void UpdateHealthText ()
    {
        if(healthText != null)
        {
            //Format the current health
            string currentHealth = healthComponent.GetCurrentHealth().ToString("00");

            //Format the maximum health
            string maxHealth = healthComponent.GetMaxHealth().ToString("00");

            //Display the health
            healthText.text = currentHealth + "/" + maxHealth;
        }
    }

    protected virtual void UpdatePreviousHealth ()
    {
        //Store the current health as previous
        previousHealth = healthComponent.GetCurrentHealth();

        //Fade the UI in very fast
        FadeUI(1f, fadeInTime);

        //Reset the fade timer
        fadeOutTimer.Reset();
    }

    protected virtual bool HasHealthChanged ()
    {
        return previousHealth != healthComponent.GetCurrentHealth();
    }

    protected virtual void FadeUI(float targetAlpha, float time)
    {
        //Get all fadeable images
        var images = GetComponentsInChildren<Image>();

        //Fade the images
        for (int i = 0; i < images.Length; ++i)
        {
            LeanTween.alpha(images[i].rectTransform, targetAlpha, time).setEase(LeanTweenType.easeInOutCubic);
        }

        //Get all fadeable texts
        var texts = GetComponentsInChildren<Text>();

        //Fade the texts
        for (int i = 0; i < texts.Length; ++i)
        {
            LeanTween.textAlpha(texts[i].rectTransform, targetAlpha, time).setEase(LeanTweenType.easeInOutCubic);
        }
    }
}
