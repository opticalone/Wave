using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilesPerHour : MonoBehaviour {

    public Text niceee;
    int score = 0;
 
	void Start () {
		
	}
                                                                                                                              

	void Update () {
        score++;
        niceee.text = score.ToString();
	}
}
