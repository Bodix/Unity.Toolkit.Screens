// Copyright © 2025 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Toolkit.Tweens.Animations
{
    public class GraphicFadeAnimation : InOutTweenBehaviour
    {
        public float TargetAlpha = 1;
        public float InitialAlpha = 0;
        public float InDuration = 1;
        public float OutDuration = 1;
        public Ease InEase = Ease.Linear;
        public Ease OutEase = Ease.Linear;

        [SerializeField, HideIf(nameof(SameGameObjectWithTarget))]
        private Graphic _graphic;

        public Graphic Graphic => _graphic;
        private bool SameGameObjectWithTarget => _graphic && _graphic.gameObject == gameObject;

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

            return Graphic.DOFade(TargetAlpha, InDuration)
                .From(InitialAlpha)
                .SetEase(InEase);
        }

        public override Tween PlayOut()
        {
            InitializeIfRequired();

            return Graphic.DOFade(InitialAlpha, OutDuration)
                .From(TargetAlpha)
                .SetEase(OutEase);
        }

        private void InitializeIfRequired()
        {
            if (!Graphic)
                _graphic = GetComponent<Graphic>();
        }
    }
}