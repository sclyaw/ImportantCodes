using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void EnterState(EnemyMain enemy);   // Duruma girerken yapýlacak iþler
    void UpdateState(EnemyMain enemy); // Durum sýrasýnda sürekli yapýlacak iþler
    void ExitState(EnemyMain enemy);   // Durumdan çýkarken yapýlacak iþler
}