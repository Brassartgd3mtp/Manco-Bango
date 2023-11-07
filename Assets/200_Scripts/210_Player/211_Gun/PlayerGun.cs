using UnityEngine;
using TMPro;

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
    public GameObject bossRedParticlePrefab; // Préfab de l'effet de particules pour les objets "BossRed"
    public GameObject bossBlueParticlePrefab; // Préfab de l'effet de particules pour les objets "BossBlue"
    public TextMeshProUGUI bossRedCountText; // Référence au composant TextMeshPro pour le total "BossRed"
    public TextMeshProUGUI bossBlueCountText; // Référence au composant TextMeshPro pour le total "BossBlue"
    private TextMeshProUGUI bossRedRemainingText; // Référence au composant TextMeshPro pour le nombre restant "BossRed"
    private TextMeshProUGUI bossBlueRemainingText; // Référence au composant TextMeshPro pour le nombre restant "BossBlue"

    private int bossRedTotal = 0; // Total d'objets "BossRed" dans la scène
    private int bossBlueTotal = 0; // Total d'objets "BossBlue" dans la scène
    private int bossRedCount = 0; // Compteur pour les objets "BossRed"
    private int bossBlueCount = 0; // Compteur pour les objets "BossBlue"

    private void Start()
    {
        barrel = GetComponent < Barrel>();

        // Assurez-vous que les composants TextMeshPro sont correctement référencés
        if (bossRedCountText == null || bossBlueCountText == null || bossRedRemainingText == null || bossBlueRemainingText == null)
        {
            Debug.LogError("Les composants TextMeshPro ne sont pas correctement référencés. Faites glisser et déposez-les dans l'Inspector Unity.");
        }

        // Comptez le nombre total d'objets "BossRed" et "BossBlue" dans la scène
        GameObject[] bossRedObjects = GameObject.FindGameObjectsWithTag("BossRed");
        GameObject[] bossBlueObjects = GameObject.FindGameObjectsWithTag("BossBlue");
        bossRedTotal = bossRedObjects.Length;
        bossBlueTotal = bossBlueObjects.Length;

        UpdateBossCountsUI();
    }

    private void UpdateBossCountsUI()
    {
        // Mettez à jour le texte pour afficher le total et le nombre restant d'objets "BossRed" et "BossBlue"
        bossRedCountText.text = "BossRed: " + bossRedCount + " / " + bossRedTotal;
        bossBlueCountText.text = "BossBlue: " + bossBlueCount + " / " + bossBlueTotal;

        bossRedRemainingText.text = "Remaining: " + (bossRedTotal - bossRedCount);
        bossBlueRemainingText.text = "Remaining: " + (bossBlueTotal - bossBlueCount);
    }

    private void Update()
    {
        if (!canvasToggle.isGamePaused)
        {
            if (Input.GetButtonDown("Fire1")) Shoot();

            if (Input.GetButtonDown("Dump")) Dump();

            if (barrel.barrelStock.Count > 0) reloadText.enabled = false;
            if (barrel.barrelStock.Count == 0)
            {
                reloadText.enabled = true;
                nextBulletColor.material.color = Color.black;
            }
            else if (barrel.barrelStock.Count >= 1) nextBulletColor.material.color = barrel.barrelStock[0];
        }

        CheckMagicObjects();
    }

    private void Shoot()
    {
        if (barrel.barrelStock.Count > 0 && !Cursor.visible)
        {
            Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(ignoredColliders)))
            {
                if (hit.collider.gameObject.layer == 10)
                {
                    // Obtenez la référence à l'ennemi s'il est touché par le raycast
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

                    if (enemyHealth is not null)
                    {
                        // Appel de la fonction TakeDamage pour réduire les points de vie de l'ennemi
                        enemyHealth.TakeDamage(10);
                        Debug.Log("-10PV"); 
                    }
                }

                if (hit.transform.CompareTag("Destroyable") && hit.collider.gameObject.layer == 0)
                {
                    Destroy(hit.transform.gameObject);
                }

                if (barrel.barrelStock[0] == Color.red)
                {
                    if (hit.transform.CompareTag("BossRed") && hit.collider.gameObject.layer == 9)
                    {
                        // L'objet touché a le tag "BossRed", détruisez-le
                        Destroy(hit.transform.gameObject);

                        // Mettez à jour le compteur pour les objets "BossRed" et l'UI
                        bossRedCount++;
                        UpdateBossCountsUI();

                        // Créez un effet de particules pour les objets "BossRed"
                        if (bossRedParticlePrefab != null)
                        {
                            Instantiate(bossRedParticlePrefab, hit.point, Quaternion.identity);
                        }
                    }
                }
                    
                else if (barrel.barrelStock[0] == Color.blue)
                {
                    if (hit.transform.CompareTag("BossBlue") && hit.collider.gameObject.layer == 8)
                    {
                        // L'objet touché a le tag "BossBlue", détruisez-le
                    Destroy(hit.transform.gameObject);

                    // Mettez à jour le compteur pour les objets "BossBlue" et l'UI
                    bossBlueCount++;
                    UpdateBossCountsUI();

                    // Créez un effet de particules pour les objets "BossBlue"
                    if (bossBlueParticlePrefab != null)
                    {
                        Instantiate(bossBlueParticlePrefab, hit.point, Quaternion.identity);
                    }
                    }
                    
                }

                // Je joue ma particule d'impact à l'endroit du contact avec la couleur de l'élément
                particleManager.Impact(barrel.barrelStock[0], hit.point, hit.normal);
            }
            barrel.RemoveStock();
        }
    }

    private void Dump()
    {
        bossRedCount = 0; // Réinitialisez le compteur pour les objets "BossRed" à zéro
        bossBlueCount = 0; // Réinitialisez le compteur pour les objets "BossBlue" à zéro
        UpdateBossCountsUI();

        barrel.barrelStock.Clear();
    }

    private void CheckMagicObjects()
    {
        GameObject[] bossRedObjects = GameObject.FindGameObjectsWithTag("BossRed");
        GameObject[] bossBlueObjects = GameObject.FindGameObjectsWithTag("BossBlue");

        // Vérifiez s'il ne reste plus aucun objet "BossRed" ni "BossBlue" dans la scène
        if (bossRedObjects.Length == 0 && bossBlueObjects.Length == 0)
        {
            // Trouvez tous les objets ayant le tag "Magic" et détruisez-les
            GameObject[] magicObjects = GameObject.FindGameObjectsWithTag("Magic");
            foreach (var magicObject in magicObjects)
            {
                Destroy(magicObject);
            }
        }
    }
}
