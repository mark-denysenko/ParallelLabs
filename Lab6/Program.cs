using MPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6
{
    class Program
    {
        // mpiexec -n 5 Lab6.exe
        // in folder with ASCII path
        static void Main(string[] args)
        {
            MPI.Environment.Run(ref args, comm =>
            {
                if (comm.Rank == 0)
                {
                    var random = new Random(DateTime.UtcNow.Millisecond);
                    var vector = Enumerable.Range(0, 10_000_000).Select(_ => random.NextDouble()).ToList();

                    int partitionSize = (int)Math.Ceiling((double)vector.Count / (comm.Size - 1));
                    for (int dest = 1; dest < comm.Size; ++dest)
                    {
                        comm.Send(vector.Skip((dest - 1) * partitionSize).Take(partitionSize).ToList(), dest, 0);
                    }

                    double sum = 0.0;
                    for (int dest = 1; dest < comm.Size; ++dest)
                    {
                        sum += comm.Receive<double>(dest, 1);
                    }

                    Console.WriteLine("Euclidean Norm : " + Math.Sqrt(sum));
                }
                else
                {
                    var subVector = comm.Receive<IEnumerable<double>>(0, 0);
                    var subSum = subVector.Sum(v => v * v);
                    comm.Send(subSum, 0, 1);
                }

                //PingPong(comm);
            });

        }

        public static double EuclideanNormCalculate(IEnumerable<double> vector)
        {
            double sum = 0d;

            foreach (var number in vector)
            {
                sum += number * number;
            }

            return Math.Sqrt(sum);
        }

        private static void PingPong(Intracommunicator comm)
        {
            if (comm.Rank == 0)
            {
                Console.WriteLine("Rank 0 is alive and running on " + MPI.Environment.ProcessorName);
                for (int dest = 1; dest < comm.Size; ++dest)
                {
                    Console.Write("Pinging process with rank " + dest + "...");
                    comm.Send("Ping!", dest, 0);
                    string destHostname = comm.Receive<string>(dest, 1);
                    Console.WriteLine(" Pong!");
                    Console.WriteLine("  Rank " + dest + " is alive and running on " + destHostname);
                }
            }
            else
            {
                comm.Receive<string>(0, 0);
                comm.Send(MPI.Environment.ProcessorName, 0, 1);
            }
        }

    }
}
