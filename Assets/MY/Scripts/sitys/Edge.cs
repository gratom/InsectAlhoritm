using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge 
{
    public int IDSity1;
    public int IDSity2;

    public float PheromoneLevel;
    public float Distance;

    public void SolveDistance()
    {
        Corditanes cord1 = SityGenerator.Instance.GetSityFromID(IDSity1).SityPlace;
        Corditanes cord2 = SityGenerator.Instance.GetSityFromID(IDSity2).SityPlace;
        Distance = Corditanes.Distance(cord1, cord2);
    }

    public bool IsExist(int ID)
    {
        if (ID == IDSity1 || ID == IDSity2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetEdge(SityBase sity1, SityBase sity2)
    {
        IDSity1 = sity1.ID;
        IDSity2 = sity2.ID;
        SolveDistance();
    }

    public void SetEdge(int ID1, int ID2)
    {
        IDSity1 = ID1;
        IDSity2 = ID2;
        SolveDistance();
    }

    public void UpdatePheromone()
    {
        PheromoneLevel *= Setting.Instance.EvaporationKoef;
    }

    public int GetAnother(int Sity)
    {
        if (Sity == IDSity1)
        {
            return IDSity2;
        }
        else 
            if (Sity == IDSity2)
            {
                return IDSity1;
            }
        return -1;
    }

    public float GetWeight()
    {
        float W = Setting.Instance.AverageDistance * ((PheromoneLevel * Setting.Instance.PheromonWeight) + ((1 / Distance) * Setting.Instance.DistanceWeight)); // normilize
        return W;
    }

}
