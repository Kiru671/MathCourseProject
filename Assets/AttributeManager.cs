using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeManager : MonoBehaviour
{
    static public int magic = 16;
    static public int intelligence = 8;
    static public int charisma = 4;
    static public int fly = 2;
    static public int invisible = 1;
    
    public Text attributeDisplay;
    private int attributes = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Magic"))
        {
            attributes |= magic;
        }
        else if (other.gameObject.CompareTag("Intelligence"))
        {
            attributes |= intelligence;
        }
        else if (other.gameObject.CompareTag("Charisma"))
        {
            attributes |= charisma;
        }
        else if (other.gameObject.CompareTag("Fly"))
        {
            attributes |= fly;
        }
        else if (other.gameObject.CompareTag("Invisible"))
        {
            attributes |= invisible;
        }
        else if (other.gameObject.CompareTag("AntiMagic"))
        {
            attributes &= ~magic;
        }
        else if (other.gameObject.CompareTag("AntiIntelligence"))
        {
            attributes &= ~intelligence;
        }
        else if (other.gameObject.CompareTag("AntiCharisma"))
        {
            attributes &= ~charisma;
        }
        else if (other.gameObject.CompareTag("AntiFly"))
        {
            attributes &= ~fly;
        }
        else if (other.gameObject.CompareTag("AntiInvisible"))
        {
            attributes &= ~invisible;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
        attributeDisplay.transform.position = screenPoint + new Vector3(0,-75,0);
        attributeDisplay.text = Convert.ToString(attributes,2).PadLeft(8,'0');
    }
       
}
