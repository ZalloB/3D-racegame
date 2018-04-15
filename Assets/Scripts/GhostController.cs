using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

    public GameObject car;
    public GameObject carGhostPrefab;

    List<Vector3> actualPositions = new List<Vector3>();
    List<Quaternion> actualRotations = new List<Quaternion>();

    List<Vector3> positions = new List<Vector3>();
    List<Quaternion> rotations = new List<Quaternion>();
    GameObject clone;
    public bool isStartGame;
    public bool isNewLap;
    public bool isShowing;
    public bool isGhost;

    private void Start()
    {
        isStartGame = false;
        isNewLap = false;
        isShowing = false;
        isGhost = false;
    }

    private void Update()
    {
        if (isStartGame)
        {
            if (!isNewLap)
            {
                recordGhost();

                if (positions.Count > 0)
                {
                    if (!isGhost)
                    {
                        //Show ghost
                        isGhost = true;
                        clone = Instantiate(carGhostPrefab, actualPositions[0], Quaternion.identity) as GameObject;
                    }

                    if (!isShowing)
                    {
                        isShowing = true;
                        StartCoroutine("showGhost");
                    }
                }
            }
            else
            {
                //comprobar si es mejor el tiempo y guardarlo
                isNewLap = false;
                if (isNewBestTime())
                {
                    positions = actualPositions;
                    rotations = actualRotations;
                }
               
                actualPositions = new List<Vector3>();
                actualRotations = new List<Quaternion>();
            }
        }  
    }

    IEnumerator showGhost()
    {
        for(int i = 0; i < positions.Count; i ++)
        {
            clone = GameObject.FindGameObjectWithTag("Ghost");
            //Debug.Log("position " +i+ "     "+positions[i]);
            clone.transform.position = positions[i];
            //positions.RemoveAt(i);
            clone.transform.rotation = rotations[i];
            //rotations.RemoveAt(i);
            yield return new WaitForSeconds(.01f);
        }
        isShowing = false;
    }

    public void recordGhost()
    {
       actualPositions.Add(car.GetComponent<Transform>().position);
       actualRotations.Add(car.GetComponent<Transform>().rotation);  
    }

    public bool isNewBestTime()
    {
        float newTime = GameObject.Find("GameController").GetComponent<GameController>().actualTime;
        float lastTime = GameObject.Find("GameController").GetComponent<GameController>().lastTime;
        if (newTime <= lastTime)
        return true;

        return false;
    }
}
