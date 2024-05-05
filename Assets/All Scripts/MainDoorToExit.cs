using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainDoorToExit : MonoBehaviour
{
    private Animator anim;
    private bool Isopen = false;
    private bool isLocked = true;
    private int collectedKeys = 0;
    private int requiredKeys = 5;
    private bool isPlayerColliding = false;

    [SerializeField]
    private Text openDoorText;

    public AudioSource doorOpenSound;
    public AudioSource doorLockedSound;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        openDoorText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Isopen && collectedKeys >= requiredKeys && isPlayerColliding && Input.GetKey(KeyCode.E))
        {
            anim.SetTrigger("Open");
            Isopen = false;
            doorOpenSound.Play();
        }
        else if (isLocked && isPlayerColliding && Input.GetKey(KeyCode.E))
        {
            doorLockedSound.Play(); // Play the door locked sound
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(collectedKeys >= requiredKeys)
            {
                Isopen = true;
                isLocked = false;
            }
            isPlayerColliding = true;
            openDoorText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isPlayerColliding = false;
        }
        openDoorText.gameObject.SetActive(false);
    }

    public void KeysCollected()
    {
        collectedKeys++;

        if (collectedKeys >= requiredKeys)
        {
            // Optionally, you can trigger some visual or audio feedback
            // to indicate that the player has collected enough items to open the door.
        }
    }
}
