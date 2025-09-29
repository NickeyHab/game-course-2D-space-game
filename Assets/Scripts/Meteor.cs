using Unity.VisualScripting;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            other.BroadcastMessage("Death");
        }
        
    }
}
