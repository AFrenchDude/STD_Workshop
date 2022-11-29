//From Template
using UnityEngine;

public interface IPickerGhost
{
	Transform GetTransform();

	bool GetIsPlaceable();
	void PlaceGhost();
	void EnableDragNDropVFX(bool enable);
	void SetDragNDropVFXColorToGreen(bool setToGreen);
}

public interface UpgradeComponent
{
	ProjectileUpgradeData.NeutralUpgrades GetNeutralUpgradeValue();
	ProjectileUpgradeData.EnergyUpgrades GetEnergyUpgradeValue();
	ProjectileUpgradeData.FoodUpgrades GetFoodUpgradeValue();
	ProjectileUpgradeData.TrapUpgrades GetTrapUpgradeValue();
}
