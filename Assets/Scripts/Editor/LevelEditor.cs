using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    void OnSceneGUI()
    {
        var level = target as Level;
        var color = new Color(0.4f, 0.6f, 0.9f, 0.2f);
        var outline = new Color(0.1f, 0.3f, 0.6f);
        var bounds = level.Bounds;
        var verts = new Vector3[] {
            level.transform.position + new Vector3(bounds.min.x, bounds.min.y, 0.0f),
            level.transform.position + new Vector3(bounds.min.x, bounds.max.y, 0.0f),
            level.transform.position + new Vector3(bounds.max.x, bounds.max.y, 0.0f),
            level.transform.position + new Vector3(bounds.max.x, bounds.min.y, 0.0f),
        };
        Handles.DrawSolidRectangleWithOutline(verts, color, outline);
    }
}
