using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinImage
{
    public static class RowAnalyzer
    {
        public static List<double> Autocorrelation(double[] array)
        {
            var funcAutocorrelation = new List<double>();

            for (int i = 1; i < array.Length / 4; i++)
            {
                var lag = GetLag(array, i);
                double[] shortArray = new double[array.Length - i];
                Array.Copy(array, shortArray, array.Length - i);
                funcAutocorrelation.Add(Correlation(shortArray, lag));
            }

            return funcAutocorrelation;
        }
        public static double Correlation(double[] array1, double[] array2)
        {
            var arrayAverage1 = array1.Average();
            var arrayAverage2 = array2.Average();
            array1.Select(x => x - arrayAverage1);
            var topArray = new double[array1.Length];
            for (int i = 0; i < array1.Length; i++)
                topArray[i] = ((array1[i] - arrayAverage1) * (array2[i] - arrayAverage2));
            var topValue = topArray.Sum();
            var downLeft = array1.Select(x => Math.Pow(x - arrayAverage1, 2)).ToArray();
            var downRight = array2.Select(x => Math.Pow(x - arrayAverage2, 2)).ToArray();
            return topValue / Math.Sqrt(downLeft.Sum() * downRight.Sum());
        }
        public static double[] GetLag(double[] array, int number)
        {
            double[] result = new double[array.Length - number];
            Array.Copy(array, number, result, 0, array.Length - number);

            return result;
        }
    }
}
