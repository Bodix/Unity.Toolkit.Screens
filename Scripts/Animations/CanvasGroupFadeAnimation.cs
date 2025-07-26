// Copyright © 2025 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Toolkit.Tweens.Animations
{
    public class CanvasGroupFadeAnimation : InOutTweenBehaviour
    {
        public float TargetAlpha = 1;
        public float InitialAlpha = 0;
        public float InDuration = 1;
        public float OutDuration = 1;
        public Ease InEase = Ease.Linear;
        public Ease OutEase = Ease.Linear;

        [SerializeField, HideIf(nameof(SameGameObjectWithTarget))]
        private CanvasGroup _canvasGroup;

        public CanvasGroup CanvasGroup => _canvasGroup;
        private bool SameGameObjectWithTarget => _canvasGroup && _canvasGroup.gameObject == gameObject;

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

            return CanvasGroup.DOFade(TargetAlpha, InDuration)
                .From(InitialAlpha)
                .SetEase(InEase);
        }

        public override Tween PlayOut()
        {
            InitializeIfRequired();

            return CanvasGroup.DOFade(InitialAlpha, OutDuration)
                .From(TargetAlpha)
                .SetEase(OutEase);
        }

        private void InitializeIfRequired()
        {
            if (!CanvasGroup)
                _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}