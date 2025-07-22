// Copyright © 2025 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Toolkit.Screens.Animations
{
    public class GraphicFadeAnimation : InOutTweenBehaviour
    {
        public float Alpha = 1f;
        public float Duration = 0.5f;
        public Ease Ease = Ease.Linear;

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

            return Graphic.DOFade(Alpha, Duration)
                .From(0)
                .SetEase(Ease);
        }

        public override Tween PlayOut()
        {
            InitializeIfRequired();

            return Graphic.DOFade(0, Duration)
                .From(Alpha)
                .SetEase(Ease);
        }

        private void InitializeIfRequired()
        {
            if (!Graphic)
                _graphic = GetComponent<Graphic>();
        }
    }
}