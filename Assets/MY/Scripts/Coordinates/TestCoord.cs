using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoord : MonoBehaviour {

    public Corditanes Cord1 = new Corditanes();
    public Corditanes Cord2 = new Corditanes();
    public float SH1, DL1;
    public float SH2, DL2;

	// Use this for initialization
	void Start () {
        Cord1.SetCoordinates(SH1, DL1);
        Cord2.SetCoordinates(SH2, DL2);
        Debug.Log(Corditanes.Distance(Cord1, Cord2));
	}
	
	// Update is called once per frame
	void Update () {

	}
}
