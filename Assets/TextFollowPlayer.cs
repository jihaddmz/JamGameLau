using UnityEngine;
using TMPro;

public class FollowPlayerText : MonoBehaviour
{
    public Transform player; // your capsule
    public TMP_Text label;

    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position + Vector3.up * 2);
        label.transform.position = screenPos;
    }
}
