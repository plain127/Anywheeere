using Photon.Pun;
using UnityEngine;

public class Bullet2 : MonoBehaviourPun
{
    public float moveSpeed = 10;

    // Rigidboby
    Rigidbody rb;

    // 충돌 되었을 때 효과 Prefab
    public GameObject exploFactory;

    public AudioClip explosionSound;

    void Start()
    {
        // 내것일때만 
        //if(photonView.IsMine)
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.up * moveSpeed;
        }
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        // 내것일때만 
        if (photonView.IsMine)
        {
            // 부딪힌 지점을 향해서 Raycast 하자.
            Ray ray = new Ray(Camera.main.transform.position, transform.position - Camera.main.transform.position);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            // 폭발효과를 만들자
            photonView.RPC(nameof(CreatExplo), RpcTarget.All, transform.position, hit.normal);

            // 사운드 재생
            photonView.RPC(nameof(PlayExplosionSound), RpcTarget.All);

            // 나를 파괴하자
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    void CreatExplo(Vector3 position, Vector3 normal)
    {
        // 폭발효과 생성
        GameObject explo = Instantiate(exploFactory);
        // 생성된 효과를 나의 위치에 위치시키자 
        explo.transform.position = position;
        // 생성된 효과의 앞방향을 부딪힌 지점의 normal 바꾸자
        explo.transform.forward = normal;
    }

    [PunRPC]
    void PlayExplosionSound()
    {
        // 사운드 재생
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
    }
}
