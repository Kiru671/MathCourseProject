using UnityEngine;

public class HitRay : MonoBehaviour
{
    [SerializeField, Range(0,15)] private int ShiftCount;
    void Update()
    {
        int layerMask = (1 << 9 | 1 << 10);
        
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            Debug.Log("Missed");
        }
    }
}
