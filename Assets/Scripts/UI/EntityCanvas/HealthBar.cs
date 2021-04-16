using System;
using Actors.Destructible;
using Core;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
namespace UI.EntityCanvas
{
    public class HealthBar : BaseMonoBehaviour
    {
        [Required] [SerializeField] private Image fill;
        [SerializeField] private float positionOffset;
        [SerializeField] private bool adjustOffset;
        [SerializeField] private bool adjustScale;
        [SerializeField][Range(0,0.15f)] private float valuePerUnit = 0.05f;
        [SerializeField][Range(0,2f)] private float startOffset = 0.8f;
        [SerializeField][Range(0,2f)] private float startScale = 1.5f;

        private bool _isUpdated;
        private bool _isInitialized;
        private IDestructible _destructible;

        private Canvas _c;
        private Canvas _canvas
        {
            get
            {
                if (_c == null) _c = GetComponent<Canvas>();
                return _c;
            }
        }
        
        protected override void ReleaseReferences()
        {
            if(_destructible != null) _destructible.OnTakeDamage -= UpdateProgression;
            _destructible = null;
            _c = null;
            fill = null;
        }
        
        public void Initialize()
        {
            if(_isInitialized) return;
            _isInitialized = true;
        }

        private void CheckProgression()
        {
            if(_destructible == null || _isUpdated) return;
            _isUpdated = true;
            UpdateProgression(_destructible, _destructible.GetGameObject.transform.position);
        }
        
        public void BindData(IDestructible inDestructible)
        {
            _destructible = inDestructible;
            _destructible.OnTakeDamage += UpdateProgression;
            fill.fillAmount = _destructible.Percentage;
        }

        public void ResetData()
        {
            _destructible.OnTakeDamage -= UpdateProgression;
            _destructible = null;
        }
        
        private void UpdateProgression(IDestructible inTarget, Vector3 inPosition)
        {
            if(_destructible == null || inTarget != _destructible) return;
            fill.fillAmount = _destructible.Percentage;
        }

        private void LateUpdate()
        {
            if(_destructible == null) return;
            if(!_isUpdated) CheckProgression();
            try
            {
                transform.position = GameController.MainCamera.WorldToScreenPoint(
                    _destructible.GetGameObject.transform.position + Vector3.up * positionOffset);
                
                UpdateView();
            }
            catch
            {
                _destructible = null;
                Destroy(gameObject);
            }
        }

        private void UpdateView()
        {
            var targetDepth = _destructible.GetGameObject.transform.position.z;
            if (adjustOffset) positionOffset = startOffset + (targetDepth * valuePerUnit);
            if (adjustScale)
            {
                var scale = startScale - (targetDepth * valuePerUnit);
                transform.localScale = new Vector3(scale, scale, scale);
            }
        }

        public void SetVisibility(bool inStatus)
        {
            _canvas.enabled = inStatus;
        }
    }
}
