using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject InputManagerObject = new GameObject("InputManager");
                _instance = InputManagerObject.AddComponent<InputManager>();
            }
            return _instance;
        }
        set { _instance = value; }
    }
    public delegate void Event();
    public Event OnJump;
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            OnJump?.Invoke();
        }
    }
}
