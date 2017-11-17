using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SityBase
{
    public GameObject SityObject;

    public Corditanes SityPlace = new Corditanes();
    public string SityName = "Sity1";
    public float PheromoneLevel = 0;
    public int ID;

    public void SetSity(Corditanes Place, string Name, float Pheromon, int SityID)
    {
        SityPlace = Place;
        SityObject.transform.position = SityPlace.ToPosition(new Vector3(0, 0, 0), 7.5f);
        SityName = Name;
        ID = SityID;
        PheromoneLevel = Pheromon;
    }

}
