using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public int numFish = 50;
    GameObject[] allFish;
    public bool followLeader = false;
    public Transform leader;

    //[Header("Fish Settings")]
    public float minSpeed;
    public float maxSpeed;
    public float neighbourDistance;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish];
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < numFish; ++i)
        {
            //Vector3 pos = this.transform.position + Random.
            //Vector3 randomize = ... // random vector direction
            //allFish[i] = (GameObject)Instantiate(fishPrefab, pos,
            //                    Quaternion.LookRotation(randomize));
            //allFish[i].GetComponent<Flock>().myManager = this;
        }
    }
}
