using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoSingleton<CanvasManager>
{

    [SerializeField] private Menu[] menus;





    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void CloseMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Close();
                break;
            }      
        }
    }

    public void OpenMenu(Menu menu)
    {
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    public void quitGame()
    {
        Application.Quit();
    }



}
