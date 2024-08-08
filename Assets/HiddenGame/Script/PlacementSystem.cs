using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    public GridSystem gridSystem;
    public GameObject objectToPlace;
    public GameObject previewPrefab; // Prefab untuk pratinjau
    public Transform character; // Referensi ke karakter
    public float placementDistance = 5f; // Jarak dari karakter
    public GameObject placementVFX; // Referensi ke prefab VFX

    private GameObject previewObject;
    private Material previewMaterial;
    private Material originalMaterial;

    private float rotationSpeed = 100f; // Kecepatan rotasi dengan scroll mouse

    void Start()
    {
        if (objectToPlace != null)
        {
            // Instantiate the preview object from the previewPrefab
            if (previewPrefab != null)
            {
                previewObject = Instantiate(previewPrefab);
                previewObject.GetComponent<Collider>().enabled = false;

                // Coba dapatkan Renderer dari objek utama dan anak-anak
                Renderer renderer = previewObject.GetComponent<Renderer>() ?? previewObject.GetComponentInChildren<Renderer>();
                if (renderer != null)
                {
                    originalMaterial = renderer.material;
                    previewMaterial = new Material(originalMaterial);
                    previewMaterial.color = Color.green; // Ganti dengan warna pratinjau yang diinginkan
                    renderer.material = previewMaterial;
                }
                else
                {
                    Debug.LogError("Renderer is null on previewObject. Ensure the prefab has a Renderer component or its child.");
                }
                
                // Pastikan pratinjau dimulai dalam keadaan tidak aktif
                previewObject.SetActive(false);
            }
            else
            {
                Debug.LogError("previewPrefab is null");
            }
        }
        else
        {
            Debug.LogError("objectToPlace is null");
        }
    }

    void Update()
    {
        if (gridSystem != null && previewObject != null && character != null)
        {
            Vector3 placementPosition = character.position + character.forward * placementDistance;
            Vector2Int gridPosition = gridSystem.GetGridPosition(placementPosition);
            Vector3 targetPosition = gridSystem.GetWorldPosition(gridPosition.x, gridPosition.y);
            previewObject.transform.position = targetPosition;

            // Pastikan pratinjau aktif jika diperlukan
            if (!previewObject.activeSelf)
            {
                previewObject.SetActive(true);
            }

            // Rotasi pratinjau dengan scroll mouse
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0f)
            {
                previewObject.transform.Rotate(Vector3.up, scrollInput * rotationSpeed);
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Tempatkan objek dengan material asli
                GameObject placedObject = Instantiate(objectToPlace, targetPosition, Quaternion.identity);
                Renderer placedRenderer = placedObject.GetComponent<Renderer>() ?? placedObject.GetComponentInChildren<Renderer>();
                if (placedRenderer != null)
                {
                    placedRenderer.material = originalMaterial;
                }
                else
                {
                    Debug.LogError("Renderer is null on placedObject. Ensure the prefab has a Renderer component or its child.");
                }

                if (placementVFX != null)
                {
                    Instantiate(placementVFX, targetPosition, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("placementVFX is not assigned");
                }

                // Setelah penempatan selesai, matikan pratinjau
                previewObject.SetActive(false);
            }
            else if (Input.GetMouseButtonDown(1)) // Ganti dengan tombol lain jika diperlukan
            {
                // Jika klik kanan atau tombol tertentu ditekan, aktifkan mode placement
                previewObject.SetActive(true);
            }
        }
        else
        {
            if (gridSystem == null)
            {
                Debug.LogError("gridSystem is null");
            }
            if (previewObject == null)
            {
                Debug.LogError("previewObject is null");
            }
            if (character == null)
            {
                Debug.LogError("character is null");
            }
        }
    }
}
