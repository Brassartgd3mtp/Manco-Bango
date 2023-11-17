using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera fpCam;
    [SerializeField] private Renderer nextBulletColor;
    [SerializeField] private TextMeshProUGUI reloadText;
    [SerializeField] private CanvasToggle canvasToggle;
    public ParticleManager particleManager;
    [SerializeField] private LayerMask ignoredColliders;
    private Barrel barrel;
    public GameObject bossRedParticlePrefab;
    public GameObject bossBlueParticlePrefab;

    private int bossRedTotal = 0;
    private int bossBlueTotal = 0;
    private int bossRedCount = 0;
    private int bossBlueCount = 0;

    private void Start()
    {
        barrel = GetComponent<Barrel>();

        // Assurez-vous que les composants TextMeshPro et les barres de progression sont correctement référencés

        // Comptez le nombre total d'objets "BossRed" et "BossBlue" dans la scène
        GameObject[] bossRedObjects = GameObject.FindGameObjectsWithTag("BossRed");
        GameObject[] bossBlueObjects = GameObject.FindGameObjectsWithTag("BossBlue");
        bossRedTotal = bossRedObjects.Length;
        bossBlueTotal = bossBlueObjects.Length;

        // Mettez à jour l'UI des compteurs de boss
        UpdateBossCountsUI();
    }

    private void UpdateBossCountsUI()
    {
        // Mettez à jour le texte pour afficher le total et le nombre restant d'objets "BossRed" et "BossBlue"

    }


    private void Update()
    {
        if (!canvasToggle.isGamePaused)
        {
            if (Input.GetButtonDown("Fire1")) Shoot();

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

    private void Shoot()
    {
        if (barrel.barrelStock[Barrel.SelectedBullet] != Color.black && !Cursor.visible)
        {
            Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(ignoredColliders)))
            {
                if (hit.collider.gameObject.layer == 10)
                {
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth is not null)
                    {
                        enemyHealth.TakeDamage(10);
                        Debug.Log("-10PV");
                    }
                }

                if (hit.transform.CompareTag("Destroyable") && hit.collider.gameObject.layer == 0)
                {
                    Destroy(hit.transform.gameObject);
                    UpdateBossCountsUI();
                }

                if (barrel.barrelStock[Barrel.SelectedBullet] == Color.red)
                {
                    if (hit.transform.CompareTag("BossRed") && hit.collider.gameObject.layer == 9)
                    {
                        Destroy(hit.transform.gameObject);
                        bossRedCount++;
                        UpdateBossCountsUI();

                        if (bossRedParticlePrefab != null)
                        {
                            Instantiate(bossRedParticlePrefab, hit.point, Quaternion.identity);
                            Debug.Log("Particule");
                        }
                    }
                }

                else if (barrel.barrelStock[Barrel.SelectedBullet] == Color.blue)
                {
                    if (hit.transform.CompareTag("BossBlue") && hit.collider.gameObject.layer == 8)
                    {
                        Destroy(hit.transform.gameObject);
                        bossBlueCount++;
                        UpdateBossCountsUI();

                        if (bossBlueParticlePrefab != null)
                        {
                            Instantiate(bossBlueParticlePrefab, hit.point, Quaternion.identity);
                        }
                    }
                }

                particleManager.Impact(barrel.barrelStock[Barrel.SelectedBullet], hit.point, hit.normal);
            }
            barrel.NextBullet();
        }
    }

    private void Dump()
    {
        bossRedCount = 0;
        bossBlueCount = 0;
        UpdateBossCountsUI();

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
