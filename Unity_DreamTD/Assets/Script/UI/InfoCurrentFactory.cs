using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoCurrentFactory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name = null;
    [SerializeField] private TextMeshProUGUI _production = null;
    [SerializeField] private TextMeshProUGUI _maxStorage = null;
    [SerializeField] private Transform _position = null;
    private float _awakeTime = 0.0f;
    public TextMeshProUGUI Name => _name;
    public TextMeshProUGUI Production => _production;
    public TextMeshProUGUI MaxStorage => _maxStorage;
}
