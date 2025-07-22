// Copyright © 2025 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using DG.Tweening;
using UnityEngine;

namespace Toolkit.Screens
{
    public abstract class InOutTweenBehaviour : MonoBehaviour
    {
        public abstract Tween PlayIn();

        public abstract Tween PlayOut();
    }
}