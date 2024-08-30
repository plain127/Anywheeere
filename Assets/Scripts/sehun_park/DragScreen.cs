using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScreen : MonoBehaviour
{

    private Vector3 dragStartPoint;
    private Vector3 dragStartPosition;
    private bool isDragging = false;

    public float moveSpeed = 5;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 누른 경우
        {
            print("왼쪽 눌러짐");
            StartDrag();
        }

        if (Input.GetMouseButton(0) && isDragging) // 드래그 중일 때
        {
            print("드래그 중");
            Drag();
        }

        if (Input.GetMouseButtonUp(0)) // 마우스 버튼을 뗀 경우
        {
            print("드래그 끝");
            isDragging = false;
        }
    }

    void StartDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 오브젝트의 위치를 기준으로 드래그 시작 위치를 계산합니다.
        dragStartPoint = ray.GetPoint(CalculateDistanceToDragPlane(ray)); // 드래그 시작 위치
        dragStartPosition = transform.position; // 드래그 시작 시 오브젝트의 위치
        isDragging = true;
    }

    void Drag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 현재 마우스 위치를 기준으로 드래그 위치를 계산합니다.
        Vector3 dragCurrentPoint = ray.GetPoint(CalculateDistanceToDragPlane(ray));
        Vector3 difference = dragCurrentPoint - dragStartPoint;
        print(difference);

        // 오브젝트의 새 위치를 계산하여 이동
        transform.position = dragStartPosition + difference * moveSpeed;

        // 드래그 시작 지점을 업데이트하여 지속적인 이동 가능
        dragStartPoint = dragCurrentPoint;

        print("dragStartPoint" + dragStartPoint);
        print("dragCurrentPoint" + dragCurrentPoint);
    }

    // 드래그가 가능한 평면까지의 거리를 계산합니다.
    float CalculateDistanceToDragPlane(Ray ray)
    {
        // 드래그를 위한 평면을 정의합니다. (여기서는 간단히 Y=0으로 설정)
        Plane dragPlane = new Plane(Vector3.up, Vector3.zero);
        float enter;
        if (dragPlane.Raycast(ray, out enter))
        {
            return enter;
        }
        return 0;
    }
}