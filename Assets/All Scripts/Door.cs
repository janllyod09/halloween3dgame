using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    private Animator anim;
    private bool Isopen = false;

    public AudioSource doorOpenSound;
    [SerializeField]
    private Text openDoorText;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        openDoorText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Isopen == true && (Input.GetKey(KeyCode.E)))
        {
            anim.SetTrigger("OpenDoor");
            Isopen = false;
            doorOpenSound.Play();
            openDoorText.gameObject.SetActive(false);
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Isopen = true;
            openDoorText.gameObject.SetActive(true);
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        //anim.SetTrigger("CloseDoor");
        Isopen = false;
        openDoorText.gameObject.SetActive(false);
    }
}
