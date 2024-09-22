using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public GameObject CesiumMap;

    // Skybox Material 배열로 여러 Skybox를 관리할 수 있음
    public Material[] skyboxMaterials;

    // 시작할 때 기본으로 사용할 Skybox index
    public int defaultSkyboxIndex = 0;

    // 현재 Skybox index
    public int currentSkyboxIndex;

    // 활성화/비활성화할 게임 오브젝트들
    public GameObject cesiumWorldTerrain;
    public GameObject googlePhotorealistic3DTiles;

    void Start()
    {
        // 기본 Skybox 설정
        SetSkybox(defaultSkyboxIndex);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            print("Immersive_Statue of Liberty");
            SetSkybox(1);
        }

        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            print("Immersive_Colosseum");
            SetSkybox(2);
        }

        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            print("Immersive_Eiffel Tower");
            SetSkybox(3);
        }

    }

    // Skybox를 변경하는 함수
    public void SetSkybox(int index)
    {
        if (index >= 0 && index < skyboxMaterials.Length)
        {
            RenderSettings.skybox = skyboxMaterials[index];
            currentSkyboxIndex = index;
            Debug.Log("Skybox changed to: " + skyboxMaterials[index].name);

            // Skybox가 0일 때 Cesium 관련 오브젝트들을 활성화
            if (currentSkyboxIndex == 0)
            {
                ActivateObjects();
            }
            else
            {
                DeactivateObjects();
            }
        }
        else
        {
            Debug.LogError("Skybox index out of range!");
        }
    }

    // Skybox를 순환시키는 함수
    //public void NextSkybox()
    //{
    //    currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxMaterials.Length;
    //    SetSkybox(currentSkyboxIndex);
    //}

    //// Skybox를 이전 것으로 되돌리는 함수
    //public void PreviousSkybox()
    //{
    //    currentSkyboxIndex = (currentSkyboxIndex - 1 + skyboxMaterials.Length) % skyboxMaterials.Length;
    //    SetSkybox(currentSkyboxIndex);
    //}

    // 게임 오브젝트들을 활성화하는 함수
    public void ActivateObjects()
    {
        if (cesiumWorldTerrain != null)
        {
            cesiumWorldTerrain.SetActive(true);
        }

        if (googlePhotorealistic3DTiles != null)
        {
            googlePhotorealistic3DTiles.SetActive(true);
        }

        Debug.Log("Objects activated.");
    }

    // 게임 오브젝트들을 비활성화하는 함수
    public void DeactivateObjects()
    {
        if (cesiumWorldTerrain != null)
        {
            cesiumWorldTerrain.SetActive(false);
        }

        if (googlePhotorealistic3DTiles != null)
        {
            googlePhotorealistic3DTiles.SetActive(false);
        }

        Debug.Log("Objects deactivated.");
    }
}
