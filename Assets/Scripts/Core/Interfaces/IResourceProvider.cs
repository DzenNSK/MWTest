using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace MWTest
{
    public interface IResourceProvider
    {
        public Sprite GetSprite(string name);
        public UniTask LoadResources();
        public event Action ResourcesLoaded;
    }
}
