using UnityEngine;

public class ChemicalMenuManager : MonoBehaviour
{
    public GameObject menu1; // First menu panel
    public GameObject menu2; // Second menu panel

    void Start()
    {
        menu1.SetActive(false);
        menu2.SetActive(false);
    }

    public void ToggleMenu1()
    {
        menu1.SetActive(!menu1.activeSelf);
    }

    public void ToggleMenu2()
    {
        menu2.SetActive(!menu2.activeSelf);
    }

    public void HideAllMenus()
    {
        menu1.SetActive(false);
        menu2.SetActive(false);
    }
}
