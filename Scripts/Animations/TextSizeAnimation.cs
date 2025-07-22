// Copyright © 2025 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Toolkit.Screens.Animations
{
    public class TextSizeAnimation : InOutTweenBehaviour
    {
        public float TargetSize = 36;
        public float InitialSize = 24;
        public float Duration = 1;
        public Ease Ease = Ease.Linear;

        [SerializeField, HideIf(nameof(SameGameObjectWithTarget))]
        private TMP_Text _text;

        public TMP_Text Text => _text;
        private bool SameGameObjectWithTarget => _text && _text.gameObject == gameObject;

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

            return DOVirtual.Float(InitialSize, TargetSize, Duration, value => Text.fontSize = value)
                .SetEase(Ease);
        }

        public override Tween PlayOut()
        {
            InitializeIfRequired();

            return DOVirtual.Float(TargetSize, InitialSize, Duration, value => Text.fontSize = value)
                .SetEase(Ease);
        }

        private void InitializeIfRequired()
        {
            if (!Text)
                _text = GetComponent<TMP_Text>();
        }
    }
}