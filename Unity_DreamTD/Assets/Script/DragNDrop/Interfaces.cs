using UnityEngine;

public interface IPickerGhost
{
	Transform GetTransform();

	bool GetIsPlaceable();
}
