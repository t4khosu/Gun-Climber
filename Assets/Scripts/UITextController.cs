using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextController : MonoBehaviour
{

    public static UITextController instance { get; set; }

    public TextMeshProUGUI Manual;
    public TextMeshProUGUI Score;
    
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
