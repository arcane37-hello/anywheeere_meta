using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScreen : MonoBehaviour
{
    private Vector3 dragStartPoint;
    private Vector3 dragStartPosition;
    private bool isDragging = false;

    public float moveSpeed = 1f; // �巡�� �ӵ� ����

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư�� ���� ���
        {
            StartDrag();
        }

        if (Input.GetMouseButton(0) && isDragging) // �巡�� ���� ��
        {
            Drag();
        }

        if (Input.GetMouseButtonUp(0)) // ���콺 ��ư�� �� ���
        {
            isDragging = false;
        }
    }

    void StartDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        dragStartPoint = ray.GetPoint(CalculateDistanceToDragPlane(ray)); // �巡�� ���� ��ġ
        dragStartPosition = transform.position; // �巡�� ���� �� ������Ʈ�� ��ġ
        isDragging = true;
    }

    void Drag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 dragCurrentPoint = ray.GetPoint(CalculateDistanceToDragPlane(ray));
        Vector3 difference = dragCurrentPoint - dragStartPoint;

        // �巡�� �ӵ��� �����ϱ� ���� ���̸� moveSpeed�� ���Ͽ� �̵�
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