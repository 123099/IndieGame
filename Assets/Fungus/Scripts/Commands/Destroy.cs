// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus
{
    /// <summary>
    /// Destroys a specified game object in the scene.
    /// </summary>
    [CommandInfo("Scripting", 
                 "Destroy", 
                 "Destroys a specified game object in the scene.")]
    [AddComponentMenu("")]
    [ExecuteInEditMode]
    public class Destroy : Command
    {   
        [Tooltip("Reference to game object or component to destroy")]
        [SerializeField] protected ObjectData _targetObject;

        #region Public members

        public override void OnEnter()
        {
            if (_targetObject.Value != null)
            {
                Destroy(_targetObject.Value);
            }

            Continue();
        }

        public override string GetSummary()
        {
            if (_targetObject.Value == null)
            {
                return "Error: No object selected";
            }

            return _targetObject.Value.name;
        }

        public override Color GetButtonColor()
        {
            return new Color32(235, 191, 217, 255);
        }

        #endregion

        #region Backwards compatibility

        [HideInInspector] [FormerlySerializedAs("targetGameObject")] public GameObject targetGameObjectOLD;

        protected virtual void OnEnable()
        {
            if (targetGameObjectOLD != null)
            {
                _targetObject.Value = targetGameObjectOLD;
                targetGameObjectOLD = null;
            }
        }

        #endregion
    }
}