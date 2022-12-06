using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HUDTestRaycast : MonoBehaviour
{
    [SerializeField]
    private Vector3 tower;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tower = this.transform.position;

        Ray ray = Camera.main.ScreenPointToRay(tower);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }
}
