using System.Linq;
using UnityEditor;
using UnityEngine;

public class MissingReferenceFinderWindow : EditorWindow
{
    string info = "";

    [MenuItem("Tools/Debug")]
    public static void ShowWindow()
    {
        GetWindow(typeof(MissingReferenceFinderWindow));
    }

    private void OnGUI() 
    {
        if (GUILayout.Button("Find Missing References"))
        {
            info = FindMissingReferences();
        }
        GUILayout.TextArea(info);
    }

    private string FindMissingReferences()
    {
        return new MissingReferenceFinder().Find();
    }
}
