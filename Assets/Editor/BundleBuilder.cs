using System.IO;
using UnityEditor;
using UnityEngine;

namespace MWTest
{
    public class BundleBuilder : MonoBehaviour
    {
        [MenuItem("Assets/Build AssetBundles/Windows")]
        static void BuildAllAssetBundlesW()
        {
            string assetBundleDirectory = "Assets/AssetBundles";
            if (!Directory.Exists(assetBundleDirectory))
                Directory.CreateDirectory(assetBundleDirectory);

            BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                            BuildAssetBundleOptions.None,
                                            BuildTarget.StandaloneWindows);
        }

        [MenuItem("Assets/Build AssetBundles/Android")]
        static void BuildAllAssetBundlesA()
        {
            string assetBundleDirectory = "Assets/AssetBundles";
            if (!Directory.Exists(assetBundleDirectory))
                Directory.CreateDirectory(assetBundleDirectory);

            BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                            BuildAssetBundleOptions.None,
                                            BuildTarget.Android);
        }
    }
}
