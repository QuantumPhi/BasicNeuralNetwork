using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicNeuralNetwork
{
    delegate void Print(int i);
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class PerceptronNetwork
    {
        public readonly int Inputs;

        double[] weights;

        public double this[int index]
        {
            get { return weights[index]; }
            set { weights[index] = value; }
        }
        public PerceptronNetwork(int size)
        {
            weights = new double[size];
            Inputs = size;
        }

        public double Run(double[] input)
        {
            double data = Enumerable
                .Range(0, weights.Length)
                .AsParallel()
                .Select(x => weights[x] * input[x])
                .Sum();

            return data > 0 ? 1 : 0;
        }
    }

    class PerceptronTeacher
    {
        public PerceptronNetwork Network { get; private set; }

        double Alpha { get; private set; }

        public PerceptronTeacher(PerceptronNetwork network, double alpha)
        {
            Network = network;
            Alpha = alpha;
        }

        public double Teach(double[] input, double output)
        {
            double sum = 0;

            double error = output - Network.Run(input);

            for (int i = 0; i < Network.Inputs; i++)
            {
                Network[i] += Alpha * error * input[i];
                sum += error;
            }
            return sum;
        }
    }
}
