using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Pengaturan Koin")]
    public int totalKoin;
    private int koinTerkumpul = 0;

    [Header("Pengaturan Skor Zombie")]
    [SerializeField] private int skor = 0;

    void Start()
    {
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    private void OnEnable()
    {
        Enemy.OnZombieMati += TambahSkorSaatZombieMati;
    }

    private void OnDisable()
    {
        Enemy.OnZombieMati -= TambahSkorSaatZombieMati;
    }

    private void TambahSkorSaatZombieMati(Enemy zombieYangMati)
    {
        skor += 10;
        Debug.Log($"Zombie mati: {zombieYangMati.name} | Skor saat ini: {skor}");
    }

    public void AmbilKoin()
    {
        koinTerkumpul++;

        if (koinTerkumpul == totalKoin)
        {
            Menang();
        }
    }

    void Menang()
    {
        Debug.Log("KAMU MENANG!");
    }
}