using System.Text;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class MissingReferenceFinder
{
    public string Find()
    {
        StringBuilder output = new StringBuilder();

        var prefabPaths = GetPrefabPaths();
        foreach (var prefabPath in prefabPaths)
        {
            var prefabComponents = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath).GetComponents<Component>();
            foreach (Component component in prefabComponents)
            {
                var serializedObject  = new SerializedObject(component);
                var serializedProperty = serializedObject.GetIterator();
                while (serializedProperty.NextVisible(true))
                {
                    if (serializedProperty.propertyType == SerializedPropertyType.ObjectReference &&
                        serializedProperty.objectReferenceValue == null && 
                        serializedProperty.objectReferenceInstanceIDValue != 0) 
                    {
                        output.AppendFormat("(Missing Reference in prefab): {0},\nComponent: {1},\nField: {2}\n\n", prefabPath, component.GetType().Name, serializedProperty.displayName);
                    }
                }   
            }
        }

        return output.ToString();
    }

    private string[] GetPrefabPaths()
    {
		return AssetDatabase.GetAllAssetPaths().Where(path => path.EndsWith(".prefab")).ToArray();
	}
}

