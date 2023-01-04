using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FindAllButtons : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _animatorController;

    private void Start()
    {
        DontDestroyOnLoad(this);
        UpdateList();
    }

    private void OnLevelWasLoaded(int level)
    {
        UpdateList();
    }

    private void UpdateList()
    {
        List<Button> _allButtonsInScene = new List<Button>();
        _allButtonsInScene = FindObjectsOfType<Button>().ToList();

        SetScriptOnButton(_allButtonsInScene);
    }

    private void SetScriptOnButton(List<Button> list)
    {
        StandartButtonAnimation _script;

        for (int i = 0; i < list.Count; i++)
        {
            _script = list[i].gameObject.AddComponent<StandartButtonAnimation>();
            _script.AnimatorController = _animatorController;
        }
    }

    private IEnumerator TimerToUpdate()
    {
        yield return new WaitForSeconds(3f);
        UpdateList();
    }
}
