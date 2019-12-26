using UnityEngine;
using System.Collections;

public class DoorNearbyTrigger : MonoBehaviour
{
    [SerializeField] EndLvlDoorMain EndLvlDoorMain;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EndLvlDoorMain.PlayerNearbyAction();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EndLvlDoorMain.PlayerSteppedBack();
    }
}