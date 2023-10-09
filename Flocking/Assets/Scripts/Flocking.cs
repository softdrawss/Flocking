using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flocking : MonoBehaviour
{
    public FlockingManager myManager;
    float intervalTime;
    float speed = 5;
    Vector3 direction;

    void Start()
    {
        StartCoroutine(NewHeading());
    }


    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                           Quaternion.LookRotation(direction),
                                           myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }

    Vector3 Cohesion()
    {
        Vector3 cohesion = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    num++;
                }
            }
        }
        if (num > 0) { cohesion = (cohesion / num - transform.position).normalized * speed; }

        return cohesion;
    }

    Vector3 Align()
    {
        Vector3 align = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    align += go.GetComponent<Flocking>().direction;
                    num++;
                }
            }
        }
        if (num > 0)
        {
            align /= num;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
        }
        return align;
    }

    Vector3 Separation()
    {
        Vector3 rand = UnityEngine.Random.insideUnitCircle * 10;
        Vector3 separation = Vector3.zero;
        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= myManager.neighbourDistance)
                    separation -= (transform.position - go.transform.position) /
                                  (distance * distance);
                separation -= rand;
            }
        }
        return separation;
    }

    IEnumerator NewHeading()
    {
        intervalTime = Random.Range(0.0f,1.0f);
        while (true)
        {
            foreach (GameObject go in myManager.allFish)
            {
                //// Seek
                //if (myManager.followLeader)
                //{

                //    //if(go == myManager.allFish[0])
                //    //{
                //    //    direction = (Cohesion() + Align() + Separation()).normalized * speed;
                //    //    print("leader change posiiton");
                //    //}
                //    //else
                //    //{
                //    direction = myManager.allFish[0].transform.position - go.transform.position;
                //    print("everyone");
                //    //}
                //    break;
                //}

                if (myManager.bounded == true && myManager.bound.Contains(go.transform.position) == true)
                {
                    direction = (Cohesion() + Align() + Separation()).normalized * speed;
                }
                else
                {
                    direction = (myManager.bound.center - transform.position).normalized;
                }   
            }
            yield return new WaitForSeconds(intervalTime);
        }
    }
}