using UnityEngine;

public class Factory : MonoBehaviour
{
    private FactoryDatas _datas;

    private int _price = 0;

    public void Enable(bool isEnabled)
    {
        enabled = isEnabled;
    }
    public void SetPrice(int price)
    {
        _price = price;
    }
    public void SetFactoryDatas(FactoryDatas datas)
    {
        _datas = datas;
    }
}
