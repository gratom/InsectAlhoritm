using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SityGenerator : MonoBehaviour {

    public static SityGenerator Instance;

    public GameObject SityPrefab;
    public List<SityBase> ArrayOfSity;
    public List<Edge> ArrayOfEdges;
    public List<string> SityNames;

    void Awake()
    {
        Instance = this;
    }

    public string RandomName()
    {
        string name = "";
        for (int i = 0; i < 3; i++)
        {
            name += SityNames[Random.Range(0, SityNames.Count)];
        }
        return name; 
    }

    public Edge GetEdgeFromIDs(int ID1, int ID2)
    {
        foreach(Edge E in ArrayOfEdges)
        {
            if ((E.IDSity1 == ID1 && E.IDSity2 == ID2) || (E.IDSity2 == ID1 && E.IDSity1 == ID2))
            {
                return E;
            }
        }
        return new Edge();
    }

    public SityBase GetSityFromID(int SityID)
    {
        foreach (SityBase s in ArrayOfSity)
        {
            if (s.ID == SityID)
            {
                return s;
            }
        }
        return new SityBase();
    }

    public void CreateSitys(int Amount)
    {
        ArrayOfSity = new List<SityBase>();
        for (int i = 0; i < Amount; i++)
        {
            SityBase s = new SityBase();
            s.SityObject = Instantiate(SityPrefab);
            if (i == 0)
            {
                s.SityObject.transform.localScale *= 2;
            }
            s.SetSity(Corditanes.GetRandom(), RandomName(), 0, i);
            //Debug.Log(s.SityName);
            ArrayOfSity.Add(s);
        }
        //Debug.Log(ArrayOfSity.Count);
    }

    public void CreateEdges(int Amount)
    {
        float AverageD = 0;
        ArrayOfEdges = new List<Edge>();
        for (int i = 0; i < (Amount - 1); i++)
        {
            for (int j = 1; j < Amount; j++)
            {
                if (j > i)
                {
                    Edge e = new Edge();
                    e.SetEdge(i, j);
                    AverageD += e.Distance;
                    //Debug.Log(i.ToString() + "  " + j.ToString());
                    ArrayOfEdges.Add(e);
                }
            }
        }
        //Debug.Log(ArrayOfEdges.Count);
        Setting.Instance.AverageDistance = AverageD / ArrayOfEdges.Count;
    }

}
