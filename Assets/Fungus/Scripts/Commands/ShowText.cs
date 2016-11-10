using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

[CommandInfo("UI",
             "Show Text",
             "Makes a text visible / invisible by setting the color alpha.")]
public class ShowText : Command {

    [Tooltip("The text to show/hide")]
    [SerializeField] protected Text text;

    [Tooltip("Make the text visible/invisible")]
    [SerializeField] protected BooleanData _visible;

    [Tooltip("Affect the visibility of child sprites")]
    [SerializeField] protected bool affectChildren = true;

    protected virtual void SetTextAlpha (Text text, bool visible)
    {
        Color textColor = text.color;
        textColor.a = visible ? 1f : 0f;
        text.color = textColor;
    }

    #region Public members

    public override void OnEnter ()
    {
        if (text != null)
        {
            if (affectChildren)
            {
                var texts = text.gameObject.GetComponentsInChildren<Text>();
                for (int i = 0; i < texts.Length; i++)
                {
                    var txt = texts[i];
                    SetTextAlpha(txt, _visible.Value);
                }
            }
            else
            {
                SetTextAlpha(text, _visible.Value);
            }
        }

        Continue();
    }

    public override string GetSummary ()
    {
        if (text == null)
        {
            return "Error: No text selected";
        }

        return text.name + " to " + ( _visible.Value ? "visible" : "invisible" );
    }

    public override Color GetButtonColor ()
    {
        return new Color32(221, 184, 169, 255);
    }

    #endregion
}
