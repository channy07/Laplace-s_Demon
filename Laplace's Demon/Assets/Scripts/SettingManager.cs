using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour
{
    public TMP_InputField gravityAccelerationInput;

    // Start is called before the first frame update
    void Start()
    {
        gravityAccelerationInput.text = Variables.gravityConstant.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
