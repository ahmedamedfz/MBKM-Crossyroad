using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanelButton : MonoBehaviour
{
 [Header("Panel")]
 [SerializeField] GameObject Panel;
 [SerializeField] bool set;
 
public void openclosepanel()
 {
    if (Panel != null)
    {   
        if (set == false) 
        {
            Panel.SetActive(true);
            set = true;
        }
        else 
        {
            Panel.SetActive(false);
            set = false;
        }
    }
 }
}
