using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HUDanimationTest : MonoBehaviour
{
    [SerializeField]
    private Animator animator_ = null;

    private bool hudSwaper_ = false;

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
        SetCurrentButton(currentButton_[0]);
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

    private void SetAllButtonsInteractable()
    {
        foreach (Button button in currentButton_)
        {
            button.interactable = true;

            button.animator.SetBool("Selected 0", false);
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

        currentButton_[buttonIndex].animator.SetBool("Selected 0",true);
    }

    private void HUDshow()
    {
        animator_.SetBool("animation", true);
    }

    private void HUDhide()
    {
        animator_.SetBool("animation", false);
    }

    public void ShowTowerShop()
    {
        towerShopHUD_.SetActive(true);
    }

    public void HideTowerShop()
    {
        towerShopHUD_.SetActive(false);
    }
}