using UnityEngine;
using TMPro;

public class PlayerGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera fpCam;
    [SerializeField] private Renderer nextBulletColor;
    [SerializeField] private TextMeshProUGUI reloadText;
    [SerializeField] private CanvasToggle canvasToggle;
    [SerializeField] private BarrelRotate barrelRotate;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform canonPos;
    [SerializeField] private LayerMask hitableColliders;

    [Header("Particles")]
    public ParticleManager particleManager;
    public GameObject bossRedParticlePrefab;
    public GameObject bossBlueParticlePrefab;

    [Header("Shoot Parameters")]
    [SerializeField] private float shootDelay = 0.02f;
    [SerializeField] private int bulletSpeed = 10;
    private Barrel barrel;
    private bool canShoot = true;

    private int bossRedTotal = 0;
    private int bossBlueTotal = 0;
    private int bossRedCount = 0;
    private int bossBlueCount = 0;

    private void Start()
    {
        barrel = GetComponent<Barrel>();

        // Comptez le nombre total d'objets "BossRed" et "BossBlue" dans la scène
        GameObject[] bossRedObjects = GameObject.FindGameObjectsWithTag("BossRed");
        GameObject[] bossBlueObjects = GameObject.FindGameObjectsWithTag("BossBlue");
        bossRedTotal = bossRedObjects.Length;
        bossBlueTotal = bossBlueObjects.Length;
    }


    private void Update()
    {
        if (!canvasToggle.isGamePaused)
        {
            if (Input.GetButtonDown("Fire1") && canShoot)
            {
                canShoot = false;
                Shoot();
                Invoke(nameof(ShootDelay), shootDelay);
            }

            if (Input.GetButtonDown("Dump")) Dump();

            if (barrel.barrelStock[Barrel.SelectedBullet] != Color.black) reloadText.enabled = false;
            if (barrel.barrelStock[Barrel.SelectedBullet] == Color.black)
            {
                reloadText.enabled = true;
                nextBulletColor.material.color = Color.black;
            }
            else
                nextBulletColor.material.color = barrel.barrelStock[Barrel.SelectedBullet];
        }

        CheckMagicObjects();
    }

    private void ShootDelay()
    {
        canShoot = true;
    }

    private void Shoot()
    {
        if (barrel.barrelStock[Barrel.SelectedBullet] != Color.black && !Cursor.visible)
        {
            Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            barrelRotate.Rotate();

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitableColliders))
            {
                if (hit.collider.gameObject.layer == 10)
                {
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth is not null)
                    {
                        enemyHealth.TakeDamage(10);
                    }
                }

                if (hit.transform.CompareTag("Destroyable") && hit.collider.gameObject.layer == 0)
                {
                    Destroy(hit.transform.gameObject);
                }

                if (barrel.barrelStock[Barrel.SelectedBullet] == Color.red)
                {
                    if (hit.transform.CompareTag("BossRed") && hit.collider.gameObject.layer == 9)
                    {
                        Destroy(hit.transform.gameObject);
                        bossRedCount++;

                        if (bossRedParticlePrefab != null)
                        {
                            Instantiate(bossRedParticlePrefab, hit.point, Quaternion.identity);
                        }
                    }
                }

                else if (barrel.barrelStock[Barrel.SelectedBullet] == Color.blue)
                {
                    if (hit.transform.CompareTag("BossBlue") && hit.collider.gameObject.layer == 8)
                    {
                        Destroy(hit.transform.gameObject);
                        bossBlueCount++;

                        if (bossBlueParticlePrefab != null)
                        {
                            Instantiate(bossBlueParticlePrefab, hit.point, Quaternion.identity);
                        }
                    }
                }

                particleManager.Impact(barrel.barrelStock[Barrel.SelectedBullet], hit.point, hit.normal);
            }

            Vector3 direction = hit.point - canonPos.position;

            GameObject _bullet = Instantiate(bullet, canonPos.position, Quaternion.identity);

            _bullet.transform.forward = direction.normalized;

            _bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * bulletSpeed, ForceMode.Impulse);
            _bullet.GetComponent<Renderer>().material.color = barrel.barrelStock[Barrel.SelectedBullet];

            Destroy(_bullet, 1);

            barrel.NextBullet();
        }
    }

    private void Dump()
    {
        bossRedCount = 0;
        bossBlueCount = 0;

        for (int i = 0; i < barrel.barrelStock.Count; i++)
        {
            barrel.barrelStock[i] = Color.black;
        }
    }

    private void CheckMagicObjects()
    {
        GameObject[] bossRedObjects = GameObject.FindGameObjectsWithTag("BossRed");
        GameObject[] bossBlueObjects = GameObject.FindGameObjectsWithTag("BossBlue");

        if (bossRedObjects.Length == 0 && bossBlueObjects.Length == 0)
        {
            GameObject[] magicObjects = GameObject.FindGameObjectsWithTag("Magic");
            foreach (var magicObject in magicObjects)
            {
                Destroy(magicObject);
            }
        }
    }
}
