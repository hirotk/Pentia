using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pentia.Utilities {
    public static class ImageLoader {
        private static Dictionary<String, WriteableBitmap> images = new Dictionary<string, WriteableBitmap>();

        public static bool IsRegistered(string key) {
            return images.ContainsKey(key);
        }

        public static void AddImage(string key, Uri src) {
            if (images.ContainsKey(key)) {
                throw new ArgumentException(key + " is already registerd.");
            }

            using (var ms = new MemoryStream(File.ReadAllBytes(src.OriginalString))) {
                var wbmp = new WriteableBitmap(BitmapFrame.Create(ms));
                images.Add(key, wbmp);
            }
        }

        public static void RemoveImage(string key) {
            if (images.ContainsKey(key)) {
                images.Remove(key);
            }
        }

        public static void LoadImage(string key, Rectangle rectangle) {
            if (images.ContainsKey(key) == false) {
                throw new ArgumentException(key + " is not registerd.");
            }

            rectangle.Width = images[key].PixelWidth;
            rectangle.Height = images[key].PixelHeight;
            var brush = new ImageBrush(images[key]);
            rectangle.Fill = brush;
        }    
    }
}
