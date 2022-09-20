using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FurnitureApp.Utility
{
    public class DirectoryCreator
    {
        /// <summary>
        /// ディレクトリをコピーする
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="overwrite"></param>
        /// <param name="copySubDirectory"></param>
        public static void Copy(string sourcePath,
                                string destinationPath,
                                bool overwrite = true,
                                bool copySubDirectory = true)
        {
            var sourceDirectory = new DirectoryInfo(sourcePath);
            var destinationDirectory = new DirectoryInfo(destinationPath);

            if (!sourceDirectory.Exists)
            {
                throw new DirectoryNotFoundException($"コピーするディレクトリが存在しません。: {sourcePath}");
            }

            if (!destinationDirectory.Exists)
            {
                destinationDirectory.Create();
                destinationDirectory.Attributes = sourceDirectory.Attributes;
            }

            foreach (var file in sourceDirectory.GetFiles())
            {
                file.CopyTo(Path.Combine(destinationDirectory.FullName, file.Name), overwrite);
            }

            if (copySubDirectory)
            {
                foreach (var directory in sourceDirectory.GetDirectories())
                {
                    Copy(directory.FullName,
                         Path.Combine(destinationDirectory.FullName, directory.Name),
                         overwrite,
                         copySubDirectory
                         );
                }
            }
        }
        /// <summary>
        /// ディレクトリを作成する
        /// 存在する場合は何もしない
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void CreateSafely(string directoryPath)
        {
            var directory = new DirectoryInfo(directoryPath);
            if (!directory.Exists)
            {
                directory.Create();
            }

        }
    }
}
