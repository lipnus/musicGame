#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("lQ/5IR1EpeYlVxU4UpTi+TDzEKMW9M6SZCSeoBDsDpHZSKV8XEwQGZYhbwgcBlHEAjd4lMnH5nohifP3XgWX7VmbWkGFimvnx/hMGPs+ZSW+YzkUylSKm4lDc1gJlXuXD4hFEKRUOnUwcSyg2FLf7ctQGe8FZAB2K1sRSydJ/fDlZ7cB9SN4Z2jotQ6KCQcIOIoJAgqKCQkIhaEptcanU+GhJAkoTp2GIy6C6yH/NfEDSSLru7T9J8+e1a1YgqoqdI3hkf15iL9Wpf7YIlDF6qNutJZD0ISlfoz4rKCx+f0YtA/F9zKtHPOa7DGFeBaeOIoJKjgFDgEijkCO/wUJCQkNCAvGy8npOLFzSpdPlc2WRgi156vkMzEbwPzh9ZQQiQoLCQgJ");
        private static int[] order = new int[] { 4,6,10,12,10,7,7,7,13,13,11,13,13,13,14 };
        private static int key = 8;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
