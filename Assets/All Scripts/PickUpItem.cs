using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{

    [SerializeField]
    private Text pickUpText;
    private bool pickUpAllowed;
    private KeysCounterInScreen itemCounterUI;


    // Start is called before the first frame update
    void Start()
    {
        pickUpText.gameObject.SetActive(false);
        itemCounterUI = FindObjectOfType<KeysCounterInScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pickUpAllowed && Input.GetKeyDown(KeyCode.C))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name.Equals("FPS Player"))
        {
            pickUpText.gameObject.SetActive(true);
            pickUpAllowed = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.name.Equals("FPS Player"))
        {
            pickUpText.gameObject.SetActive(false);
            pickUpAllowed = false;
        }
    }

    public void PickUp()
    {
        Destroy(gameObject);
        pickUpText.gameObject.SetActive(false);
        pickUpAllowed = false;

        MainDoorToExit door = FindObjectOfType<MainDoorToExit>();
        if (door != null)
        {
            door.KeysCollected();
        }

        if (itemCounterUI != null)
        {
            itemCounterUI.IncrementItemCount();
        }
    }
}
