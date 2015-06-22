using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicNeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] input = new double[][]
            {
                new double[]{1,1,0,1,1,0,1,0,1},
                new double[]{0,0,1,0,1,1,1,1,1},
                new double[]{0,1,0,1,0,1,1,0,0},
                new double[]{0,1,1,0,1,0,1,0,1},
                new double[]{1,1,0,1,0,0,1,1,0}
            };

            double[] output = new double[] { 1, 0, 1 };

            PerceptronNetwork network = new PerceptronNetwork(input[0].Length);
            PerceptronTeacher teacher = new PerceptronTeacher(network, 0.05D);

            for (int x = 0; x < 1000; x++)
            {
                for (int i = 0; i < output.Length; i++)
                {
                    double error = teacher.Teach(input[i], output[i]);
                    Console.WriteLine(error);
                }
            }

            Console.ReadLine();
        }
    }

    class PerceptronNetwork
    {
        public readonly int Inputs;

        public double[] Weights { get; set; }

        private Random random = new Random();

        public double this[int index]
        {
            get { return Weights[index]; }
            set { Weights[index] = value; }
        }
        public PerceptronNetwork(int inputs)
        {
            Weights = Enumerable
                .Range(0, inputs)
                .Select(x => random.NextDouble() * 2 - 1)
                .ToArray();
            Inputs = inputs;
        }

        public double Run(double[] input)
        {
            Trace.Assert(Weights.Length == input.Length);

            double data = Enumerable
                .Range(0, Weights.Length)
                .Select(x => Weights[x] * input[x])
                .Sum();

            return data > 0 ? 1 : 0;
        }
    }

    class PerceptronTeacher
    {
        public PerceptronNetwork Network { get; private set; }

        public double Alpha { get; private set; }

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
