using UnityEngine;
/// <summary>
/// Attach this script in inspector for which you want to put a comment.
/// </summary>
public class CommentInspector : MonoBehaviour
{
    [TextArea]
    public string Comment = "";  //Write comments from inspector, not here
}