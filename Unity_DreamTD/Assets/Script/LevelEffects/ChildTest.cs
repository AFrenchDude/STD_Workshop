using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTest : ParentTest
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other); // Do parent's thing
        Debug.Log("Do my child thing");
    }
}
