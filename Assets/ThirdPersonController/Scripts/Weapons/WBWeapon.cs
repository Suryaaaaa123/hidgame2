using UnityEngine;

namespace WeirdBrothers.ThirdPersonController
{
    public enum WeaponSlot
    {
        None,
        First,
        Second
    }

    public class WBWeapon : MonoBehaviour, IItemImage, IItemName
    {
        [Header("Weapon Data")]
        [Space]
        public WBWeaponData Data;

        public WeaponSlot WeaponSlot;

        [Space]
        public Transform LeftHandRef;

        [SerializeField]
        private Transform _shellSpawnPoint;

        [SerializeField]
        private ParticleSystem _muzzelFlash;

        private int _currentAmmo;
        public int CurrentAmmo => _currentAmmo;

        private AudioSource _audioSource;
        private float _nextFire;
        private Vector3 _directionToTarget;
        private RaycastHit _hit;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _currentAmmo = Data.MagSize;
            _directionToTarget = Vector3.zero;
            _hit = new RaycastHit();
        }

        public void RemoveRigidBody()
        {
            Destroy(GetComponent<Rigidbody>());
        }

        public Sprite GetItemImage()
        {
            return Data.WeaponImage;
        }

        public string GetItemName()
        {
            return Data.WeaponName;
        }

        public void Fire(Vector3 hitPoint, LayerMask DamageLayer)
        {
            if (Time.time > _nextFire)
            {
                if (_currentAmmo > 0)
                {
                    _nextFire = Time.time + Data.FireRate;
                    _currentAmmo--;
                    _audioSource.PlayOneShot(Data.FireSound);
                    OnCaseOut();
                    _muzzelFlash.Play();

                    if (hitPoint != Vector3.zero)
                    {
                        _directionToTarget = hitPoint - _muzzelFlash.transform.position;
                        if (Physics.Raycast(_muzzelFlash.transform.position,
                            CalculateLateSpread(Data.WeaponSpread, _directionToTarget), out _hit,
                            Data.Range, DamageLayer))
                        {
                            OnTargetHit(_hit);
                        }
                    }
                }
            }
        }

        private void OnTargetHit(RaycastHit hit)
        {
            // Cek apakah objek yang terkena tembakan memiliki tag "Enemy"
            if (hit.transform.CompareTag("Enemy"))
            {
                // Hapus objek dengan tag "Enemy"
                Destroy(hit.transform.gameObject);
            }
            else
            {
                // Jika objek yang terkena tidak memiliki tag "Enemy", buat efek visual bullet hole
                GameObject bulletHole = Instantiate(Data.BulletHole, hit.point + (hit.normal * 0.01f), Quaternion.FromToRotation(-Vector3.forward, hit.normal));
                bulletHole.transform.SetParent(hit.transform);
                Destroy(bulletHole, 10);
            }
        }

        private Vector3 CalculateLateSpread(float inaccuracy, Vector3 direction)
        {
            if (inaccuracy == 0)
                return direction;
            direction.x += Random.Range(-inaccuracy, inaccuracy);
            direction.y += Random.Range(-inaccuracy, inaccuracy);
            direction.z += Random.Range(-inaccuracy, inaccuracy);
            return direction;
        }

        public void AddAmmo(int amount)
        {
            _currentAmmo += amount;
        }

        public void MagIn()
        {
            _audioSource.PlayOneShot(Data.MagInSound);
        }

        public void MagOut()
        {
            _audioSource.PlayOneShot(Data.MagOutSound);
        }

        public void Bolt()
        {
            _audioSource.PlayOneShot(Data.BoltSound);
        }

        private void OnCaseOut()
        {
            GameObject ejectedCase = Instantiate(Data.BulletShell, _shellSpawnPoint.position, _shellSpawnPoint.rotation);
            Rigidbody caseRigidbody = ejectedCase.GetComponent<Rigidbody>();
            caseRigidbody.velocity = _shellSpawnPoint.TransformDirection(-Vector3.left * Data.BulletEjectingSpeed);
            caseRigidbody.AddTorque(Random.Range(-0.5f, 0.5f), Random.Range(0.2f, 0.3f), Random.Range(-0.5f, 0.5f));
            caseRigidbody.AddForce(0, Random.Range(2f, 4f), 0, ForceMode.Impulse);
            Destroy(ejectedCase, 5f);
        }
    }
}
