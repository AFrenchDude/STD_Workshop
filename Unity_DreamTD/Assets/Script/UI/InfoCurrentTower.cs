using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoCurrentTower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name = null;
    [SerializeField] private TextMeshProUGUI _damage = null;
    [SerializeField] private TextMeshProUGUI _firerate = null;
    [SerializeField] private TextMeshProUGUI _range = null;
    [SerializeField] private Transform _position = null;
    private float _awakeTime = 0.0f;
    public TextMeshProUGUI Name => _name;
    public TextMeshProUGUI Damage => _damage;
    public TextMeshProUGUI Firerate => _firerate;
    public TextMeshProUGUI Range => _range;
}
