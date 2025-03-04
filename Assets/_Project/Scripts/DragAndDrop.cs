using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class DragAndDrop : EventTrigger
    {
        public Rigidbody2D rb;
        public Vector2 offset; 
        public bool Dragging; 
        void Start()
        {
            rb = GetComponent<Rigidbody2D>(); 
        }

        private void OnTriggerStay2D(Collider2D other)
        { 
            rb.isKinematic = true; 
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            rb.isKinematic = false;
        }

        private void Update()
        {
            if (Dragging)
            {
                transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - offset;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            Dragging = true;
            rb.isKinematic = true;  
            offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
        }
  
        public override void OnEndDrag(PointerEventData eventData)
        {
            Dragging = false; 
        }
    }
}
