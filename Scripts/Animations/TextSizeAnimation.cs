// Copyright © 2025 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Toolkit.Tweens.Animations
{
    public class TextSizeAnimation : InOutTweenBehaviour
    {
        public float TargetSize = 36;
        public float InitialSize = 24;
        public float InDuration = 1;
        public float OutDuration = 1;
        public Ease EaseIn = Ease.Linear;
        public Ease EaseOut = Ease.Linear;

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

            return DOVirtual.Float(InitialSize, TargetSize, InDuration, value => Text.fontSize = value)
                .SetEase(EaseIn);
        }

        public override Tween PlayOut()
        {
            InitializeIfRequired();

            return DOVirtual.Float(TargetSize, InitialSize, OutDuration, value => Text.fontSize = value)
                .SetEase(EaseOut);
        }

        private void InitializeIfRequired()
        {
            if (!Text)
                _text = GetComponent<TMP_Text>();
        }
    }
}