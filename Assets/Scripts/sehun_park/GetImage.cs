using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetImage : MonoBehaviourPun
{
    public string url;              // 요청할 URL
    public Renderer targetRenderer; // 이미지를 적용할 3D 오브젝트의 Renderer
    public Material defaultMaterial; // 초기 상태의 기본 Material (선택 사항)

    private void Update()
    {
        // 숫자 9 키를 눌렀을 때 GetImage1 메소드 호출
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GetImage1();
        }
    }

    public void GetImage1()
    {
        StartCoroutine(GetImageRequest(url));
    }

    // 이미지 파일을 Get으로 받는 함수
    IEnumerator GetImageRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // 이미지를 다운로드하여 byte[]로 변환
                byte[] imageData = request.downloadHandler.data;

                // Photon RPC를 사용하여 모든 클라이언트에 이미지 적용 요청
                photonView.RPC("ApplyTexture", RpcTarget.AllBuffered, imageData);
            }
            else
            {
                Debug.LogError(request.error);
            }
        }
    }

    [PunRPC]
    void ApplyTexture(byte[] textureData)
    {
        // byte[]를 Texture2D로 변환
        Texture2D newTexture = new Texture2D(2, 2);
        newTexture.LoadImage(textureData);

        // Renderer의 material에 Texture2D를 적용
        if (targetRenderer != null)
        {
            // 기본 Material이 필요한 경우 사용할 수 있습니다.
            Material material = new Material(defaultMaterial);
            material.mainTexture = newTexture;
            targetRenderer.material = material;
        }
    }
}