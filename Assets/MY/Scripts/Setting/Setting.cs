using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour {

    public static Setting Instance;

    public int SityCount;
    public int InsectCount;
    public float EARTH_RADIUS;
    public float EvaporationKoef = 0.9f;
    public float PheromonWeight;
    public float DistanceWeight;
    public float AverageDistance;
    public bool IsVisualize = true;
    public float StartPheromone;
    public float PheromonLeftOnEdge; //количество оставляемое на ребрах при прохождении (процент)
    public float PheromoneRegen = 0;
    public float Speed;

    public Toggle IsVisual;
    public Slider RatioWeight;
    public Text RatioText;
    public Slider EvaporationSlider;
    public Text EvaporationText;
    public Slider SityCountSlider;
    public Text SityCountText;
    public Slider InsectCountSlider;
    public Text InsectCountText;
    public Button UpdateSitysButton;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateSitys()
    {
        SityGenerator.Instance.RegenerateSitys();
        InsectAlhoritm.Instance.BestWayToZero();
    }

    public void ChangeSityCount()
    {
        SityCountText.text = "Sity count : " + Mathf.Round(SityCountSlider.value);
        SityCount = Mathf.RoundToInt(SityCountSlider.value);
    }

    public void ChangeInsectCount()
    {
        InsectCountText.text = "Insect count : " + Mathf.Round(InsectCountSlider.value);
        InsectCount = Mathf.RoundToInt(InsectCountSlider.value);
    }

    public void ChangeEvaporation()
    {
        EvaporationText.text = "Evaporation : " + (Mathf.Round(EvaporationSlider.value * 100) / 100).ToString();
        EvaporationKoef = EvaporationSlider.value;
    }

    public void ChangeRatio()
    {
        RatioText.text = (Mathf.Round(RatioWeight.value * 100) / 100).ToString() + " : " + (Mathf.Round((RatioWeight.maxValue - RatioWeight.value) * 100) / 100).ToString();
        PheromonWeight = RatioWeight.value;
        DistanceWeight = (RatioWeight.maxValue - RatioWeight.value);
        SityGenerator.Instance.UpdateAverageDistanceWeight();
        SityGenerator.Instance.UpdateAveragePheromone();
    }

    public void ChangeIsVisual()
    {
        IsVisualize = IsVisual.isOn;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
