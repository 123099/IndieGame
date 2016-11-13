using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Title : MonoBehaviour
{
    protected Text cachedText;

    void Awake ()
    {
        cachedText = GetComponent<Text>();
    }

    #region Public members

    public virtual void SetTitle(string title)
    {
        //Check if we managed to cache the text component before this function call.
        //cachedText may not be set when the menudialog object just spawned, and awake hasn't been called yet.
        if(cachedText == null)
        {
            cachedText = GetComponent<Text>();
        }

        //Should not happen.
        if(cachedText == null)
        {
            return;
        }

        cachedText.text = title;
    }

    #endregion
}
