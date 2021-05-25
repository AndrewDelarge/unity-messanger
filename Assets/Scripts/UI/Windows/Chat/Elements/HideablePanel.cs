
using DG.Tweening;
using UnityEngine;

namespace UI.Windows.Chat.Elements
{
    public class HideablePanel : MonoBehaviour
    {
        [SerializeField] private float hideDuration = .25f;
        [SerializeField] private float showDuration = .25f;
        
        public virtual Sequence GetShowSequence()
        {
            var seq = DOTween.Sequence();

            seq.Append(transform.DOMoveY(0, showDuration));
            
            return seq;
        }
        
        public virtual Sequence GetHideSequence()
        {
            var seq = DOTween.Sequence();

            seq.Append(transform.DOMoveY(-200, hideDuration));
            
            return seq;
        }
    }
}