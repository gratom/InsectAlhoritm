using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour {

    public static Setting Instance;

    public int SityCount;
    public int InsectCount;
    public float EARTH_RADIUS;
    public float EvaporationKoef;
    public float PheromonWeight;
    public float DistanceWeight;
    public float AverageDistance;
    public bool IsVisualize = true;
    public float StartPheromone;
    public float PheromonLeftOnEdge; //количество оставляемое на ребрах при прохождении (процент)
    public float PheromoneRegen = 0;
    public float Speed;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
