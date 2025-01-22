using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MWTest
{
    public class BundleResourceProvider : MonoBehaviour, IResourceProvider
    {
        private readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

        private const string bundleName = "justbundle";
        private const string buttonName = "button";

        public event Action ResourcesLoaded;

        public Sprite GetSprite(string name)
        {
            if (sprites.ContainsKey(name))
            {
                return sprites[name];
            }

            return null;
        }

        public async UniTask LoadResources()
        {
            var bundle = await AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, bundleName));
            var sprite = (Sprite) await bundle.LoadAssetAsync<Sprite>(buttonName);
            sprites.Add(buttonName, sprite);
            ResourcesLoaded?.Invoke();
        }
    }
}
