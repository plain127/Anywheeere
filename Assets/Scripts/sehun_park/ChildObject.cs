using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObject : MonoBehaviour
{
    // 프리팹을 드래그하여 할당합니다.
    public GameObject prefabToAttach;

    // Start는 게임 시작 시 한 번 호출됩니다.
    void Start()
    {
        if (prefabToAttach != null)
        {
            // 프리팹을 인스턴스화합니다.
            GameObject instance = Instantiate(prefabToAttach);

            // 인스턴스를 현재 스크립트가 부착된 게임 오브젝트의 자식으로 설정합니다.
            instance.transform.SetParent(transform);

            // 자식 오브젝트의 위치를 현재 부모 오브젝트의 위치에 상대적으로 맞추도록 설정합니다.
            instance.transform.localPosition = new Vector3(0, 0, 6);
            instance.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("prefabToAttach가 할당되지 않았습니다. Inspector에서 프리팹을 할당해주세요.");
        }
    }
}