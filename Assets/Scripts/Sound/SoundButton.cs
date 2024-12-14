using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound; // Звук клика

    private void Start()
    {
        // Получаем компонент кнопки и подписываемся на событие OnClick
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        // Воспроизводим звук клика
        if (clickSound != null)
        {
            // Воспроизводим звук без необходимости добавлять AudioSource вручную
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        }
    }
}
