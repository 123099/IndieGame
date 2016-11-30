using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Tooltip("The container of the options menu")]
    [SerializeField] protected RectTransform optionsMenuContainer;

    [Tooltip("The first button that should be highlighted when the options menu is enabled")]
    [SerializeField] protected Button firstButton;

    protected bool isOpen;
    
    protected virtual void Awake ()
    {
        if(optionsMenuContainer == null)
        {
            Debug.LogError("Options menu was not set for object " + name);
        }
        else
        {
            optionsMenuContainer.gameObject.SetActive(false);
        }
    }

    private IEnumerator DoSelectButton()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }

    protected virtual void Update ()
    {
        CheckInput();
    }

    /// <summary>
    /// Checks whether the User clicked the pause button to open or close the menu
    /// </summary>
    protected virtual void CheckInput ()
    {
        if(Input.GetButtonDown("Pause"))
        {
            SetOpen(!isOpen);
        }
    }

    #region Public members

    public virtual void SetOpen(bool open)
    {
        isOpen = open;
        optionsMenuContainer.gameObject.SetActive(isOpen);
        GameplayUtils.SetPaused(isOpen);

        if(isOpen)
        {
            StartCoroutine(DoSelectButton());
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public virtual void ResetProgress ()
    {
        SetOpen(false);
        GameplayUtils.ResetProgress();
        Fungus.SceneLoader.LoadScene("Village", null);
    }

    public virtual void QuitGame ()
    {
        GameplayUtils.QuitGame();
    }

    #endregion
}
