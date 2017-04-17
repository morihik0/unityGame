using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {
    //height:-240 -120 0 120 240 widht:-140~140

    public GameObject oden , doors;

    private GameObject odenObj;
    private DoorManager doorManager;

	// Use this for initialization
	void Start () {
        doorManager = doors.GetComponent<DoorManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (odenObj == null)
        {
            odenObj = (GameObject)Instantiate(oden, new Vector3(Random.Range(-140, 141), Random.Range(-2, 3) * 120, 0), Quaternion.identity);
            doorManager.DoorColorChange();
        }
    }
}
