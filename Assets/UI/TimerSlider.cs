using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerSlider : MonoBehaviour
{
    public Slider timerSlider; // ������ Slider
    public float duration = 60f; // Ÿ�̸� �ð� (1��)
    private float elapsedTime = 0f;

    void Start()
    {
        // Slider �ʱ�ȭ
        timerSlider.maxValue = duration;
        timerSlider.value = duration;

        // 3�� �Ŀ� Ÿ�̸� ����
        StartCoroutine(StartTimerAfterDelay(5f));
    }

    IEnumerator StartTimerAfterDelay(float delay)
    {
        // 3�� ���
        yield return new WaitForSeconds(delay);

        // Ÿ�̸� ����
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // ��� �ð� ���
            timerSlider.value = duration - elapsedTime; // ���� �ð� ������Ʈ
            yield return null; // ���� �����ӱ��� ���
        }

        TimerEnd(); // Ÿ�̸� ���� ó��
    }

    void TimerEnd()
    {
        Debug.Log("Ÿ�̸� ����!");
    }
}
