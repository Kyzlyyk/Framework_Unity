using UnityEngine;

namespace Kyzlyk.LifeSupportModules.Breaking.Modules
{
    public interface IWrapper 
    {
        Builder Builder { get; set; }

        void WrapGMaterial(Vector2Int position);
        void UnwrapGMaterial(Vector2Int position);

        void UnwrapAll();
        void ApplyWrap();
    }
}