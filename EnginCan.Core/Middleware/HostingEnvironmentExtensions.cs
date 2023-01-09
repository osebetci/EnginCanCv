using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;

namespace EnginCan.Core.Middleware
{
    /// <summary>
    /// Extension methods for <see cref="IHostingEnvironment"/>.
    /// </summary>
    public static class HostingEnvironmentExtensions
    {
        /// <summary>
        /// Projenin bir üst patinde tutulan statik klasörünü ana path olarak geri döndürür. (Docs)
        /// </summary>
        public static string ContentRootPath(this IHostingEnvironment env)
        {
            return env.ContentRootPath.Replace(env
                           .ContentRootPath
                           .Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar })
                           .Last(), "Docs");
        }

        /// <summary>
        /// Görsellerin tutulduğu klasör yoludur
        /// </summary>
        public static string ContentImagePath(this IHostingEnvironment env) => Path.Combine(env.ContentRootPath(), "images");

        /// <summary>
        /// Belgelerin tutulduğu klasör yoludur
        /// </summary>
        public static string ContentDocumentPath(this IHostingEnvironment env) => Path.Combine(env.ContentRootPath(), "documents");

        /// <summary>
        /// Videoların tutulduğu klasör yoludur
        /// </summary>
        public static string ContentVideoPath(this IHostingEnvironment env) => Path.Combine(env.ContentRootPath(), "videos");

        /// <summary>
        /// Sistem başlangıcında ana içerik klasörlerini oluşturur
        /// </summary>
        public static void CreateContentFolders(this IHostingEnvironment env)
        {
            if (!Directory.Exists(env.ContentImagePath()))
            {
                Directory.CreateDirectory(env.ContentImagePath());
            }

            if (!Directory.Exists(env.ContentDocumentPath()))
            {
                Directory.CreateDirectory(env.ContentDocumentPath());
            }

            if (!Directory.Exists(env.ContentVideoPath()))
            {
                Directory.CreateDirectory(env.ContentVideoPath());
            }
        }
    }
}