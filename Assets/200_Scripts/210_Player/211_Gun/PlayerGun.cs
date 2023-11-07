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

    private void Start()
    {
        barrel = GetComponent<Barrel>();
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

                //Je joue ma particule d'impacte à l'endroit du contact avec la couleur de l'élément
                particleManager.Impact(barrel.barrelStock[0], hit.point, hit.normal);
            }
            barrel.RemoveStock();
        }
    }
    private void Dump()
    {
        barrel.barrelStock.Clear();
    }
}