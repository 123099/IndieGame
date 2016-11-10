using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("FMOD",
             "Play One Shot",
             "Spawn an instance of a given event at a location or attached to an object, and play it to completion")]
public class PlayOneShot : Command
{
    [FMODUnity.EventRef]
    [Tooltip("The path of the event to play")]
    [SerializeField] protected string eventPath;

    [Tooltip("The location at which to play the event. If a game object is specified, the location is ignored")]
    [SerializeField] protected Vector3Data location;

    [Tooltip("The game object to which to attached the instance of the event. If this is specified, location is ignored")]
    [SerializeField] protected GameObjectData targetGameObject;

    #region Public members

    public override void OnEnter ()
    {
        if (targetGameObject.Value != null)
            FMODUnity.RuntimeManager.PlayOneShotAttached(eventPath, targetGameObject);
        else
            FMODUnity.RuntimeManager.PlayOneShot(eventPath, location);
    }

    public override string GetSummary ()
    {
        if (eventPath == "")
            return "Error: No event path specified";

        if (targetGameObject.Value == null)
            return eventPath + " at " + location.Value;

        return eventPath + " attached to " + targetGameObject.Value.name;
    }

    public override Color GetButtonColor ()
    {
        return new Color32(235, 191, 217, 255);
    }

    #endregion
}
