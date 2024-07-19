using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropdownUpdater : MonoBehaviour
{
    public TMP_Dropdown dropdown;  // Reference to the dropdown component

    void Start()
    {
        List<string> options = new List<string>
        {
            "Lentils",
            "Apples and Bananas",
            "Stew",
            "Watermelon",
            "Chips",
            "Alcohol",
            "Cola",
            "Beer",
            "SweetPeppers",
            "Pasta Sauce",
            "Tomatos and Eggplant",
            "Stew (1)",
            "Soda",
            "Tomato Paste",
            "Yogurt",
            "Coffee",
            "Chips Bag",
            "Meat",
            "Toast",
            "Bread",
            "Muffin",
            "Icecream",
            "Donut",
            "Milk",
            "Soap",
            "Cheese",
            "Cleaning Solution",
            "Wine Glasses",
            "Plates",
            "Martini Glasses",
            "Mugs"
        };

        UpdateDropdown(options);
    }

    void UpdateDropdown(List<string> options)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }
}
