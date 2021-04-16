using Actors.Destructible;
using Core;
using Interfaces;
using Models;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Actors.Player
{
    public class PlayerController : GameSystem, ITick
    {
        public Text debug;//screen debugger

        [BoxGroup("Establishing")] [SerializeField] [Required] [Expandable]
        private PlayerSettings settings;

        [BoxGroup("Internal References")] [SerializeField] [Required]
        private PlayerRotation touchAndRotation;

        [BoxGroup("Internal References")] [SerializeField] [Required]
        private PlayerAnimController animController;

        [BoxGroup("Internal References")] [SerializeField] [Required]
        private new Camera camera;

        [BoxGroup("Internal References")] [SerializeField] [Required]
        private LaserBeam laserBeam;

        [BoxGroup("Internal References")] [SerializeField] [Required]
        private Animator animator;
        
        public Camera MainCamera => camera;
        
        //- private variables
        private bool _isMouseDown;
        private IDestructible _currentTarget;
        
        protected override void ReleaseReferences()
        {
            _currentTarget = null;
            settings = null;
            camera = null;
            touchAndRotation = null;
            animController = null;
            laserBeam = null;
            animator = null;
        }

        public override void Initialize()
        {
            //- init
            _currentTarget = null;
            _isMouseDown = false;
            animController = new PlayerAnimController(animator, camera, settings);
            touchAndRotation.Initialize(settings, camera.transform);
            laserBeam.Initialize();

            //- subscribe to events
            touchAndRotation.onFingerDown += FingerDown;
            touchAndRotation.onFingerUp += FingerUp;
            touchAndRotation.onMove += OnMove;
        }

        private void OnMove()
        {
            if (debug != null) debug.text = "\nOnMove";
        }

        private void FingerDown()
        {
            if (debug != null) debug.text = string.Empty;
            _isMouseDown = true;
        }

        private void FingerUp()
        {
            _isMouseDown = false;
            touchAndRotation.Stop();
        }

        public void Tick(float deltaTime)
        {
            if(!isRun) return;
            touchAndRotation.Tick();
            animController.Tick(deltaTime);
            Shooting();
        }

        private void Shooting()
        {
            if (_isMouseDown)
            {
                animController.SetAnimation(TreeStates.Aim);
                var screenRay = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 1));
                if (Physics.Raycast(screenRay.origin, screenRay.direction, out var hit, settings.laserMaxLength))
                {
                    if (hit.collider) 
                    {
                        if (debug != null) debug.text = $"\n Hit [{hit.collider.gameObject.name}]";
                        var destructible = hit.collider.gameObject.GetComponent<IDestructible>();
                        if (destructible == null || destructible.IsMelted)
                        {
                            laserBeam.Fire(hit.point);
                            NoDetection();
                            return;
                        }
                        OnDetectTarget(destructible);
                        destructible.ApplyDamage(settings.damage);
                        laserBeam.Fire(hit.point);
                    }
                    else 
                    {
                        NoDetection();
                    }
                }
                else
                {
                    NoDetection();
                    var pos = screenRay.GetPoint(settings.laserMaxLength);
                    laserBeam.Fire(pos);
                }
            }
            else
            {
                animController.SetAnimation(TreeStates.Idle);
                laserBeam.Stop();
                NoDetection();
            }
        }

        private void OnDetectTarget(IDestructible inDestructible)
        {
            if(_currentTarget == inDestructible) return;
            //-Do something when player aimEnter on enemy
        }

        private void NoDetection()
        {
            _currentTarget = null;
        }
    }
}