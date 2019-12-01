using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RectTransform))]
public sealed class RectTransformEditor : Editor
{
    private static readonly Type DEFAULT_EDITOR_TYPE = typeof(Editor)
        .Assembly
        .GetType("UnityEditor.RectTransformEditor");

    private Editor m_defaultEditor;

    public override void OnInspectorGUI()
    {
        if (m_defaultEditor == null)
        {
            m_defaultEditor = CreateEditor(target, DEFAULT_EDITOR_TYPE);
        }

        m_defaultEditor.OnInspectorGUI();

        BoxCollider2D collider = (target as Transform).GetComponent<BoxCollider2D>();
        if (collider)
        {
            if (GUILayout.Button("Fecth Size BoxCollider2d"))
            {
                var concertTarget = target as RectTransform;
                collider.size = concertTarget.sizeDelta;
            }
        }
    }
}