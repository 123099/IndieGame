using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [Tooltip("The container of the options menu")]
    [SerializeField] protected RectTransform optionsMenuContainer;

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
    }

    #endregion
}
