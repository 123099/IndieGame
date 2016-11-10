using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("FMOD",
             "Unload Bank",
             "Unloads all FMOD Studio Banks as specified by the attached bank loader components on a game object. " +
             "The unload option on the game object should be set to NONE.")]
public class UnloadBank : Command
{
    [Tooltip("The game object that has one or more StudioBankLoader components attached")]
    [SerializeField]
    protected GameObjectData banksLoader;

    #region Public members

    public override void OnEnter ()
    {
        if (banksLoader.Value != null)
            banksLoader.Value.SendMessage("Unload");

        Continue();
    }

    public override string GetSummary ()
    {
        if (banksLoader.Value == null)
            return "Error: No bank loader specified";

        return "Unload banks on " + banksLoader.Value.name;
    }

    public override Color GetButtonColor ()
    {
        return new Color32(235, 191, 217, 255);
    }

    #endregion
}
