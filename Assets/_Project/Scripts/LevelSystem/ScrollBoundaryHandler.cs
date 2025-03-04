using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.LevelSystem
{
    public class ScrollBoundaryHandler : MonoBehaviour
    {
        private const float EdgeThreshold = 200f;
        private const float ScrollSpeed = 0.5f;
        private ScrollRect _scrollRect;
        private  RectTransform _content;
        private  RectTransform _canvasRect;
        
        [Inject] public void Construct(ScrollRect scrollRect, RectTransform content)
        {
            _scrollRect = scrollRect;
            _content = content;
            _canvasRect = scrollRect.GetComponent<RectTransform>();
        }

        public void ClampToBounds(Transform item)
        {
            Vector3[] corners = new Vector3[4];
            _content.GetWorldCorners(corners);

            float leftLimit = corners[0].x;
            float rightLimit = corners[2].x;
            float bottomLimit = corners[0].y;
            float topLimit = corners[1].y;

            Vector3 newPosition = item.position;
            newPosition.x = Mathf.Clamp(newPosition.x, leftLimit + 150f, rightLimit - 150f);
            newPosition.y = Mathf.Clamp(newPosition.y, bottomLimit + 150f, topLimit - 150f);

            item.position = newPosition;
        }

        public void ScrollIfNearEdge(Transform item)
        {
            Vector3[] corners = new Vector3[4];
            _canvasRect.GetWorldCorners(corners);

            float leftEdge = corners[0].x + EdgeThreshold;
            float rightEdge = corners[2].x - EdgeThreshold;

            Vector3 worldPos = item.position;

            if (worldPos.x < leftEdge && _scrollRect.horizontalNormalizedPosition > 0)
            {
                _scrollRect.horizontalNormalizedPosition -= ScrollSpeed * Time.deltaTime;
            }
            else if (worldPos.x > rightEdge && _scrollRect.horizontalNormalizedPosition < 1)
            {
                _scrollRect.horizontalNormalizedPosition += ScrollSpeed * Time.deltaTime;
            }
        }
    }
}
