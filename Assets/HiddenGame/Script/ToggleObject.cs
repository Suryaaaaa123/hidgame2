using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    // Referensi untuk objek yang akan diaktifkan/dinonaktifkan
    public GameObject targetObject;

    // Status apakah objek aktif atau tidak
    private bool isActive = true;

    void Update()
    {
        // Periksa apakah tombol "P" ditekan
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Balik status aktif
            isActive = !isActive;

            // Atur aktif/tidak aktifnya objek
            targetObject.SetActive(isActive);
        }
    }
}
