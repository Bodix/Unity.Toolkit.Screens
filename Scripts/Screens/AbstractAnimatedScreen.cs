// Copyright © 2025 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using DG.Tweening;

namespace Toolkit.Tweens.Screens
{
    public abstract class AbstractAnimatedScreen : AbstractScreen
    {
        public abstract Tween ShowTween { get; }

        public abstract Tween HideTween { get; }

        public override Tween Push()
        {
            return ScreenStack.Push(this);
        }

        public override Tween Pop()
        {
            return ScreenStack.Pop(this);
        }

        public new Tween PushImmediately()
        {
            return ScreenStack.PushImmediately(this);
        }

        public new Tween PopImmediately()
        {
            return ScreenStack.PopImmediately(this);
        }
    }
}