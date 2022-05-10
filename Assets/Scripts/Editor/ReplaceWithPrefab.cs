using UnityEngine;
using UnityEditor;

// https://unity3d.college/2017/09/07/replace-gameobjects-or-prefabs-with-another-prefab/?scrlybrkr
// FULL DISCLOSURE: I did not write this code, this code does not have any functionality in my game, this is only used for
// Unity Editor functionality to make my life a little bit easier early on in the project.

public class ReplaceWithPrefab : EditorWindow
{
    [SerializeField] private GameObject prefab;

    [MenuItem("Tools/Replace With Prefab")]
    static void CreateReplaceWithPrefab()
    {
        EditorWindow.GetWindow<ReplaceWithPrefab>();
    }

    private void OnGUI()
    {
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);

        if (GUILayout.Button("Replace"))
        {
            var selection = Selection.gameObjects;

            for (var i = selection.Length - 1; i >= 0; --i)
            {
                var selected = selection[i];
#pragma warning disable CS0618 // Type or member is obsolete
                var prefabType = PrefabUtility.GetPrefabType(prefab);
                GameObject newObject;

                if (prefabType == PrefabType.Prefab)
#pragma warning restore CS0618 // Type or member is obsolete
                {
                    newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                }
                else
                {
                    newObject = Instantiate(prefab);
                    newObject.name = prefab.name;
                }

                if (newObject == null)
                {
                    Debug.LogError("Error instantiating prefab");
                    break;
                }

                Undo.RegisterCreatedObjectUndo(newObject, "Replace With Prefabs");
                newObject.transform.parent = selected.transform.parent;
                newObject.transform.localPosition = selected.transform.localPosition;
                newObject.transform.localRotation = selected.transform.localRotation;
                //newObject.transform.localRotation = Quaternion.Euler(new Vector3(-90, -90, 0));
                newObject.transform.localScale = selected.transform.localScale;
                //newObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                newObject.transform.SetSiblingIndex(selected.transform.GetSiblingIndex());
                newObject.name = selected.name;
                Undo.DestroyObjectImmediate(selected);
            }
        }

        GUI.enabled = false;
        EditorGUILayout.LabelField("Selection count: " + Selection.objects.Length);
    }
}