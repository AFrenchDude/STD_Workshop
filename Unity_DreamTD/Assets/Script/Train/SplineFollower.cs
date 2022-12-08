using UnityEngine;

//Original code by CodeMonkey: https://unitycodemonkey.com/video.php?v=7j_BNf9s0jM, Modified by Melinon Remy then by ALBERT Esteban to update stats via S.O and debug wagons/loco distance
public class SplineFollower : MonoBehaviour
{
    [Header("Settings")]
    public SplineDone spline;
    private float _speed = 0f;
    [Header("Space betweew each wagon")]
    [Tooltip("5 between each, dont set locomotives")]
    public float margin;

    public float SplineSpeed => _speed;

    //[HideInInspector] 
    public float moveAmount;
    //[HideInInspector] 
    public float maxMoveAmount;

    private void Start()
    {
        maxMoveAmount = spline.GetSplineLength();
        moveAmount = maxMoveAmount - margin;
    }

    private void Update()
    {
        moveAmount = (moveAmount + (Time.deltaTime * _speed));
        float newMoveAmount = moveAmount % maxMoveAmount; //Separated so wagons don't bug out
        transform.position = spline.GetPositionAtUnits(newMoveAmount);
        transform.forward = spline.GetForwardAtUnits(newMoveAmount);
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
}
