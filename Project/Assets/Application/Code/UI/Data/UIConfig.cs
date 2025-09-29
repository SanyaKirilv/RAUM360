using UnityEngine;

[System.Serializable]
public class UIConfig
{
    public Vector2 Size = Vector2.zero;
    public Vector2 Position = Vector2.zero;
    public float Duration = 0;

    public UIConfig(Vector2? size = null, Vector2? position = null, float? duration = null)
    {
        Size = size ?? Vector2.zero;
        Position = position ?? Vector2.zero;
        Duration = duration ?? 0;
    }
}
