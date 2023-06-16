using UnityEngine;
using Kyzlyk.Attributes;
using Kyzlyk.LSGSystem.Layout.Composing;

namespace Kyzlyk.LSGSystem.Layout
{
    public sealed class GLayout : MonoBehaviour
    {
		[SerializeField] private GSpace _space;

		[Header("Entity Build")]
        [RequireInterface(typeof(IEntityConstructor))]
        [SerializeField] private Object _entityConstructor;

        [Header("Power-Up Build")]
        [RequireInterface(typeof(IPowerUpConstructor))]
        [SerializeField] private Object _powerUpConstructor;
        
		[Header("Interactive Object Build")]
        [RequireInterface(typeof(IInteractiveObjectConstructor))]
        [SerializeField] private Object _interactiveObjectConstructor;
		
		[Header("Affector Build")]
        [RequireInterface(typeof(IAffectorConstructor))]
        [SerializeField] private Object _affectorConstructor;

        private IEntityConstructor EntityConstructor => _entityConstructor as IEntityConstructor;
		private IPowerUpConstructor PowerUpConstructor => _powerUpConstructor as IPowerUpConstructor;
		private IInteractiveObjectConstructor InteractiveConstructor => _interactiveObjectConstructor as IInteractiveObjectConstructor;
		private IAffectorConstructor AffectorConstructor => _affectorConstructor as IAffectorConstructor;

		//TODO: Make customizable options from inspector.
        public void Assemble()
		{
			if (_entityConstructor != null)
			{
				EntityConstructor.SpawnEntity(0);
			}

			if (_powerUpConstructor != null)
			{
				PowerUpConstructor
					.AddBuffs()
					.AddDebuffs();
			}

			if (_interactiveObjectConstructor != null)
			{
				InteractiveConstructor
					.Add();
			}

			if (_affectorConstructor != null)
			{
				AffectorConstructor?
					.Add();
			}

			DrawSpace();
        }

		private void DrawSpace()
		{
            if (_space != null)
            {
                _space.Draw();
            }
            else
            {
                Application.Quit();
                throw new System.Exception("Space is null! Scene will not load!");
            }
        }
    }
}