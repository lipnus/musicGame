#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Z7rgzRONU0JQmqqB0EyiTtZRnMlM1iD4xJ18P/yOzOGLTTsg6SrJeh8SEDDhaKqTTpZMFE+f0Ww+cj3qzy0XS739R3nJNddIAJF8pYWVycBibST+FkcMdIFbc/OtVDhIJKBRZofcTjSAQoOYXFOyPh4hlcEi57z8eWggJMFt1hwu63TFKkM16Fyhz0d9jeOs6aj1eQGLBjQSicA23L3Zr498JwH7iRwzerdtT5oJXXynVSF18oLIkv6QJCk8vm7YLPqhvrExbNfhU9Dz4dzX2PtXmVcm3NDQ0NTR0lPQ3tHhU9Db01PQ0NFcePBsH36KOHj90PGXRF/691sy+CbsKNqQ+zJP+LbRxd+IHdvuoU0QHj+j+FAqLujCGSU4LE3JUNPS0NHQ");
        private static int[] order = new int[] { 2,4,3,6,8,12,9,7,11,12,12,11,13,13,14 };
        private static int key = 209;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
