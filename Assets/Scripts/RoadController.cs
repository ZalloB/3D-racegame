using UnityEngine;

public class RoadController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dentrooooooooooo");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("fueraaaaaaaaaa");
    }



}
