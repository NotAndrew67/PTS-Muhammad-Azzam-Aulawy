using System; // Wajib untuk Action
using UnityEngine;
using UnityEngine.Events; // Wajib untuk UnityEvent

public class Enemy : MonoBehaviour
{
    [Header("Scriptable Object Config")]
    [SerializeField] private ZombieConfig config;

    public static event Action<Enemy> OnZombieMati;

    [SerializeField] private UnityEvent onZombieMatiVisual;

    [SerializeField] private int hp = 100;
    public float ms = 2f;
    protected Transform player;

    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.5f;
    [SerializeField] private float jedaSerang = 1f;
    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;

    protected virtual void Start()
    {
        if (config != null)
        {
            hp = config.hp;
            ms = config.ms;
            jarakDeteksi = config.jarakDeteksi;
            jarakSerang = config.jarakSerang;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        PeriksaTransisi();

        // switch (state)
        // {
        //     case StateZombie.IDLE: PerilakuIdle(); break;
        //     case StateZombie.PATROL: PerilakuPatrol(); break;
        //     case StateZombie.CHASE: PerilakuChase(); break;
        //     case StateZombie.ATTACK: PerilakuAttack(); break;
        // }
    }

    public float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= jarakSerang)
            state = StateZombie.ATTACK;   
        else if (jarak <= jarakDeteksi)
            state = StateZombie.CHASE;  
        else
            state = StateZombie.PATROL;  
    }

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy menyerang!");
    }

    void PerilakuIdle() { }

    void PerilakuPatrol()
    {
        Debug.Log("Enemy sedang patroli");
    }

    void PerilakuChase()
    {
        Kejar();
        Debug.Log("Enemy Sedang mengejar player");      
    }

    void PerilakuAttack()
    {
        Debug.Log("Enemy Menyerang Player");
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;
        Debug.Log($"{gameObject.name} kena damage {jumlah}, HP sisa: {hp}");

        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {
        Debug.Log(gameObject.name + " kalah!");

        OnZombieMati?.Invoke(this);

        onZombieMatiVisual?.Invoke();

        Destroy(gameObject);
    }
}