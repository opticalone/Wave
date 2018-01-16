using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuOptions : MonoBehaviour
{
    public GameObject startMarker;
    public GameObject endMarker;
    public GameObject camera;
    public void optionsClicked()
    {

        camera.transform.position = Vector3.Lerp(startMarker.transform.position, endMarker.transform.position, 10);
    }

}
