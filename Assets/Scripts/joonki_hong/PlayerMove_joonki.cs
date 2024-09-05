using UnityEngine;

public class PlayerMove_joonki : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        // 커서를 중앙에 고정하고 숨깁니다.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 입력을 받습니다.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 위아래(피치) 회전을 계산합니다.
        xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 시점이 뒤집히지 않도록 제한

        // 카메라 회전을 적용합니다.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
