using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerDash))]
class PlayerDashEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerDash playerDash = (PlayerDash)target;

        DrawDefaultInspector();
        GUILayout.Label("\nPresets");
        GUILayout.BeginVertical("box");

        if (GUILayout.Button("Default"))
        {
            playerDash.dashForce = 200;
            playerDash.dashCooldown = 1;
            playerDash.dashDuration = .5f;
        }

        if (GUILayout.Button("Preset 1"))
        {
            playerDash.dashForce = 800;
            playerDash.dashCooldown = 1;
            playerDash.dashDuration = .25f;
        }

        //if (GUILayout.Button("Preset 2"))
        //    Debug.Log("It's alive: " + target.name);

        GUILayout.EndVertical();
    }
}
