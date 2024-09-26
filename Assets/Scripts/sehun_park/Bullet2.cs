using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Bullet2 : MonoBehaviourPun
{
    public float moveSpeed = 10;

    // RigidBody
    Rigidbody rb;

    // 충돌 되었을 때 효과 Prefab
    public GameObject exploFactory;

    public AudioClip explosionSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            // 플레이어와 충돌했는지 확인
            if (other.CompareTag("Player"))
            {
                // HPSystem 컴포넌트를 가져옴
                HPSystem hpSystem = other.GetComponent<HPSystem>();
                if (hpSystem != null)
                {
                    // HP 1 감소
                    hpSystem.UpdateHP(-1f);

                    // 점수 1 증가 (ScoreManager에서 관리)
                    ScoreManager.instance.AddScore(1);  // 점수 추가
                }
            }

            // 폭발 효과 및 사운드 처리
            Ray ray = new Ray(Camera.main.transform.position, transform.position - Camera.main.transform.position);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            photonView.RPC(nameof(CreatExplo), RpcTarget.All, transform.position, hit.normal);
            photonView.RPC(nameof(PlayExplosionSound), RpcTarget.All);

            // 나를 파괴
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    void CreatExplo(Vector3 position, Vector3 normal)
    {
        GameObject explo = Instantiate(exploFactory);
        explo.transform.position = position;
        explo.transform.forward = normal;
    }

    [PunRPC]
    void PlayExplosionSound()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
    }
}