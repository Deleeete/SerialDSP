using System.Collections.Generic;

namespace SerialDSP
{
    public class Integration
    {
        private readonly object _lockObj = new object();
        private readonly Queue<float> _bufferIn = new Queue<float>();
        private readonly Queue<float> _bufferOut = new Queue<float>();
        private int _size;
        private float _sumIn = 0f;
        private float _sumOut = 0f;

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
        public float ValueInPhase { get => _sumIn; }
        /// <summary>
        /// Get value of the out-of-phase integration
        /// </summary>
        public float ValueOutOfPhase { get => _sumOut; }
        /// <summary>
        /// Get average value of the in-phase integration
        /// </summary>
        public float AverageInPhase { get => _sumIn / WindowSize; }
        /// <summary>
        /// Get average value of the out-of-phase integration
        /// </summary>
        public float AverageOutOfPhase { get => _sumOut / WindowSize; }

        public Integration(int windowSize = 64)
        {
            _bufferIn = new Queue<float>(windowSize);
            _bufferOut = new Queue<float>(windowSize);
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
                _bufferIn.Enqueue(valueInPhase);
                _sumIn = _sumIn + valueInPhase - _bufferIn.Dequeue();
                _bufferOut.Enqueue(valueOutOfPhase);
                _sumOut = _sumOut + valueOutOfPhase - _bufferOut.Dequeue();
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
                for (int i = 0; i < _size; i++)
                {
                    _bufferIn.Enqueue(0f);
                    _bufferOut.Enqueue(0f);
                }
                _sumIn = _sumOut = 0f;
            }
        }
    }
}
