using UnityEngine;

namespace _Project.Scripts
{
    public class ImageScroll : MonoBehaviour
    {
        public RectTransform imageTransform; // Ссылка на RectTransform изображения
        public float speed = 5f; // Скорость прокрутки
        public float minX = -500f; // Левая граница
        public float maxX = 500f;  // Правая граница
        private Vector3 lastMousePosition;
        private bool isDragging = false;

        void Update()
        {
            float input = Input.GetAxis("Horizontal"); // Ввод с клавиатуры
            if (input != 0)
            {
                MoveImage(input * speed * Time.deltaTime);
            }

            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;
                MoveImage(-delta.x * 0.5f); // Корректируем чувствительность
                lastMousePosition = Input.mousePosition;
            }
        }

        void MoveImage(float deltaX)
        {
            float newX = Mathf.Clamp(imageTransform.anchoredPosition.x + deltaX, minX, maxX);
            imageTransform.anchoredPosition = new Vector2(newX, imageTransform.anchoredPosition.y);
        }
    }

}
