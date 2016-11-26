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

    protected bool isVisible; //Tracks whether the UI is currently faded in or out

    protected Image[] cachedUIImages; //A list of images to fade
    protected Text[] cachedUITexts; //A list of texts to fade
    protected List<LTDescr> currentlyFadingTasks; //A list of the currently fading UI components

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

        //Cache all the UI elements that can fade
        cachedUIImages = GetComponentsInChildren<Image>();
        cachedUITexts = GetComponentsInChildren<Text>();

        //Initialize fading task list
        currentlyFadingTasks = new List<LTDescr>(cachedUIImages.Length + cachedUITexts.Length);

        //Fade the UI out so that it can fade in on enable
        FadeUI(0, 0);

        //Set visibility to false
        isVisible = false;
    }

    protected virtual void OnEnable ()
    {
        FadeUIIn();
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
                FadeUIIn();
            }
            else if(fadeOutTimer.IsReady())
            {
                FadeUIOut();
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
    }

    protected virtual bool HasHealthChanged ()
    {
        return previousHealth != healthComponent.GetCurrentHealth();
    }

    protected virtual void FadeUIIn()
    {
        //Fade In
        FadeUI(1f, fadeInTime);

        //Mark as visible
        isVisible = true;

        //Reset the fade out timer to start from the moment this UI became visible
        fadeOutTimer.Reset();
    }

    protected virtual void FadeUIOut ()
    {
        //Only fade out when the UI is visible
        if (isVisible == true)
        {
            //Fade Out
            FadeUI(0f, fadeOutTime);

            //Mark as invisible
            isVisible = false;
        }
    }

    protected virtual void FadeUI(float targetAlpha, float time)
    {
        //Cancel the previously fading UI tasks
        for(int i = 0; i < currentlyFadingTasks.Count; ++i)
        {
            LeanTween.cancel(currentlyFadingTasks[i].uniqueId);
        }

        //Clear task list
        currentlyFadingTasks.Clear();

        //Fade all the images
        for(int i = 0; i < cachedUIImages.Length; ++i)
        {
            currentlyFadingTasks.Add(LeanTween.alpha(cachedUIImages[i].rectTransform, targetAlpha, time));
        }

        //Fade all the texts
        for(int i = 0; i < cachedUITexts.Length; ++i)
        {
            currentlyFadingTasks.Add(LeanTween.textAlpha(cachedUITexts[i].rectTransform, targetAlpha, time));
        }
    }
}
