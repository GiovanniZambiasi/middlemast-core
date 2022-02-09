using System;
using System.IO;
using UnityEngine.SceneManagement;

namespace MiddleMast
{
    public static class SceneManagerExtensions
    {
        public static string GetSceneNameFromBuildIndex(int index)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(index);

            return Path.GetFileNameWithoutExtension(path);
        }

        public static int GetSceneBuildIndexFromName(string name)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scene = GetSceneNameFromBuildIndex(i);

                if (scene != name)
                {
                    continue;
                }

                return i;
            }

            throw new Exception("Scene " + name + " not found!");
        }
    }
}
