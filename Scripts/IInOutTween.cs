using DG.Tweening;

namespace Toolkit.Screens
{
    public interface IInOutTween
    {
        Tween PlayIn();
        Tween PlayOut();
    }
}