using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    AttackPower,
    Speed,
    AttackRange,
    Health,
    HealthPermanent,
    Doom,
    Freeze,
    Stun
}

public class Effect
{
    public float duration; // Etkinin s�resi
    public float valueChange; // �zellikteki de�i�im
    public EffectType effectType; // Etkinin t�r�

    public Effect(float duration, float valueChange, EffectType effectType)
    {
        this.duration = duration;
        this.valueChange = valueChange;
        this.effectType = effectType;
    }


}