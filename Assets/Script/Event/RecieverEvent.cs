using UnityEngine;

public class RecieverEvent : MonoBehaviour
{
    private void OnEnable()
    {
        PemancarEvent.SaatTombolDitekan += Respon;
    }

    private void OnDisable()
    {
        PemancarEvent.SaatTombolDitekan -= Respon;
    }

    void Respon()
    {
        Debug.Log("RecieverEvent Menerima Event Tombol ditekan");
    }
}