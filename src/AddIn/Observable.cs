using ExcelDna.Integration;
using MoexXL.MoexApi;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MoexXL
{
    class Observable : IExcelObservable
    {
        Timer timer;
        List<IExcelObserver> observers;
        string ticker;
        string attribute;
        SecurityType securityType;

        public Observable(string ticker, string attribute, SecurityType securityType)
        {
            timer = new Timer(TimerTick, null, TimeSpan.Zero, new TimeSpan(0, 1, 0));
            observers = new List<IExcelObserver>();
            this.ticker = ticker;
            this.attribute = attribute;
            this.securityType = securityType;
        }

        public IDisposable Subscribe(IExcelObserver observer)
        {
            observers.Add(observer);
            observer.OnNext(ExcelError.ExcelErrorGettingData);

            return new ActionDisposable(() =>
            {
                observers.Remove(observer);
                timer.Dispose();
            });
        }

        async void TimerTick(object _)
        {
            object result = await IssUtil.GetExcelRangeAsync(ticker, attribute, securityType);

            foreach (var obs in observers)
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
            }
        }
    }
}
