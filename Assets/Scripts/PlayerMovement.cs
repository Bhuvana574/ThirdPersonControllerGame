using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 5)]
    [SerializeField] float fireRate = 1f;
    [SerializeField] float timer = 5f;
    private CharacterController character;
    [SerializeField]
    private float playerspeed = 5;
    private float gravity = 9.8f;
    [SerializeField]
    private GameObject muzzlePrfeab;
    [SerializeField]
    private GameObject hitMarketPrefab;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClip;
    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;
    private bool isReloading = false;
    private UIManager uIManager;
    public bool hasCoin=false;
    [SerializeField]
    private GameObject weapon;
    public static PlayerMovement instance;

   

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        character = GetComponent<CharacterController>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > fireRate)
        {
            if (Input.GetMouseButton(0))
            {

                if(currentAmmo>0)
                {
                    Shoot();
                }
                timer = 0f;
                Shoot();
                audioSource.clip = audioClip[0];
                audioSource.Play();
                audioSource.loop = false;

            }
            else
            {
                muzzlePrfeab.SetActive(false);
                audioSource.clip = audioClip[1];
                audioSource.Play();
                audioSource.loop = true;
            }
            if(Input.GetKeyDown(KeyCode.R) &&  isReloading==false)
            {
                isReloading = true;
                StartCoroutine(Reload());
            }
        }
        Movement();

        //raycast from the centre of main camera


    }

    private void Shoot()
    {
        muzzlePrfeab.SetActive(true);
        currentAmmo--;
        uIManager.UpdateAmmo(currentAmmo);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("raycast got hit" + hit.transform.name);
            HitPool.instance.AddParticleEffect(hitMarketPrefab);
            HitPool.instance.Spawning(hit);
            //check if we hit crate then destroy crate
            Destructable crate = hit.transform.GetComponent<Destructable>();
            if(crate!=null)
            {
                crate.OnCrateDestroyed();
            }
            
            //  GameObject temp = (GameObject)Instantiate(hitMarketPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            //  Destroy(temp, 2.0f);

        }

    }

    private void Movement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * playerspeed;
        velocity.y -= gravity;
        velocity = transform.transform.TransformDirection(velocity);
        character.Move(velocity * Time.deltaTime);
    }
    //reload bullets
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    public void EnableWeapon()
    {
        weapon.SetActive(true);
    }
}
