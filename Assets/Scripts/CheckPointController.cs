using UnityEngine;
using System.Collections;

public class CheckPointController : MonoBehaviour
{

    public int checkpointnumber;
    public bool CheckpointAdd = true;

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("<color=red>CheckPoint !!!!!!!</color> " + collision.tag);
        if (collision.tag =="Player")
        {
            StartCoroutine("AddCheckpoint", collision);
        }
       
    }



    IEnumerator AddCheckpoint(Collider collision)
    {
        if (CheckpointAdd)
        {
            CheckpointAdd = false;
            //Debug.Log("<color=red>checkpointnumber</color> " +checkpointnumber);
            GameObject.Find("GameController").GetComponent<GameController>().SendMessage("AddCheckpoint", checkpointnumber);
        }

        yield return new WaitForSeconds(1f);
        CheckpointAdd = true;
    }
}
