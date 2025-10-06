using UnityEngine;

public class Lifetime : MonoBehaviour
{

    [SerializeField] float lifeTime = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(Die), lifeTime);
    }

    void Die() {
        Destroy(gameObject);
    }
}
