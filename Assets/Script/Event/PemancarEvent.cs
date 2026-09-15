using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PemancarEvent : MonoBehaviour
{
    public static event Action SaatTombolDitekan;
    
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Tombol ditekan menerima event TekanTombol");
            SaatTombolDitekan?.Invoke();
        }
    }
}
