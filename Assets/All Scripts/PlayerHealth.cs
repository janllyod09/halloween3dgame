using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public float regenDelay = 5f; // Time delay before health regeneration starts
    public float regenRate = 2f; // Rate at which health regenerates per second

    private float regenTimer;
    private bool isRegenerating;
    private bool isBeingAttacked;

    public Canvas gameOverCanvas;
    public Image bloodOverlay; // Reference to the UI Image component for the blood overlay
    public Text healthText; // Reference to the UI Text element for displaying the player's health

    private void Start()
    {
        currentHealth = maxHealth;
        regenTimer = regenDelay;

        bloodOverlay.gameObject.SetActive(false); // Hide the blood overlay initially

        UpdateHealthText();
    }

    private void Update()
    {
        if (!isBeingAttacked && currentHealth < maxHealth)
        {
            regenTimer -= Time.deltaTime;
            if (regenTimer <= 0f)
            {
                StartRegeneration();
            }
        }
        else if (isBeingAttacked)
        {
            StopRegeneration();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isRegenerating)
        {
            StopRegeneration();
            bloodOverlay.gameObject.SetActive(false); // Hide the blood overlay
        }

        currentHealth -= damageAmount;
        Debug.Log("Player took " + damageAmount + " damage. Remaining health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FadeOverlay(true)); // Start the fading transition to show the blood overlay

            ResetRegenerationTimer();
            UpdateHealthText();
        }
    }

    public void StartAttack()
    {
        isBeingAttacked = true;
        StopRegeneration();
    }

    public void EndAttack()
    {
        isBeingAttacked = false;
        StartRegeneration();
    }

    private void StartRegeneration()
    {
        if (!isRegenerating)
        {
            isRegenerating = true;
            InvokeRepeating("RegenerateHealth", 0f, 1f / regenRate);

            StartCoroutine(FadeOverlay(false)); // Start the fading transition to hide the blood overlay
        }
    }

    private void RegenerateHealth()
    {
        currentHealth++;

        if (currentHealth >= maxHealth)
        {
            StopRegeneration();
        }

        UpdateHealthText();
    }

    private void StopRegeneration()
    {
        if (isRegenerating)
        {
            isRegenerating = false;
            CancelInvoke("RegenerateHealth");
        }
    }

    private void ResetRegenerationTimer()
    {
        regenTimer = regenDelay;
    }

    private void Die()
    {
        Debug.Log("Player died.");
        gameOverCanvas.enabled = true; // Show the game over canvas
        
        // Stop any ongoing actions or happenings in the game
        // For example, you can pause the game or stop player input by setting the Time.timeScale to 0.
        Time.timeScale = 0;
        ToggleCursorLock();

        if (healthText != null)
        {
            healthText.gameObject.SetActive(false);
        }
        // Optionally, you can disable relevant game objects to prevent further actions.
        // For example, if you have a player controller script attached to the player object, you can disable it:
        // GetComponent<PlayerController>().enabled = false;
    }
    
    public void ToggleCursorLock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    private System.Collections.IEnumerator FadeOverlay(bool fadeIn)
    {
        const float fadeDuration = 0.5f;
        float targetAlpha = fadeIn ? 1f : 0f;
        float currentAlpha = bloodOverlay.color.a;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / fadeDuration;
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, normalizedTime);

            Color overlayColor = bloodOverlay.color;
            overlayColor.a = newAlpha;
            bloodOverlay.color = overlayColor;

            yield return null;
        }

        Color finalColor = bloodOverlay.color;
        finalColor.a = targetAlpha;
        bloodOverlay.color = finalColor;

        bloodOverlay.gameObject.SetActive(fadeIn); // Show/hide the blood overlay based on the fade direction
    }
}
