using ExcelDna.Integration;
using MoexXL.MoexApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MoexXL
{
    class Observable : IExcelObservable
    {
        Timer _timer;
        List<IExcelObserver> _observers;
        string _ticker;
        string _attribute;

        public Observable(string ticker, string attribute)
        {
            _timer = new Timer(timer_tick, null, TimeSpan.Zero, new TimeSpan(1, 0, 5));
            _observers = new List<IExcelObserver>();
            _ticker = ticker;
            _attribute = attribute;
        }

        public IDisposable Subscribe(IExcelObserver observer)
        {
            _observers.Add(observer);
            observer.OnNext("Загрузка...");

            return new ActionDisposable(() =>
            {
                _observers.Remove(observer);
                _timer.Dispose();
            });
        }

        async void timer_tick(object _)
        {
            object result = await MoexUtil.GetStockInfo(_ticker, _attribute);

            foreach (var obs in _observers)
            {
                if (result == null)
                    obs.OnNext(ExcelError.ExcelErrorNA);
                else
                    obs.OnNext(result);
            }
        }

        class ActionDisposable : IDisposable
        {
            Action _disposeAction;
            public ActionDisposable(Action disposeAction)
            {
                _disposeAction = disposeAction;
            }
            public void Dispose()
            {
                _disposeAction();
                Debug.WriteLine("Disposed");
            }
        }
    }
}
