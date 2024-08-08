using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab proyektil yang akan ditembakkan
    public Transform shootPoint; // Titik dari mana proyektil akan ditembakkan
    public float projectileSpeed = 10f; // Kecepatan proyektil
    public Camera playerCamera; // Kamera pemain untuk menentukan arah tembakan

    void Update()
    {
        // Periksa jika tombol Ctrl kiri ditekan atau tombol mouse kiri diklik
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        // Buat instance proyektil di posisi shootPoint dengan rotasi shootPoint
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Dapatkan komponen Rigidbody dari proyektil
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Tentukan arah tembakan berdasarkan posisi kursor mouse
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 direction = (hit.point - shootPoint.position).normalized;
                rb.velocity = direction * projectileSpeed;
            }
            else
            {
                // Jika tidak mengenai apapun, tembak lurus ke depan
                rb.velocity = shootPoint.forward * projectileSpeed;
            }
        }
    }
}
