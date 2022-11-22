using UnityEngine;

//Made by Remy Melinon
public class SplineFollower : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SplineDone spline;
    public float speed = 1f;
    [Header("Space betweew each wagon")]
    [Tooltip("5 between each, dont set locomotives")]
    public float margin;

    private float moveAmount;
    private float maxMoveAmount;

    private void Start()
    {
        maxMoveAmount = spline.GetSplineLength();
        moveAmount = maxMoveAmount - margin;
    }

    private void Update()
    {
        moveAmount = (moveAmount + (Time.deltaTime * speed)) % maxMoveAmount;
        transform.position = spline.GetPositionAtUnits(moveAmount);
        transform.forward = spline.GetForwardAtUnits(moveAmount);        
    }
}
