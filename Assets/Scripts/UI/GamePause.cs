using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public GameObject window;
    private bool isPaused = false;

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

    // �����ͣ��ʱ���ô˷���
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
       
        isPaused = true;
        
        if (SceneManager.GetActiveScene().name == "MapChooseScene")
        {
            Time.timeScale = 1f;
        }
        else 
        { 
            Time.timeScale = 0f;  // ��Ϸ��ͣ
        }
        ShowWindow();  // ��ʾ����
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  // �ָ���Ϸ
        HideWindow();  // ���ش���
    }
}