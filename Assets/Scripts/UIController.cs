using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public static UIController instance { get; set; }

    public TextMeshProUGUI Manual;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI FinalScore;
    public GameObject FailedCanvas;
    
    void Awake()
    {
        instance = this;
        FailedCanvas.SetActive(false);
    }

    void Update()
    {
        
    }
}
