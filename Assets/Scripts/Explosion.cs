using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject); // Destruye el objeto explosion
    }
}
