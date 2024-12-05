using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public GameObject CesiumMap;

    // Skybox Material �迭�� ���� Skybox�� ������ �� ����
    public Material[] skyboxMaterials;

    // ������ �� �⺻���� ����� Skybox index
    public int defaultSkyboxIndex = 0;

    // ���� Skybox index
    public int currentSkyboxIndex;

    // Ȱ��ȭ/��Ȱ��ȭ�� ���� ������Ʈ��
    public GameObject cesiumWorldTerrain;
    public GameObject googlePhotorealistic3DTiles;

    void Start()
    {
        // �⺻ Skybox ����
        SetSkybox(defaultSkyboxIndex);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            print("TokyoTower");
            SetSkybox(1);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            print("St.Peter's Bascilica");
            SetSkybox(2);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            print("Pantheon");
            SetSkybox(3);
        }

        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            print("Default");
            SetSkybox(0);
        }

    }

    // Skybox�� �����ϴ� �Լ�
    public void SetSkybox(int index)
    {
        if (index >= 0 && index < skyboxMaterials.Length)
        {
            RenderSettings.skybox = skyboxMaterials[index];
            currentSkyboxIndex = index;
            Debug.Log("Skybox changed to: " + skyboxMaterials[index].name);

            // Skybox�� 0�� �� Cesium ���� ������Ʈ���� Ȱ��ȭ
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

    // Skybox�� ��ȯ��Ű�� �Լ�
    //public void NextSkybox()
    //{
    //    currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxMaterials.Length;
    //    SetSkybox(currentSkyboxIndex);
    //}

    //// Skybox�� ���� ������ �ǵ����� �Լ�
    //public void PreviousSkybox()
    //{
    //    currentSkyboxIndex = (currentSkyboxIndex - 1 + skyboxMaterials.Length) % skyboxMaterials.Length;
    //    SetSkybox(currentSkyboxIndex);
    //}

    // ���� ������Ʈ���� Ȱ��ȭ�ϴ� �Լ�
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

    // ���� ������Ʈ���� ��Ȱ��ȭ�ϴ� �Լ�
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
