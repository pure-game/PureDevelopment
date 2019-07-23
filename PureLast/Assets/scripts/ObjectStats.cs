using UnityEngine;

public abstract class ObjectStats : MonoBehaviour
{
    // отнимаем кислород
    public abstract void OxygenDamage(float damage);
    // отнимаем жизни
    public abstract void Damaged(float damage);
    // вызывается при смерти персонажа
    public abstract void Death();
}
