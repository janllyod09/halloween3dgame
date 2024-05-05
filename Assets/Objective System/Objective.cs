using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    public AudioSource objSFX;
    public GameObject theObjective;
    public GameObject theTrigger;
    public GameObject theText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            StartCoroutine(missionObj());
    }

    private IEnumerator missionObj()
    {
        objSFX.Play();
        theObjective.SetActive(true);
        theObjective.GetComponent<Animation>().Play("ObjectiveDisplayAnim");
        theText.GetComponent<Text>().text = "Objective: Find the five keys!!!";
        yield return new WaitForSeconds(5.3f);
        theText.GetComponent<Text>().text = "";
        theTrigger.SetActive(false);
        theObjective.SetActive(false);
    }
}
