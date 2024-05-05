using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject popupPanel;
    public Text popupText;

    private bool gameStopped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!gameStopped && other.CompareTag("Player"))
        {
            ShowPopupPanel();
            StopGame();
            ToggleCursorLock();
            DisableGameAudio();
        }
    }

    private void ShowPopupPanel()
    {
        popupPanel.SetActive(true);
        popupText.gameObject.SetActive(true);
    }

    public void ToggleCursorLock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
        gameStopped = true;
    }

    private void DisableGameAudio()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            AudioSource audioSource = mainCamera.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.enabled = false;
            }
        }
    }
}