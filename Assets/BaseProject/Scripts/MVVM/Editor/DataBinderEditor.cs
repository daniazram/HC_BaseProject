using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(DataBinder))]
public class DataBinderEditor : Editor
{
    private DataBinder instance;
    private int targetPropIndex;
    private int sourceValueIndex;

    public override void OnInspectorGUI()
    {
        instance = (DataBinder)target;
        var targetPropsData = PropertyFinderHelper.GetTargetProperties(instance.gameObject);
        var targetOptions = targetPropsData.Select(data => new GUIContent(string.Concat(data.type.Name, "/", data.property.PropertyType.Name, " : ", data.property.Name))).ToArray();

        var sourcePropsData = PropertyFinderHelper.GetSourceProperties(instance.gameObject);
        var sourceOptions = sourcePropsData.Select(data => new GUIContent(string.Concat(data.type.Name, "/", data.property.PropertyType.Name, " : ", data.property.Name))).ToArray();

        DrawPopup(ref targetPropIndex, new GUIContent("Target Property"), targetOptions);
        DrawPopup(ref sourceValueIndex, new GUIContent("Source Value"), sourceOptions);

        instance.TargetProperty = targetPropsData.ElementAt(targetPropIndex).FullReference;
        instance.SourceValue = sourcePropsData.ElementAt(sourceValueIndex).FullReference;
    }

    private void DrawPopup(ref int index, GUIContent label, GUIContent[] options)
    {
        index = EditorGUILayout.Popup(label, index, options);
    }
}
