using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketScript : MonoBehaviour
{
    public GameObject bucketEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
           GameObject effect = Instantiate(bucketEffect,transform.position,transform.rotation);
            Destroy(effect,1f);
        }
    }
}
