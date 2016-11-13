using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Narrative",
             "Menu Title",
             "Set the title of the currently active menu dialog.")]
public class MenuTitle : Command
{
    [Tooltip("The title to set to the active menu dialog")]
    [SerializeField] protected StringData title;

    #region Public members

    public override void OnEnter ()
    {
        MenuDialog menuDialog = MenuDialog.GetMenuDialog();

        if(menuDialog != null)
        {
            menuDialog.SetTitle(title);
        }

        Continue();
    }

    public override string GetSummary ()
    {
        return title;
    }

    public override Color GetButtonColor ()
    {
        return new Color32(242, 209, 176, 255);
    }

    #endregion
}
