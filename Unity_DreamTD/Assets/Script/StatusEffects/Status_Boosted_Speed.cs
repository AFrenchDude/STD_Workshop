using UnityEngine;

//Made by Melinon Remy
public class Status_Boosted_Speed : MonoBehaviour
{
    //Timer
    [SerializeField] private float _slowDuration = 0.1f;
    private float _slowStartTime = 0.0f;
    //Speed
    private PathFollower _pathFollower = null;
    private float _originalSpeed = 0.0f;

    //Settings
    private void OnEnable()
    {
        _pathFollower = GetComponentInParent<PathFollower>();
        _originalSpeed = _pathFollower.OGSpeed;
        _slowStartTime = Time.time;
    }

    //Check for end of boost
    private void Update()
    {
        //If not in booster enemy range + cooldown over
        if (Time.time >= _slowStartTime + _slowDuration)
        {
            //Remove boost
            _pathFollower.SetSpeed(_originalSpeed);
            //Remove script
            Destroy(this);
        }
    }

    //Reset timer to keep boost
    public void ResetTimer()
    {
        _slowStartTime = Time.time;
    }

    //Set boost
    public void AddSpeed(float addedSpeed)
    {
        float _addedSpeed = addedSpeed;
        _pathFollower.SetSpeed(_originalSpeed + _addedSpeed);
    }
}
