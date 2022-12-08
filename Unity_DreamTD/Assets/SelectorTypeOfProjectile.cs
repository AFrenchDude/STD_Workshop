using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorTypeOfProjectile : MonoBehaviour
{
    [SerializeField]
    private Image _background;

    [SerializeField]
    private Image _icon;

    private ProjectileType _type;

    private CurrentProjectileUI _projectileUI;

    public void SetProjectileType(ProjectileType projectileType, CurrentProjectileUI parentRef)
    {
        _projectileUI = parentRef;

        _type = projectileType;

        _background.color = _type.ProjectileColor;
        _icon.sprite = _type.Icon;
    }

    //public void 

    public void SelectThisProjectile()
    {
        _projectileUI.ChangeProjectile(_type);
    }
}
