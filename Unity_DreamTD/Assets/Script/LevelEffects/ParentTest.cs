using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentTest : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("test parent");
    }
}
