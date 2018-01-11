using UnityEngine;
using EZObjectPools;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag != "Player") || (other.tag != "Teacher"))
        {
 //           PooledObject x = other.GetComponent<PooledObject>();
 //           other.transform.parent = x.transform;
            other.gameObject.SetActive(false);
        }
    }
}