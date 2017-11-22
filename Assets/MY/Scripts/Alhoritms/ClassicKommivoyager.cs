using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassicKommivoyager : MonoBehaviour {

    public int StartSity = 0;
    public float BestWay;
    public float WorstWay = 0;

    public Text BW;
    public Text WW;
    public Button StartButton;
    
    public void ClassicAlhoritm(int IDSity, float DistanceAll, List<int> IDSitys)
    {
        float Distance = DistanceAll;

        if (IDSitys.Count < Setting.Instance.SityCount)
        {
            for (int i = 0; i < Setting.Instance.SityCount; i++)
            {
                //float Distance = DistanceAll + SityGenerator.Instance.GetEdgeFromIDs(IDSity, i).Distance;
                if (!IDSitys.Contains(i))
                {
                    List<int> IDSitysCopy = new List<int>(IDSitys);
                    IDSitysCopy.Add(i);
                    ClassicAlhoritm(i, Distance + SityGenerator.Instance.GetEdgeFromIDs(IDSity, i).Distance, IDSitysCopy);
                }
            }
        }
        else
        {
            if (BestWay > DistanceAll)
            {
                BestWay = DistanceAll;
            }
            if (WorstWay < DistanceAll)
            {
                WorstWay = DistanceAll;
            }
        }
    }

    public void StartClassicAlhoritm()
    {
        BestWay = Setting.Instance.AverageDistance * Setting.Instance.SityCount;
        List<int> StartList = new List<int>();
        StartList.Add(0);
        ClassicAlhoritm(StartSity, 0, StartList);
        UpdateWays();
        StartButton.enabled = true;
    }

    private void UpdateWays()
    {
        BW.text = "Best way : " + Mathf.Round(BestWay).ToString() + "km";
        WW.text = "Worst way : " + Mathf.Round(WorstWay).ToString() + "km";
    }

    public void ButtonClassicClick()
    {
        StartButton.enabled = false;
        StartClassicAlhoritm();
    }


}
