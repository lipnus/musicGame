#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class UnityChannelTangle
    {
        private static byte[] data = System.Convert.FromBase64String("yOLRWIsQWrjdTrpcwvhd0pJfykoY0Ij/");
        private static int[] order = new int[] { 0,1 };
        private static int key = 37;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
