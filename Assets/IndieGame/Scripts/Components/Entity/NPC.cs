using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;

public class NPC : Entity, IInteractble
{
    [Tooltip("The ID of this NPC. These should be unique numbers.")]
    [SerializeField] protected int ID;

    protected LuaScript npcScript;

    protected bool busy = false;

	protected override void Start () {
        LoadNPCScript();
	}

    protected virtual void LoadNPCScript ()
    {
        //Get the lua script component, that will execute the npc script
        npcScript = GetComponent<LuaScript>();

        //If we have a script component, that means we can execute a script. Look for an existing script
        if(npcScript != null)
        {
            TextAsset npcScriptCode = Resources.Load<TextAsset>("NPCScripts/" + ID);

            //If we haven't found an npc script, try to get the default script
            if (npcScriptCode == null)
            {
                npcScriptCode = Resources.Load<TextAsset>("NPCScripts/default");
            }

            //Set the npc script to our lua executing component
            npcScript.SetLuaFile(npcScriptCode);

        }
    }

    #region Public members

    public virtual void ExecuteScript ()
    {
        //Check if we can execute a script
        if (npcScript != null)
        {
            //We are busy when we are already executing a script, therefore, we should not do it again, until we are done.
            if (busy)
            {
                return;
            }

            //Mark ourselves as busy, since we are executing a script
            busy = true;

            //Execute a script, and mark ourselves as not busy once done
            npcScript.OnExecute(delegate { busy = false; });
        }
    }

    /// <summary>
    /// Starts an interaction sequence with the npc. If the npc has a specific npc script, it will execute that.
    /// If not, it will execute the default script, if that one exists.
    /// </summary>
    public virtual void Interact ()
    {
        ExecuteScript();
    }

    #endregion
}
