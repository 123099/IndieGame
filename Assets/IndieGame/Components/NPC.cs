using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NPC : MonoBehaviour
{
    [Tooltip("The ID of this NPC. These should be unique numbers.")]
    [SerializeField] protected int ID;

    protected LuaScript npcScript;

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

	void OnTriggerEnter ()
    {
        ExecuteScript();
    }

    #region Public members

    public virtual void ExecuteScript ()
    {
        if (npcScript != null)
        {
            npcScript.OnExecute();
        }
    }

    #endregion
}
