using UnityEngine;

public class FinishLineController : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("GameController").GetComponent<GameController>().SendMessage("CheckWin");
        }
    }
}