using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Image weaponImage; // Imagen del arma
    public TextMeshProUGUI ammoText; // Texto de munición

    public Sprite cannonBallSprite; // Sprite para el cañón
    public Sprite harpoonSprite;    // Sprite para el arpón

    void Update()
    {
        UpdateWeaponUI();
    }

    void UpdateWeaponUI()
    {
        // Obtener isCannonBall desde PlayerPrefs
        bool isCannonBall = PlayerPrefs.GetInt("isCannonBall", 1) == 1;

        // Cambiar el sprite
        weaponImage.sprite = isCannonBall ? cannonBallSprite : harpoonSprite;

        // Obtener la cantidad de munición
        int ammoCount = isCannonBall ? PlayerPrefs.GetInt("CannonBallAmmo", 10) : PlayerPrefs.GetInt("HarpoonAmmo", 5);

        // Actualizar el texto
        ammoText.text = ammoCount.ToString();
    }
}
