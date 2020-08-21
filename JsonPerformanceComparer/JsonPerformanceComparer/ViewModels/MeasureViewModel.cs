using JsonPerformanceComparer.Tests;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace JsonPerformanceComparer.ViewModels
{
    public class MeasureViewModel : NotifyPropertyChangedBase
    {
        private float _newtonsoftResult;
        private float _systemTextJsonResult;
        private bool _isBusy;
        private int _runCount;

        public MeasureViewModel()
        {
            MeasureCommand = new Command(() =>
            {
                NewtonsoftResult = PerformMeasurement(NewtonsoftDeserialization.DeserializeFromStream);
                SystemTextJsonResult = PerformMeasurement(async (s) => await SystemTextJsonDeserialization.DeserializeFromStreamAsync(s));
                RunCount++;
            }, () => IsBusy == false);
        }

        public ICommand MeasureCommand { get; private set; }

        public int Iterations => 5;

        public float NewtonsoftResult
        {
            get { return _newtonsoftResult; }
            set
            {
                SetProperty(ref _newtonsoftResult, value);
                OnPropertyChanged(nameof(DeltaPercent));
                OnPropertyChanged(nameof(ResultColor));
            }
        }

        public float SystemTextJsonResult
        {
            get { return _systemTextJsonResult; }
            set
            {
                SetProperty(ref _systemTextJsonResult, value);
                OnPropertyChanged(nameof(DeltaPercent));
                OnPropertyChanged(nameof(ResultColor));
            }
        }

        public float? DeltaPercent
        {
            get
            {
                if (SystemTextJsonResult != 0)
                {
                    return (SystemTextJsonResult - NewtonsoftResult) / NewtonsoftResult;
                }
                return null;
            }
        }

        /// <summary>
        /// Color of the result. This is of course quite subjective, but I think a performance gain greater than 20% is a reasonable 
        /// expectation since System.Text.Json has been touted as having much better performance than the old, complex Newtonsoft.Json.
        /// So that is green. 
        /// In the range from 20% to 0% it is orange (yellow on white is very hard to read).
        /// And worse performance is just red.
        /// This could/should have been encapsulated in a converter, but hey, this is just a test app which hopefully will be short lived...
        /// </summary>
        public Color ResultColor
        {
            get
            {
                Color rv = DeltaPercent switch
                {
                    _ when !DeltaPercent.HasValue => Color.Gray,
                    _ when DeltaPercent.Value <= -0.2 => Color.ForestGreen,
                    _ when DeltaPercent.Value > -0.2 && DeltaPercent.Value < 0 => Color.Orange,
                    _ => Color.Red
                };
                return rv;
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public int RunCount
        {
            get { return _runCount; }
            set
            {
                SetProperty(ref _runCount, value);
                OnPropertyChanged(nameof(IsFirstRun));
            }
        }

        public bool IsFirstRun => _runCount == 1;

        public float PerformMeasurement(Action<Stream> action)
        {
            using var stream = new MemoryStream(PayLoad.payload);
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < Iterations; i++)
            {
                action(stream);
                stream.Seek(0, SeekOrigin.Begin);
            }
            stopWatch.Stop();
            return (float)stopWatch.ElapsedMilliseconds / Iterations;
        }
    }
}
