using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour {

    public Text winText;

    void OnTriggerEnter(Collider finished)
    {
        winText.text = "you Win";
    }
}
