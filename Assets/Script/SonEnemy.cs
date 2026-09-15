using UnityEngine;

public class SonEnemy : Enemy
{
    public bool flag = true;

    public override void Serang()
    {
        Debug.Log("Son Gigit");
    }
}