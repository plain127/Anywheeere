using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScreen : MonoBehaviour
{
    private Vector3 dragStartPoint;
    private Vector3 dragStartPosition;
    private bool isDragging = false;

    public float moveSpeed = 1f; // 드래그 속도 조절

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 누른 경우
        {
            StartDrag();
        }

        if (Input.GetMouseButton(0) && isDragging) // 드래그 중일 때
        {
            Drag();
        }

        if (Input.GetMouseButtonUp(0)) // 마우스 버튼을 뗀 경우
        {
            isDragging = false;
        }
    }

    void StartDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        dragStartPoint = ray.GetPoint(CalculateDistanceToDragPlane(ray)); // 드래그 시작 위치
        dragStartPosition = transform.position; // 드래그 시작 시 오브젝트의 위치
        isDragging = true;
    }

    void Drag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 dragCurrentPoint = ray.GetPoint(CalculateDistanceToDragPlane(ray));
        Vector3 difference = dragCurrentPoint - dragStartPoint;

        // 드래그 속도를 조절하기 위해 차이를 moveSpeed로 곱하여 이동
        transform.position = dragStartPosition + difference * moveSpeed;
    }

    float CalculateDistanceToDragPlane(Ray ray)
    {
        Plane dragPlane = new Plane(Vector3.up, Vector3.zero);
        float enter;
        if (dragPlane.Raycast(ray, out enter))
        {
            return enter;
        }
        return 0;
    }
}