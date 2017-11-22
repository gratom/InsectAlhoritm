using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectsGenerator : MonoBehaviour {

    public static InsectsGenerator Instance;

    public List<string> InsectNames;
    public List<InsectBase> InsectsArray;
    public GameObject InsectGO;

    void Awake()
    {
        Instance = this;
    }

    public string RandomName()
    {
        string name = "";
        for (int i = 0; i < 3; i++)
        {
            name += InsectNames[Random.Range(0, InsectNames.Count)];
        }
        return name;
    }

    public void CreateInsects(int Amount)
    {
        if (InsectsArray.Count != 0)
        {
            DeleteInsects();
        }
        InsectsArray = new List<InsectBase>();
        for (int i = 0; i < Amount; i++)
        {
            GameObject GI = Instantiate(InsectGO);
            GI.GetComponent<InsectBase>().SetInsect(i, RandomName(), Setting.Instance.StartPheromone);
            InsectsArray.Add(GI.GetComponent<InsectBase>());
        }
        //Debug.Log(ArrayOfSity.Count);
    }

    public void DeleteInsects()
    {
        foreach (InsectBase I in InsectsArray)
        {
            Destroy(I.InsectObject);            
        }
    }

    public InsectBase GetInsectFromID(int InsectID)
    {
        foreach (InsectBase s in InsectsArray)
        {
            if (s.ID == InsectID)
            {
                return s;
            }
        }
        return new InsectBase();
    }

}
