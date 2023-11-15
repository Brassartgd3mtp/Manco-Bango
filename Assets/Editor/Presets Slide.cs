using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerSlide))]
class PlayerSlideEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerSlide playerSlide = (PlayerSlide)target;

        DrawDefaultInspector();
        GUILayout.Label("\nPresets");
        GUILayout.BeginVertical("box");

        if (GUILayout.Button("Default"))
        {
            #region Slide
            playerSlide.maxSlideTime = .75f;
            playerSlide.slideForce = 200;
            playerSlide.slideYScale = .5f;
            #endregion
            #region FOV
            playerSlide.fovModifier = 50;
            #endregion
        }

        if (GUILayout.Button("Preset 1"))
        {
            #region Slide
            playerSlide.maxSlideTime = .75f;
            playerSlide.slideForce = 500;
            playerSlide.slideYScale = .5f;
            #endregion
            #region FOV
            playerSlide.fovModifier = 50;
            #endregion
        }

        //if (GUILayout.Button("Preset 2"))
        //    Debug.Log("It's alive: " + target.name);

        GUILayout.EndVertical();
    }
}
