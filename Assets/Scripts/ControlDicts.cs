using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlDicts
{

    public static readonly Dictionary<string, KeyCode> PlayerControls_2 = new Dictionary<string, KeyCode>()
    {
        {"forward", KeyCode.I},
        {"left", KeyCode.J},
        {"right", KeyCode.L},
        {"back", KeyCode.K},
        {"jump", KeyCode.RightShift},
        {"stomp", KeyCode.RightControl}
    };
    
    public static readonly Dictionary<string, KeyCode> PlayerControls_1 = new Dictionary<string, KeyCode>()
    {
        {"forward", KeyCode.W},
        {"left", KeyCode.A},
        {"right", KeyCode.D},
        {"back", KeyCode.S},
        {"jump", KeyCode.LeftShift},
        {"stomp", KeyCode.LeftControl}
    };
}
