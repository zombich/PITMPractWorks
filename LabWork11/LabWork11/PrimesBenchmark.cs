using BenchmarkDotNet.Attributes;

namespace LabWork11
{
    [MemoryDiagnoser]
    public class PrimesBenchmark
    {
        [Params(10_000, 50_000, 100_000)]
        public int N;

        private bool IsPrimeNaive(int n)
        {
            for (int i = 2; i < n; i++)
                if (n % i == 0) return false;
            return true;
        }

        private bool IsPrimeFast(int n)
        {
            if (n < 2) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;
            int sqrt = (int)Math.Sqrt(n);
            for (int i = 3; i <= sqrt; i += 2)
                if (n % i == 0) return false;
            return true;
        }

        [Benchmark(Baseline = true)]
        public int GetNumbersSumNaive()
        {
            int sum = 0;

            for (int i = 2; i <= N; i++)
                if (IsPrimeNaive(i))
                    sum += i;

            return sum;
        }

        [Benchmark]
        public int GetNumbersSumFast()
        {
            int sum = 0;

            for (int i = 2; i <= N; i++)
                if (IsPrimeFast(i))
                    sum += i;

            return sum;
        }
    }
}
