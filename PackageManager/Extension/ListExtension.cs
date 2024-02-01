using PackageManager.Data;
using System;

namespace PackageManager.Extension
{
    public static class ListExtension
    {
        public static IList<OperationType> MixData(this IList<OperationType> data) 
        {
            var random = new Random();
            for (int i = data.Count() - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                // обменять значения data[j] и data[i]
                var temp = data[j];
                data[j] = data[i];
                data[i] = temp;
            }

            return data;
        }
    }
}
