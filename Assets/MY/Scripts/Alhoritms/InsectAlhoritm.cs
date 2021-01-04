using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsectAlhoritm : MonoBehaviour {

    public static InsectAlhoritm Instance;
    public bool ISWORKING = false;
    public float BestWay = 0;

    public Button StartButton;
    public Text BW;

    void Awake()
    {
        Instance = this;
    }

    public void CreateEnvironment()
    {
        // creating sitys and insect
        SityGenerator.Instance.CreateSitys(Setting.Instance.SityCount);
        SityGenerator.Instance.CreateEdges(Setting.Instance.SityCount);
        SityGenerator.Instance.UpdateAverageDistanceWeight();
        SityGenerator.Instance.UpdateAveragePheromone();
        //InsectsGenerator.Instance.CreateInsects(Setting.Instance.InsectCount);
    }

    public void BestWayToZero()
    {
        BestWay = 0;
        BW.text = "Best insect way : " + Mathf.Round(BestWay).ToString() + "km";
    }

    public void StartAlhoritm()
    {
        if (BestWay == 0)
        {
            BestWay = Setting.Instance.AverageDistance * Setting.Instance.SityCount;
        }
        InsectsGenerator.Instance.CreateInsects(Setting.Instance.InsectCount);
        StartCoroutine(Alhoritm1());
    }

    IEnumerator Alhoritm1()
    {
        while (ISWORKING)
        {
            //evaporation
            if (InsectsGenerator.Instance.InsectsArray[0].ISFREE)
            {
                SityGenerator.Instance.EvaporateAll();
            }

            for (int i = 0; i < InsectsGenerator.Instance.InsectsArray.Count; i++)
            {
                //check the free insect
                if (InsectsGenerator.Instance.InsectsArray[i].ISFREE)
                {
                    //if free, gonna WORK!
                    InsectsGenerator.Instance.InsectsArray[i].SityGoalID = Brain(InsectsGenerator.Instance.InsectsArray[i]);
                    if (Setting.Instance.IsVisualize)
                    {
                        InsectsGenerator.Instance.InsectsArray[i].StartVisualizer();
                    }
                    else
                    {
                        InsectsGenerator.Instance.InsectsArray[i].EndOfMove();
                    }
                }
            }
            SityGenerator.Instance.UpdateAveragePheromone();            
            yield return new WaitForSeconds(0.1f);

            if (InsectsGenerator.Instance.InsectsArray[0].Memory.Count == Setting.Instance.SityCount)
            {
                foreach (InsectBase I in InsectsGenerator.Instance.InsectsArray)
                {
                    if (BestWay > I.Distance)
                    {
                        BestWay = I.Distance;
                    }
                }

                BW.text = "Best insect way : " + Mathf.Round(BestWay).ToString() + "km";

                ISWORKING = false;
            }
        }
        Debug.Log("End");
        StartButton.enabled = true;
    }

    public int Brain(InsectBase INS)
    {
        float AllWeight = 0;
        List<int> Edges = new List<int>();

        //находиим варианты куда можно пойти
        for(int i = 0; i < SityGenerator.Instance.ArrayOfEdges.Count; i++)
        {
            Edge ED = SityGenerator.Instance.ArrayOfEdges[i];
            if (ED.IsExist(INS.SityID)) // если в ребре есть город в котором сейчас находится муравей то
            {
                bool isOK = true;
                foreach (int ID in INS.Memory)
                {
                    if (ED.GetAnother(INS.SityID) == ID)
                    {
                        isOK = false;
                    }
                }
                if (isOK)
                {
                    AllWeight += ED.GetWeight(); // суммируем вес всех вариантов
                    Edges.Add(i);
                }
            }
        }

        // рандомим и выбираем куда пойдет муравей

        float Rand = Random.Range(0f, AllWeight);
        //Debug.Log("AW = " + AllWeight);
        //Debug.Log("RND = " + Rand);
        float Result = 0;
        for (int i = 0; i < Edges.Count; i++)
        {
            Result += SityGenerator.Instance.ArrayOfEdges[Edges[i]].GetWeight();
            if (Rand <= Result)
            {
                SityGenerator.Instance.ArrayOfEdges[Edges[i]].PheromoneLevel += ((INS.PheromoneLeft * Setting.Instance.PheromonLeftOnEdge) / SityGenerator.Instance.ArrayOfEdges[Edges[i]].Distance);
                INS.PheromoneLeft -= INS.PheromoneLeft * Setting.Instance.PheromonLeftOnEdge;
                return SityGenerator.Instance.ArrayOfEdges[Edges[i]].GetAnother(INS.SityID);
            }
        }
        return 0;
    }

    void Start()
    {
        CreateEnvironment();
    }

    public void ButtonStartClick()
    {
        ISWORKING = true;
        StartButton.enabled = false;
        StartAlhoritm();
    }

}
