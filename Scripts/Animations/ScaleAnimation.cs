using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Toolkit.Tweens.Animations
{
    public class ScaleAnimation : InOutTweenBehaviour
    {
        public Vector3 TargetScale = new Vector3(1, 1, 1);
        public Vector3 InitialScale = new Vector3(0, 0, 0);
        public float InDuration = 1;
        public float OutDuration = 1;
        public Ease InEase = Ease.Linear;
        public Ease OutEase = Ease.Linear;

        [SerializeField]
        private bool SameGameObjectWithTarget = true;
        [SerializeField, HideIf(nameof(SameGameObjectWithTarget))]
        private Transform _transform;

        public Transform Transform => _transform;

        private void Awake()
        {
            InitializeIfRequired();
        }

        private void OnValidate()
        {
            InitializeIfRequired();
        }

        public override Tween PlayIn()
        {
            InitializeIfRequired();

            return Transform.DOScale(TargetScale, InDuration)
                .From(InitialScale)
                .SetEase(InEase);
        }

        public override Tween PlayOut()
        {
            InitializeIfRequired();

            return Transform.DOScale(InitialScale, OutDuration)
                .From(TargetScale)
                .SetEase(OutEase);
        }

        private void InitializeIfRequired()
        {
            if (!Transform && SameGameObjectWithTarget)
                _transform = transform;
        }
    }
}