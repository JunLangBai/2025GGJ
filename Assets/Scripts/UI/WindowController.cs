using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WindowController : MonoBehaviour
{
    public GameObject window;

    private void Start()
    {
        if (window != null)
        {
            window.SetActive(false);
        }
    }

    public void ShowWindow()
    {
        if (window != null)
        {
            window.SetActive(true);
        }
    }

    public void HideWindow()
    {
        if (window != null)
        {
            window.SetActive(false);
        }
    }

    public void ToggleWindow()
    {
        if (window != null)
        {
            window.SetActive(!window.activeSelf);
        }
    }
}
