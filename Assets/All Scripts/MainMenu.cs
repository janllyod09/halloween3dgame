using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool quit=false;
    public AudioSource audioSource;

    private void OnMouseEnter()
    {
        audioSource.Play();
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnMouseUp()
    {
        if (quit == true)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}