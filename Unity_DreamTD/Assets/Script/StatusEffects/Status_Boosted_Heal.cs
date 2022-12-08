using UnityEngine;

//Made by Melinon Remy
public class Status_Boosted_Heal : MonoBehaviour
{
    //Timer
    [SerializeField] private float _slowDuration = 0.1f;
    private float _slowStartTime = 0.0f;
    //HP
    private Damageable _damageable = null;
    private float _originalMaxHP = 0.0f;

    //Settings
    private void OnEnable()
    {
        _damageable = GetComponentInParent<Damageable>();
        _originalMaxHP = _damageable.MaxHP;
        _slowStartTime = Time.time;
    }

    //Check for end of boost
    private void Update()
    {
        //If not in booster enemy range + cooldown over
        if (Time.time >= _slowStartTime + _slowDuration)
        {
            //Remove boost
            _damageable.setMaxHp(_originalMaxHP, false, true);
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
    public void AddHeal(float addedHeal)
    {
        float _addedHeal = addedHeal;
        _damageable.setMaxHp(_originalMaxHP + _addedHeal, false, true);
    }
}
