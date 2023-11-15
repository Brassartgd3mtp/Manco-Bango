using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerController))]
class PlayerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerController playerController = (PlayerController)target;

        DrawDefaultInspector();
        GUILayout.Label("\nPresets");
        GUILayout.BeginVertical("box");

        if (GUILayout.Button("Default"))
        {
            #region Movement
                playerController.moveSpeed = 10;
                playerController.airMultiplier = 1;
                playerController.groundDrag = 5;
            #endregion
            #region Jump
                playerController.jumpForce = 10;
                playerController.jumpCooldown = .25f;
            #endregion
            #region Coyotte
                playerController.maxCoyotteTime = 1;
            #endregion
        }

        if (GUILayout.Button("Preset 1"))
        {
            #region Movement
                playerController.moveSpeed = 15;
                playerController.airMultiplier = .4f;
                playerController.groundDrag = 5;
            #endregion
            #region Jump
                playerController.jumpForce = 10;
                playerController.jumpCooldown = .25f;
            #endregion
            #region Coyotte
                playerController.maxCoyotteTime = .5f;
            #endregion
        }

        //if (GUILayout.Button("Preset 2"))
        //    Debug.Log("It's alive: " + target.name);

        GUILayout.EndVertical();
    }
}