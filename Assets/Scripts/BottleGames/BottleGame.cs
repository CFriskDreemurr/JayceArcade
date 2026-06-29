using UnityEngine;
using System.Collections.Generic;

public class BottleGame : MonoBehaviour
{
    [SerializeField] private List<Bottle> bottles = new();
    [SerializeField] private Transform trashOutPoint;
    public int pointCounter;

    private List<GameObject> objectToThrowOut = new List<GameObject>();
    private float waitingTime = 5f;
    private float currentTime = 0;
    private bool timerWorking = false;

    void Start()
    {
        pointCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(timerWorking)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                StopTimer();
            }
        }
    }

    public void StartTimer()
    {
        Debug.Log("Start Timer");
        currentTime = waitingTime;
        timerWorking = true;
    }

    public void ResetTimer() => currentTime = waitingTime;

    public void StopTimer()
    {
        timerWorking = false;
        ResetGame();
    }

    private void ResetGame()
    {
        Debug.Log("Gierka Siê resetuje");
        foreach(Bottle bottle in bottles)
        {
            bottle.Reset();
        }

        foreach(GameObject obj in objectToThrowOut)
        {
            if(obj.TryGetComponent<Rigidbody>(out Rigidbody objRB))
            {
                if(objRB.isKinematic == false) obj.transform.position = trashOutPoint.position;
            }
            
        }

        objectToThrowOut.Clear();
        // PrintTicets(pointCounter);
        pointCounter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Void") || other.CompareTag("Bottle")) return;

        if(!timerWorking) StartTimer();

        if (!objectToThrowOut.Contains(other.gameObject))
        {
            objectToThrowOut.Add(other.gameObject);
        }
    }
}
