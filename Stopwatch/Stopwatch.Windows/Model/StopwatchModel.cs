using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stopwatch.Model
{
    class StopwatchModel
    {
        private DateTime? _started;

        private TimeSpan? _previosElapsedTime;

        public bool Running
        {
            get { return _started.HasValue; }
        }

        public TimeSpan? Elapsed
        {
            get
            {
                if (_started.HasValue)
                {
                    if (_previosElapsedTime.HasValue)
                        return CalculateTimeElapsedSinceStarted() + _previosElapsedTime;
                    else
                        return CalculateTimeElapsedSinceStarted();
                }
                else
                    return _previosElapsedTime;
            }
        }

        public TimeSpan? LapTime { get; private set; }

        private TimeSpan CalculateTimeElapsedSinceStarted()
        {
            return DateTime.Now - _started.Value;
        }

        public void Start()
        {
            _started = DateTime.Now;
            if (!_previosElapsedTime.HasValue)
                _previosElapsedTime = new TimeSpan(0);
        }

        public void Stop()
        {
            if (!_started.HasValue)
                _previosElapsedTime += DateTime.Now - _started.Value;
            _started = null;
        }

        public void Reset()
        {
            _previosElapsedTime = null;
            _started = null;
            LapTime = null;

        }
        public StopwatchModel()
        {
            Reset();
        }

        public void Lap()
        {
            LapTime = Elapsed;
            OnLapTimeUpdated(LapTime);
        }

        public event EventHandler<LapEventArgs> LapTimeUpdater;

        private void OnLapTimeUpdated(TimeSpan? lapTime){
            EventHandler<LapEventArgs> lapTimeUpdated= LapTimeUpdater;
            if(lapTimeUpdated != null){
                lapTimeUpdated(this, new LapEventArgs(lapTime));
            }
        }


    }
}
