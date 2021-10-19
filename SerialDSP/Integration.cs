using System;
using System.Collections.Generic;
using System.Linq;

namespace SerialDSP
{
    public class Integration
    {
        private readonly object _lockObj = new object();
        private readonly Queue<double> _bufferIn = new Queue<double>();
        private readonly Queue<double> _bufferOut = new Queue<double>();
        private readonly Queue<double> _bufferNorm = new Queue<double>();
        private int _size;

        /// <summary>
        /// The size of the integration window
        /// </summary>
        public int WindowSize 
        {
            get => _size;
            set
            {
                lock (_lockObj)
                {
                    _size = value;
                    Reset();    //reset queue after size changed
                }
            }
        }
        /// <summary>
        /// Get value of the in-phase integration
        /// </summary>
        public double SumInPhase { get; private set; }
        /// <summary>
        /// Get value of the out-of-phase integration
        /// </summary>
        public double SumOutOfPhase { get; private set; }
        /// <summary>
        /// Get value of the integration
        /// </summary>
        public double SumNorm { get; private set; }
        /// <summary>
        /// Get value of the sum of squre error
        /// </summary>
        public double SumNormSE { get; private set; }
        /// <summary>
        /// Get average value of the in-phase integration
        /// </summary>
        public double AverageInPhase { get; private set; }
        /// <summary>
        /// Get average value of the out-of-phase integration
        /// </summary>
        public double AverageOutOfPhase { get; private set; }
        /// <summary>
        /// Get average value of the integration
        /// </summary>
        public double AverageNorm { get; private set; }
        /// <summary>
        /// Get standard deviation of norm
        /// </summary>
        public double StandardDeviationNorm { get; private set; }

        public Integration(int windowSize = 64)
        {
            _bufferIn = new Queue<double>(windowSize);
            _bufferOut = new Queue<double>(windowSize);
            _size = windowSize;
            //Fill initial window with zeros
            Reset();
        }
        /// <summary>
        /// Roll window to the next value
        /// </summary>
        /// <param name="valueInPhase">The next value</param>
        public void Roll(float valueInPhase, float valueOutOfPhase)
        {
            lock (_lockObj)
            {
                double headIn = _bufferIn.Dequeue();
                _bufferIn.Enqueue(valueInPhase);
                double headOut = _bufferOut.Dequeue();
                _bufferOut.Enqueue(valueOutOfPhase);
                double headNorm = _bufferNorm.Dequeue();

                //rolling in-phase
                SumInPhase = SumInPhase + valueInPhase - headIn;
                AverageInPhase = SumInPhase / _size;
                //rolling out-of-phase
                SumOutOfPhase = SumOutOfPhase + valueOutOfPhase - headOut;
                AverageOutOfPhase = SumOutOfPhase / _size;
                //rolling norms
                double normSquare = AverageInPhase * AverageInPhase + AverageOutOfPhase * AverageOutOfPhase;
                double norm = Math.Sqrt(normSquare);
                _bufferNorm.Enqueue(norm);
                double headNormSE = SquredError(headNorm, AverageNorm);  //calc old SE before updating mean
                SumNorm = SumNorm + norm - headNorm;                                  //roll sum of norms
                AverageNorm = SumNorm / _size;
                double normDiffSquare = SquredError(norm, AverageNorm);
                SumNormSE = SumNormSE + normDiffSquare - headNormSE;     //roll sum of norms^2
                //standard deviation
                StandardDeviationNorm = Math.Sqrt(SumNormSE);
            }
        }
        /// <summary>
        /// Fill window with zeros
        /// </summary>
        public void Reset()
        {
            lock (_lockObj)
            {
                _bufferIn.Clear();
                _bufferOut.Clear();
                _bufferNorm.Clear();
                for (int i = 0; i < _size; i++)
                {
                    _bufferIn.Enqueue(0);
                    _bufferOut.Enqueue(0);
                    _bufferNorm.Enqueue(0);
                }
                SumInPhase = SumOutOfPhase = SumNorm = SumNormSE = 0;
                AverageInPhase = AverageOutOfPhase = AverageNorm = 0;
                StandardDeviationNorm = 0;
            }
        }

        private static double SquredError(double x, double meanX)
        {
            double diff = x - meanX;
            return diff * diff;
        }
    }
}
