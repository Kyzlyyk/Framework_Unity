using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Kyzlyk.LSGSystem.Breaking.Modules
{
    public class Supervisor : IWrapper
    {
        public Supervisor(Builder builder)
        {
            Builder = builder;
        }

        private List<IWrapper> _interactors = new();

        public Builder Builder { get; set; } 

        public void AddWrapper(IWrapper interactor)
        {
            if (_interactors.Any(i => i.GetType() == interactor.GetType())) return;

            _interactors.Add(interactor); 
            
            if (Builder != null)
                interactor.Builder = this.Builder;
        }

        public void RemoveWrapper(Type interactor)
        {
            _interactors = _interactors.Where(i => i.GetType() != interactor).ToList();
        }

        //TODO: Optimize for help job system.
        public void WrapGMaterial(Vector2Int position)
        {
            for (int i = 0; i < _interactors.Count; i++)
            {
                _interactors[i].WrapGMaterial(position);
            }
        }

        public void UnwrapGMaterial(Vector2Int position)
        {
            for (int i = 0; i < _interactors.Count; i++)
            {
                _interactors[i].UnwrapGMaterial(position);
            }
        }

        public void UnwrapAll()
        {
            for (int i = 0; i < _interactors.Count; i++)
            {
                _interactors[i].UnwrapAll();
            }
        }

        public void ApplyWrap()
        {
            for (int i = 0; i < _interactors.Count; i++)
            {
                _interactors[i].ApplyWrap();
            }
        }
    }
}