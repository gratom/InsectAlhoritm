using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

    public GameObject _Camera;
    public GameObject Center2;
    public float Speed;

    private bool IsOK = false;

	// Use this for initialization
	void Start () {
        if (_Camera != null && Center2 != null)
        {
            IsOK = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
        //controller
        if (IsOK)
        {
            if (Input.mousePosition.x > Screen.width - 3)
            {
                this.gameObject.transform.Rotate(new Vector3(0, -Speed * Time.deltaTime, 0));
            }

            if (Input.mousePosition.x < 3)
            {
                this.gameObject.transform.Rotate(new Vector3(0, Speed * Time.deltaTime, 0));
            }

            if (Input.mousePosition.y > Screen.height - 3 && (Center2.gameObject.transform.localRotation.eulerAngles.x < 15 || Center2.gameObject.transform.localRotation.eulerAngles.x > 340))
            {
                Center2.gameObject.transform.Rotate(new Vector3(Speed * Time.deltaTime, 0, 0));
            }

            if (Input.mousePosition.y < 3 && (Center2.gameObject.transform.localRotation.eulerAngles.x < 20 || Center2.gameObject.transform.localRotation.eulerAngles.x > 345))
            {
                Center2.gameObject.transform.Rotate(new Vector3(-Speed * Time.deltaTime, 0, 0));
            }

            _Camera.transform.localPosition = new Vector3(0, 0, _Camera.transform.localPosition.z + Input.mouseScrollDelta.y);
            if (_Camera.transform.localPosition.z < -50)
            {
                _Camera.transform.localPosition = new Vector3(0,0,-50);
            }

            if (_Camera.transform.localPosition.z > -15)
            {
                _Camera.transform.localPosition = new Vector3(0, 0, -15);
            }
        }
	}
}
