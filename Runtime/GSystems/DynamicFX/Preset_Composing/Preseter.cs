using UnityEngine;

namespace Kyzlyk.GSystems.DynamicFX.PresetComposing
{
    public interface IPreseter
    {
        int Layer { get; }
        void ApplyPreset(PresetStyle style);
    }

    public abstract class Preseter<TTarget> : MonoBehaviour, IPreseter where TTarget : Component
    {
        [SerializeField] protected PresetWrapper PresetWrapper;

        protected TTarget Target;

        public int Layer => gameObject.layer;

        private void Awake()
        {
            Target = GetComponent<TTarget>();
        }
     
        public virtual void ApplyPreset(PresetStyle option)
        {
            PresetWrapper.Apply(Target, 1);
        }
    }
}