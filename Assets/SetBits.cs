using System;
using UnityEngine;

public class SetBits : MonoBehaviour
{
    private int bitSequence = 31;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(Convert.ToString(bitSequence, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
