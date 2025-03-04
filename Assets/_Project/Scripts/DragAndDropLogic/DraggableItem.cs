using System;
using _Project.Scripts.LevelSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.DragAndDropLogic
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class DraggableItem : EventTrigger // Базовая логика drag and drop 
    {
        private ScrollBoundaryHandler _scrollBoundaryHandler;
        private Rigidbody2D _rb;
        private Camera _camera;
        
        private Vector2 _offset;
        private Vector3 _originalScale;
        
        private bool _isDragging;
        private int _touchId = -1;
        private float _scaleUp = 1.5f;
        private int _offsetItem = 40;

        [Inject] public void Construct( ScrollBoundaryHandler scrollBoundaryHandler) // Zenject
        {
            _scrollBoundaryHandler = scrollBoundaryHandler;
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _originalScale = transform.localScale;
            _camera = Camera.main;
        }

        private void OnTriggerStay2D(Collider2D other) => _rb.bodyType = RigidbodyType2D.Kinematic; // Реакции на коллайдеры 
  
        private void OnTriggerExit2D(Collider2D other) => _rb.bodyType = RigidbodyType2D.Dynamic;
        
        private void Update()  
        {
            ObjectMovement();
        }

        private void ObjectMovement() // Движение объекта 
        {
            if (_isDragging)
            {
                Vector2 newPosition;
                if (Input.touchCount > 0 && _touchId >= 0 && _touchId < Input.touchCount)
                {
                    Touch touch = Input.GetTouch(_touchId);
                    newPosition = _camera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0f));
                    transform.position = newPosition - _offset;
                } 
                _scrollBoundaryHandler.ClampToBounds(transform);
                _scrollBoundaryHandler.ScrollIfNearEdge(transform);
            }
        }
    
        public override void OnPointerDown(PointerEventData eventData) // Нажали на объект 
        {
            _isDragging = true;
            transform.localScale = _originalScale * _scaleUp;

            float bottomY = GetComponent<Collider2D>().bounds.min.y;
            _offset = new Vector2(0, -transform.position.y + bottomY - _offsetItem);
        }

        public override void OnPointerUp(PointerEventData eventData) // Отпустили 
        {
            _isDragging = false;
            _touchId = -1;
            transform.localScale = _originalScale;
        }
    }
}
