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
    public float duration; // Etkinin süresi
    public float valueChange; // Özellikteki deðiþim
    public EffectType effectType; // Etkinin türü

    public Effect(float duration, float valueChange, EffectType effectType)
    {
        this.duration = duration;
        this.valueChange = valueChange;
        this.effectType = effectType;
    }


}