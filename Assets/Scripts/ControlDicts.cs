using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlDicts
{

    public static readonly Dictionary<string, KeyCode> PlayerControls_2 = new Dictionary<string, KeyCode>()
    {
        {"left", KeyCode.J},
        {"right", KeyCode.L},
        {"jump", KeyCode.I},
        {"stomp", KeyCode.K}
    };
    
    public static readonly Dictionary<string, KeyCode> PlayerControls_1 = new Dictionary<string, KeyCode>()
    {
        {"left", KeyCode.A},
        {"right", KeyCode.D},
        {"jump", KeyCode.W},
        {"stomp", KeyCode.S}
    };
}
