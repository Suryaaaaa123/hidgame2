using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Daftar kamera yang akan digunakan
    private int currentCameraIndex = 0; // Indeks kamera saat ini

    void Start()
    {
        // Mengaktifkan kamera pertama dan menonaktifkan yang lainnya
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    void Update()
    {
        // Beralih kamera ketika tombol P ditekan
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Nonaktifkan kamera saat ini
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Perbarui indeks kamera
            currentCameraIndex++;
            if (currentCameraIndex >= cameras.Length)
            {
                currentCameraIndex = 0;
            }

            // Aktifkan kamera yang baru
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}
