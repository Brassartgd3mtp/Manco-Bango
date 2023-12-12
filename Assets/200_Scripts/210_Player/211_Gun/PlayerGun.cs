using UnityEngine;
using TMPro;

public class PlayerGun : MonoBehaviour
{
    #region Variables
    [Header("References")]
    [SerializeField] private Camera fpCam;
    [SerializeField] private Renderer nextBulletColor;
    [SerializeField] private TextMeshProUGUI reloadText;
    [SerializeField] private CanvasToggle canvasToggle;
    [SerializeField] private BarrelRotate barrelRotate;
    [SerializeField] private Transform canonPos;
    [SerializeField] private BarrelFader barrelFader;
    [SerializeField] private LayerMask hitableColliders;

    [Header("Particles")]
    public ParticleManager particleManager;
    public GameObject bossRedParticlePrefab;
    public GameObject bossBlueParticlePrefab;

    [Header("Shoot Parameters")]
    [SerializeField] private float shootDelay = 0.1f;
    private Barrel barrel;
    private bool canShoot = true;

    private int bossRedTotal = 0;
    private int bossBlueTotal = 0;
    private int bossRedCount = 0;
    private int bossBlueCount = 0;
    #endregion

    private void Start()
    {
        barrel = GetComponent<Barrel>();

        GameObject[] bossRedObjects = GameObject.FindGameObjectsWithTag("BossRed");
        GameObject[] bossBlueObjects = GameObject.FindGameObjectsWithTag("BossBlue");
        bossRedTotal = bossRedObjects.Length;
        bossBlueTotal = bossBlueObjects.Length;
    }

    /// <summary>
    /// Logique de la liste barrelStock :
    /// 
    /// La liste barrelStock, appartenant au script Barrel, est une liste de type Color.
    /// Chaque index contient la couleur lié à la recharge, voir le script Reload pour plus d'informations.
    /// 
    /// Dans mon code PlayerGun je cherche donc l'index de la balle actuelle pour la tirer,
    /// et si elle est noir je ne peux pas tirer (je considère l'emplacement vide).
    /// </summary>
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

            if (Input.GetButtonDown("Dump"))
                Dump();

            if (barrel.barrelStock[Barrel.SelectedBullet] != Color.black)
                reloadText.enabled = false;

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
        //Si la balle n'est pas noir, je peux initialiser le tir
        if (barrel.barrelStock[Barrel.SelectedBullet] != Color.black && !Cursor.visible)
        {
            PlayShotSound();
            particleManager.MuzzleFlash(barrel.barrelStock[Barrel.SelectedBullet]);

            Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            barrelRotate.Rotate();

            barrelFader.Delay = barrelFader.MaxDelay;
            barrelFader.canvaGroup.alpha = 1;

            //Si je touche un collider du LayerMask hitableColliders, j'effectue une action qui varie en fonction de son type
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitableColliders))
            {
                //Ici je gère les ennemis
                if (hit.collider.TryGetComponent(out EnemyHealth _enemyHealth))
                    _enemyHealth.TakeDamage(10);

                //Ici je gère tous les destructibles qui n'ont pas de couleur
                if (hit.transform.CompareTag("Destroyable") && hit.collider.gameObject.layer == 0)
                    Destroy(hit.transform.gameObject);

                //Ici je gère les blocks rouges du boss de fin de zone
                if (barrel.barrelStock[Barrel.SelectedBullet] == Color.red)
                {
                    if (hit.transform.CompareTag("BossRed") && hit.collider.gameObject.layer == 9)
                    {
                        Destroy(hit.collider.gameObject);
                        bossRedCount++;

                        if (bossRedParticlePrefab != null)
                            Instantiate(bossRedParticlePrefab, hit.point, Quaternion.identity);
                    }
                }

                //Et là les bleus
                else if (barrel.barrelStock[Barrel.SelectedBullet] == Color.blue)
                {
                    if (hit.transform.CompareTag("BossBlue") && hit.collider.gameObject.layer == 8)
                    {
                        Destroy(hit.collider.gameObject);
                        bossBlueCount++;

                        if (bossBlueParticlePrefab != null)
                            Instantiate(bossBlueParticlePrefab, hit.point, Quaternion.identity);
                    }
                }

                //Pour finir j'instancie un effet d'impact sur ce qui a été touché
                particleManager.Impact(barrel.barrelStock[Barrel.SelectedBullet], hit.point, hit.normal);
            }

            //Et je passe à la prochaine balle, tout en réinitialisant la couleur de l'actuelle
            barrel.NextBullet();
        }
    }

    //Cette méthode me permet de vider tout mon barillet d'un coup
    private void Dump()
    {
        PlayDumpSound();

        bossRedCount = 0;
        bossBlueCount = 0;

        for (int i = 0; i < barrel.barrelStock.Count; i++)
        {
            barrel.barrelStock[i] = Color.black;
        }
    }

    //Cette méthode comprend la logique de la fin du niveau, quand le boss a été vaincu
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

    public void PlayShotSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (barrel.barrelStock[Barrel.SelectedBullet] == Color.red)
            AudioManager.Instance.PlaySound(14, audioSource);

        if (barrel.barrelStock[Barrel.SelectedBullet] == Color.blue)
            AudioManager.Instance.PlaySound(16, audioSource);
    }

    public void PlayDumpSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(17, audioSource);
    }
}