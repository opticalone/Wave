using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuOptions : MonoBehaviour
{
    public GameObject startMarker;
    public GameObject endMarker;
    public GameObject anythingbutcamera;

    public GameObject mainMenuButtons;
    public GameObject optionsMenuButtons;


    public void optionsClicked()
    {

        anythingbutcamera.transform.position = Vector3.Lerp(startMarker.transform.position, endMarker.transform.position, 10000 * Time.deltaTime);
        mainMenuButtons.SetActive(false);
        optionsMenuButtons.SetActive(true);
    }

}
