using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectBase : MonoBehaviour {

    public int ID;
    public string InsectName;
    public GameObject InsectObject;
    public float PheromoneLeft;
    public float Regeneration = 0;
    public int SityID;
    public int SityGoalID;
    public Corditanes Place;
    public List<int> Memory;
    public bool ISFREE = true;
    public float Distance = 0;

    public void UpdatePosition()
    {
        InsectObject.transform.position = Place.ToPosition(new Vector3(0, 0, 0), 7.5f);
    }

    public void UpdateMemory()
    {
        Memory.Add(SityID);        
    }

    public void SetInsect(int InsectID, string IName, float Pheromone, float Regen = 0)
    {
        SityID = 0;
        SityGoalID = 0;
        ID = InsectID;
        InsectName = IName;
        PheromoneLeft = Pheromone;
        Regeneration = Regen;
        Place = new Corditanes();
        Place.SetCoordinates(SityGenerator.Instance.ArrayOfSity[SityID].SityPlace.SDegree, SityGenerator.Instance.ArrayOfSity[SityID].SityPlace.DDegree);
        Memory = new List<int>();
        InsectObject = this.gameObject;
        UpdatePosition();
        UpdateMemory();
    }

    private void MoveTo()
    {
        Corditanes Delta = new Corditanes();
        Delta.SetCoordinates(SityGenerator.Instance.ArrayOfSity[SityGoalID].SityPlace.SDegree - SityGenerator.Instance.ArrayOfSity[SityID].SityPlace.SDegree, SityGenerator.Instance.ArrayOfSity[SityGoalID].SityPlace.DDegree - SityGenerator.Instance.ArrayOfSity[SityID].SityPlace.DDegree);
        if (Delta.DDegree > 180)
        {
            Delta.DDegree = -360 + Delta.DDegree;
        }
        if (Delta.DDegree < -180)
        {
            Delta.DDegree = 360 + Delta.DDegree;
        }
        Corditanes Step = new Corditanes();

        Step.SetCoordinates(Delta.SDegree / Setting.Instance.Speed, Delta.DDegree / Setting.Instance.Speed);
        Place.SetCoordinates(Place.SDegree + Step.SDegree, Place.DDegree + Step.DDegree);
        UpdatePosition();
    }

    public void StartVisualizer()
    {
        ISFREE = false;
        StartCoroutine(Visualizer());
    }

    IEnumerator Visualizer()
    {
        for (int i = 0; i < Setting.Instance.Speed; i++)
        {
            MoveTo();
            yield return null;
        }
        EndOfMove();
    }

    public void EndOfMove()
    {
        Distance += SityGenerator.Instance.GetEdgeFromIDs(SityID, SityGoalID).Distance;
        SityID = SityGoalID;
        Place.SetCoordinates(SityGenerator.Instance.ArrayOfSity[SityID].SityPlace.SDegree, SityGenerator.Instance.ArrayOfSity[SityID].SityPlace.DDegree);
        UpdatePosition();
        UpdateMemory();
        if (Memory.Count <= Setting.Instance.SityCount)
        {
            ISFREE = true;
        }
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}    

}
