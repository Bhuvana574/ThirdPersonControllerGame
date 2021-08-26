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
    
    public static PlayerMovement instance;

    public int bulletCount = 50;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > fireRate)
        {
            if (Input.GetMouseButton(0))
            {

                timer = 0f;
                Shoot();
                audioSource.clip = audioClip[0];
                audioSource.Play();
                audioSource.loop = false;

            }
            else
            {
                muzzlePrfeab.SetActive(false);
                
            }
        }
        Movement();

        //raycast from the centre of main camera


    }

    private void Shoot()
    {
        muzzlePrfeab.SetActive(true);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("raycast got hit" + hit.transform.name);
            ObjectPooling.instance.AddParticleEffect(hitMarketPrefab);
            ObjectPooling.instance.Spawning(hit);
            audioSource.clip = audioClip[1];
            audioSource.Play();
            audioSource.loop = true;
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
}
