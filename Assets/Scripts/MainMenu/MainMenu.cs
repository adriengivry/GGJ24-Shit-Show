using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject credits;

    [SerializeField]
    private GameObject settings;

    public void ToggleSettings()
    {
        settings.SetActive(!settings.activeSelf);
    }

    public void ToggleCredits()
    {
        credits.SetActive(!credits.activeSelf);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
