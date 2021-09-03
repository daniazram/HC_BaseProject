using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

[CustomEditor(typeof(GameEventTrigger))]
public class GameEventTriggerEditor : Editor
{
    private SerializedProperty eventsProperty;
    private GUIContent minusIcon;
    private GUIContent eventName;

    [SerializeField]
    private List<GameEvent> gameEvents = new List<GameEvent>();
    [SerializeField]
    private List<GUIContent> menuItems = new List<GUIContent>();

    void OnEnable()
    {
        eventName = new GUIContent("");
        minusIcon = new GUIContent(EditorGUIUtility.IconContent("Toolbar Minus"));
        eventsProperty = serializedObject.FindProperty("events");
        
        var ids = AssetDatabase.FindAssets("t:GameEvent");
        foreach (var id in ids)
        {
            var gameEvent = (GameEvent)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(id), typeof(GameEvent));

            gameEvents.Add(gameEvent);
            menuItems.Add(new GUIContent(gameEvent.eventType + "/" + gameEvent.name));
        }
    }
    
    public override void OnInspectorGUI()
    {
        GUILayout.Space(15f);
        serializedObject.Update();

        var removeIndex = -1;
        var removeButtonSize = GUIStyle.none.CalcSize(minusIcon);

        for (int i = 0; i < eventsProperty.arraySize; ++i)
        {
            var ev = eventsProperty.GetArrayElementAtIndex(i);
            var gameEvent = ev.FindPropertyRelative("gameEvent");
            var response = ev.FindPropertyRelative("response");
            var name = ev.FindPropertyRelative("name");
            eventName.text = name.stringValue;

            gameEvent.isExpanded = EditorGUILayout.Foldout(gameEvent.isExpanded, eventName.text);

            if (gameEvent.isExpanded)
            {
                //EditorGUILayout.PropertyField(gameEvent);
                EditorGUILayout.PropertyField(response, eventName);
                var responseRect = GUILayoutUtility.GetLastRect();
                var removeButtonPosition = new Rect(responseRect.xMax - removeButtonSize.x - 8, responseRect.y + 1, removeButtonSize.x, removeButtonSize.y);

                if (GUI.Button(removeButtonPosition, minusIcon, GUIStyle.none))
                {
                    removeIndex = i;
                }
            }

            GUILayout.Space(5f);
        }

        if (removeIndex > -1)
            RemoveEvent(removeIndex);
        
        if (GUILayout.Button("Add New Event"))
        {
            DrawMenu();
        }

        serializedObject.ApplyModifiedProperties();;
    }

    void RemoveEvent(int index)
    {
        eventsProperty.DeleteArrayElementAtIndex(index);
    }
    
    void DrawMenu()
    {
        GenericMenu menu = new GenericMenu();
        
        for (int i = 0; i < menuItems.Count; i++)
        {
            var used = false;
            var item = menuItems[i];

            for (int j = 0; j < eventsProperty.arraySize; j++)
            {
                var ev = eventsProperty.GetArrayElementAtIndex(j);
                var gEv = ev.FindPropertyRelative("gameEvent");
                var evName = ev.FindPropertyRelative("name").stringValue;
                var evType = (GameEventType)(new SerializedObject(gEv.objectReferenceValue).FindProperty("eventType").intValue);
                
                used = (string.Compare(item.text, evType + "/" + evName) == 0);

                if (used)
                    break;
            }
            
            if(used)
                menu.AddDisabledItem(item, false);
            else
                menu.AddItem(item, false, AddEvent, i);
        }

        menu.ShowAsContext();
    }

    void AddEvent(object index)
    {
        eventsProperty.arraySize += 1;
        var gameEvent = eventsProperty.GetArrayElementAtIndex(eventsProperty.arraySize-1).FindPropertyRelative("gameEvent");
        var eventName = eventsProperty.GetArrayElementAtIndex(eventsProperty.arraySize - 1).FindPropertyRelative("name");
        
        gameEvent.objectReferenceValue = gameEvents[(int)index];
        eventName.stringValue = gameEvents[(int)index].name;
        
        serializedObject.ApplyModifiedProperties();
    }
}
