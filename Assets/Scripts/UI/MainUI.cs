using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    private Text cherryNum;

    private Text gemNum;

    private void OnEnable()
    {
        cherryNum = transform.GetChild(0).GetComponent<Text>();
        gemNum = transform.GetChild(1).GetComponent<Text>();
    }
    void Start()
    {
        
    }
    
    void Update()
    {
        cherryNum.text = GameManager.Instance.CurCherryNum.ToString();
        gemNum.text = GameManager.Instance.CurGemNum.ToString();
    }
}
