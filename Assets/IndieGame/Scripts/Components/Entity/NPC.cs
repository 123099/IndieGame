using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;

public class NPC : MonoBehaviour, IInteractble
{
    [Tooltip("The ID of this NPC. These should be unique numbers.")]
    [SerializeField] protected int ID;

    protected LuaScript npcScript;

    protected bool busy = false;

	void Start () {
        LoadNPCScript();
	}
	
    void LoadNPCScript ()
    {
        npcScript = GetComponent<LuaScript>();

        if(npcScript != null)
        {
            TextAsset npcScriptCode = Resources.Load("NPCScripts/" + ID) as TextAsset;

            if (npcScriptCode != null)
            {
                npcScript.SetLuaFile(npcScriptCode);
            }
        }
    }

    #region Public members

    public virtual void ExecuteScript ()
    {
        if (npcScript != null)
        {
            if (busy)
            {
                return;
            }

            busy = true;

            npcScript.OnExecute(delegate { busy = false; });
        }
    }

    public void Interact ()
    {
        ExecuteScript();
    }

    #endregion
}
