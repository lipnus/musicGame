#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("5a8dnumvkZmcyoKQnp5gm5ucnZ7v8/q/3Prt6/b59vz+6/bw8b/e6vP6v9bx/LGuua+7mZzKm5SMgt7v5r/+7Ozq8vrsv/78/Prv6/7x/Prv8/q/zfDw67/c3q+BiJKvqa+rrb/+8fu//Prt6/b59vz+6/bw8b/vr46ZnMqblYyV3u/v8/q/1vH8sa6ZnMqCkZuJm4u0T/bYC+mWYWv0ErO//Prt6/b59vz+6/q/7/Dz9vzml7SZnpqamJ2eiYH36+vv7KWwsOi1GdcZaJKenpqan6/9rpSvlpmcyujosf7v7/P6sfzw8rD+7+/z+vz+IWvsBHFN+5BU5tCrRz2hZudg9FeQAqJstNa3hVdhUSomkUbBg0lUoqypxa/9rpSvlpmcypuZjJ3KzK6MFIYWQWbU82qYNL2vnXeHoWfPlkyXwa8dno6ZnMqCv5sdnpevHZ6br6K5+L8VrPVokh1QQXQ8sGbM9cT7/fP6v+zr/vH7/u37v+v67fLsv/6Jr4uZnMqbnIyS3u/v8/q/zfDw6+v38O326+auia+LmZzKm5yMkt7v4N43B2ZOVfkDu/SOTzwke4S1XIA3Q+G9qlW6SkaQSfRLPbu8jmg+Mx+LtE/22AvplmFr9BKx3zlo2NLgNDzuDdjMyl4wsN4sZ2R871J5PNNWhu1qwpFK4MAEbbqcJcoQ0sKSbuv2+fb8/uv6v/3mv/7x5r/v/u3r1kfpAKyL+j7oC1aynZyen548HZ72+fb8/uv28PG/3urr9/Dt9uvmriqlMmuQkZ8NlC6+ibHrSqOSRP2JRqngXhjKRjgGJq3dZEdK7gHhPs2Zr5CZnMqCjJ6eYJuar5yenmCvgh2en5mWtRnXGWj8+5qerx5tr7WZ+6q8itSKxoIsC2hpAwFQzyVex8/GOJqW44jfyY6B60woFLyk2DxK8JuZjJ3KzK6Mr46ZnMqblYyV3u/vv/D5v+v3+r/r9/rxv/7v7/P2/P67fXROKO9AkNp+uFVu8udyeCqIiIAORIHYz3SacsHmG7J0qT3I08pzqq2uq6+sqcWIkqyqr62vpq2uq6+vHZskrx2cPD+cnZ6dnZ6dr5KZlvH7v/zw8fv26/bw8ey/8Pm/6uz6KIQiDN27jbVYkIIp0gPB/FfUH4ix3zlo2NLgl8GvgJmcyoK8m4eviRDsHv9ZhMSWsA0tZ9vXb/+nAYpqzfrz9v7x/Pq/8PG/6/f27L/8+u2AGhwahAai2KhtNgTfEbNLLg+NR7CvHlyZl7SZnpqamJ2drx4phR4sqQbTsucochMEQ2zoBG3pTeiv0F7t/vzr9vz6v+zr/uv68vrx6+yxr7mvu5mcypuUjILe7+/z+r/c+u3rmp+cHZ6Qn68dnpWdHZ6en3sONpYur8dzxZutE/csEIJB+uxg+MH6I7/c3q8dnr2vkpmWtRnXGWiSnp6eX/ys6GilmLPJdEWQvpFFJeyG0CqSmZa1GdcZaJKenpqan5wdnp6fwwoB5ZM72BTES4morFRbkNJRi/ZO2uGA0/TPCd4WW+v9lI8c3hisFR74EJcrv2hUM7O/8O8poJ6vEyjcUJhz4qYcFMy/TKdbLiAF0JX0YLRjzzUVSkV7Y0+WmKgv6uq+");
        private static int[] order = new int[] { 28,4,3,40,6,45,9,30,44,35,35,36,50,22,44,16,33,56,42,50,45,56,54,32,24,50,39,27,36,32,58,32,34,45,52,40,41,53,45,58,41,43,59,52,55,45,49,53,48,49,52,55,58,54,57,57,59,58,59,59,60 };
        private static int key = 159;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
