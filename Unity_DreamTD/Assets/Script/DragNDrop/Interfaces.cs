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
