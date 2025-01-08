using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void EnterState(EnemyMain enemy);   // Duruma girerken yap�lacak i�ler
    void UpdateState(EnemyMain enemy); // Durum s�ras�nda s�rekli yap�lacak i�ler
    void ExitState(EnemyMain enemy);   // Durumdan ��karken yap�lacak i�ler
}