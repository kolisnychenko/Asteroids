using UnityEditor;
using UnityEngine;

public class ClearPlayerPrefs : EditorWindow
{
    [MenuItem("Asteroid/Clear PlayerPrefs")]
    public static void NukeEverything()
    {
        PlayerPrefs.DeleteAll();
    }
}
