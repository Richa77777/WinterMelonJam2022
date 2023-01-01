using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private Animator _animator;

    public void StartGame()
    {
        _animator.Play("FadeOn", -1, 0);

        Invoke(nameof(LoadLvl), 1f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void LoadLvl()
    {
        SceneManager.LoadScene("Game");
    }
}
