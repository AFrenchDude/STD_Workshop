using UnityEngine;
using UnityEngine.UI;

public class HUDAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator_ = null;

    private bool hudSwaper_ = false;
    private bool shopSwaper_ = false;

    [SerializeField]
    private GameObject factoryShopHUD_ = null;

    [SerializeField]
    private GameObject towerShopHUD_ = null;

    [SerializeField]
    private Button[] currentButton_ = null;

    //private void OnMouseOver()
    //{
    //    HUDshow();
    //}
    //private void OnMouseExit()
    //{
    //    HUDhide();
    //}

    private void Start()
    {
        towerShopHUD_.SetActive(true);
        factoryShopHUD_.SetActive(false);

        if (currentButton_.Length > 0)
        {
            SetCurrentButton(currentButton_[0]);
        }
    }

    public void HUDswaper()
    {
        if (hudSwaper_ == false)
        {
            hudSwaper_ = true;
            HUDshow();
        }
        else if (hudSwaper_ == true)
        {
            hudSwaper_ = false;
            HUDhide();
        }
    }

    public void ShopSwap()
    {
        if (shopSwaper_ == true)
        {
            shopSwaper_ = false;

            factoryShopHUD_.SetActive(true);
            towerShopHUD_.SetActive(false);
        }
        else if (shopSwaper_ == false)
        {
            shopSwaper_ = true;

            factoryShopHUD_.SetActive(false);
            towerShopHUD_.SetActive(true);
        }

    }

    private void SetAllButtonsInteractable()
    {
        foreach (Button button in currentButton_)
        {
            button.interactable = true;

            //button.animator.SetBool("Selected 0", false);
        }
    }

    public void SetCurrentButton(Button clickedButton)
    {
        int buttonIndex = System.Array.IndexOf(currentButton_, clickedButton);

        if (buttonIndex == -1)
        {
            return;
        }

        SetAllButtonsInteractable();

        clickedButton.interactable = false;

        currentButton_[buttonIndex].Select();
        ShopSwap();
        //currentButton_[buttonIndex].animator.SetBool("Selected 0", true);
    }

    private void HUDshow()
    {
        animator_.SetBool("OpenShop", true);
    }

    private void HUDhide()
    {
        animator_.SetBool("OpenShop", false);
    }

    public void ShowTowerShop()
    {
        towerShopHUD_.SetActive(false);
        factoryShopHUD_.SetActive(true);
    }

    public void HideTowerShop()
    {
        towerShopHUD_.SetActive(true);
        factoryShopHUD_.SetActive(false);
    }
}