using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Vector3 offset;
    void Start()
    {
        offset = this.transform.position - player.transform.position;
    }

    // Update is called once per frame
    Vector3 lastPlayerPos;
    void Update()
    {
        if(player.IsDie || lastPlayerPos == player.transform.position)
            return;
        var targetPlayerPos = new Vector3(player.transform.position.x,0,player.transform.position.z);
        transform.position = targetPlayerPos + offset;
        lastPlayerPos = player.transform.position;
    }
}
