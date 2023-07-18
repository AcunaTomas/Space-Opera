using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Event), true)]
public class Event_Custom_Editor : Editor
{
    public Event evento;
    public bool show;
    public override void OnInspectorGUI()
    {
        evento = (Event) target;
        EditorGUILayout.Separator();
        evento.options =  (Event.eventType) EditorGUILayout.EnumPopup("Type", evento.options);
        
        switch (evento.options)
        {
            case Event.eventType.Spawn:
                {
                    base.serializedObject.FindProperty("spawnLocation").vector2Value = EditorGUILayout.Vector2Field("Spawn Location", evento.spawnLocation);
                    base.serializedObject.FindProperty("_thingToSpawn").objectReferenceValue = EditorGUILayout.ObjectField("Thing To Spawn", evento._thingToSpawn, typeof(GameObject));
                    break;
                }
            case Event.eventType.Teleport:
                {
                    base.serializedObject.FindProperty("who").objectReferenceValue = EditorGUILayout.ObjectField("Who?", evento.who, typeof(GameObject));
                    base.serializedObject.FindProperty("where").vector2Value = EditorGUILayout.Vector2Field("Where?", evento.where);
                    break;
                }
            case Event.eventType.EndLevel:
                {
                    base.serializedObject.FindProperty("sceneName").stringValue = EditorGUILayout.TextField("Scene Name", evento.sceneName);
                    break;
                }
        }

        evento.single_use = (bool)EditorGUILayout.Toggle("Single Use?", evento.single_use);
        EditorGUILayout.Separator();

        base.serializedObject.ApplyModifiedProperties();
    }

}
