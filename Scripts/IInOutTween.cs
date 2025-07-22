// Copyright © 2025 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using DG.Tweening;

namespace Toolkit.Screens
{
    public interface IInOutTween
    {
        Tween PlayIn();
        Tween PlayOut();
    }
}