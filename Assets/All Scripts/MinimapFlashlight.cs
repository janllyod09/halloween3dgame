using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFlashlight : MonoBehaviour
{

    [SerializeField] GameObject FLLight;
    private bool FLActive = false;

    // Start is called before the first frame update
    void Start()
    {
        FLLight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(FLActive == false)
            {
                FLLight.gameObject.SetActive(true);
                FLActive = true;
            }
            else
            {
                FLLight.gameObject.SetActive(false);
                FLActive = false;
            }
        }
    }
}
