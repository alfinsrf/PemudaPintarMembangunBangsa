using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCameraManager : MonoBehaviour
{    
    [SerializeField] private GameObject cameraRoom;
    [SerializeField] private PolygonCollider2D polygonCollider;
    [SerializeField] private Color gizmosColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            cameraRoom.GetComponent<CinemachineVirtualCamera>().Follow = PlayerManager.instance.currentPlayer.transform;

            cameraRoom.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            cameraRoom.GetComponent<CinemachineVirtualCamera>().Follow = null;
            cameraRoom.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(polygonCollider.bounds.center, polygonCollider.bounds.size);
    }
}
