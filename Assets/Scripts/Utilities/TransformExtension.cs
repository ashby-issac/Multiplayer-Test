using UnityEngine;

public static class TransformExtension 
{
    public static void DestroyChildren(this Transform trans, bool destroyImmediate)
    {
        foreach (Transform child in trans)
        {
            if (destroyImmediate)
                MonoBehaviour.DestroyImmediate(child.gameObject);
            else
                MonoBehaviour.Destroy(child.gameObject);
        }
    }
}
