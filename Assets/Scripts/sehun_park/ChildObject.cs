using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObject : MonoBehaviour
{
    // �������� �巡���Ͽ� �Ҵ��մϴ�.
    public GameObject prefabToAttach;

    // Start�� ���� ���� �� �� �� ȣ��˴ϴ�.
    void Start()
    {
        if (prefabToAttach != null)
        {
            // �������� �ν��Ͻ�ȭ�մϴ�.
            GameObject instance = Instantiate(prefabToAttach);

            // �ν��Ͻ��� ���� ��ũ��Ʈ�� ������ ���� ������Ʈ�� �ڽ����� �����մϴ�.
            instance.transform.SetParent(transform);

            // �ڽ� ������Ʈ�� ��ġ�� ���� �θ� ������Ʈ�� ��ġ�� ��������� ���ߵ��� �����մϴ�.
            instance.transform.localPosition = new Vector3(0, 0, 6);
            instance.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("prefabToAttach�� �Ҵ���� �ʾҽ��ϴ�. Inspector���� �������� �Ҵ����ּ���.");
        }
    }
}