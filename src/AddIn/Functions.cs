using ExcelDna.Integration;
using MoexXL.MoexApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MoexXL
{
    public static class Functions
    {
        [ExcelFunction]
        public static object MOEX(string ticker)
        {
            string functionName = "MOEX";
            return ExcelAsyncUtil.Observe(functionName, ticker, () => new ObservableResponse(ticker));
        }
    }

    class ObservableResponse : IExcelObservable
    {
        Timer _timer;
        List<IExcelObserver> _observers;
        string _ticker;

        public ObservableResponse(string ticker)
        {
            _timer = new Timer(timer_tick, null, TimeSpan.Zero, new TimeSpan(0, 0, 5));
            _observers = new List<IExcelObserver>();
            _ticker = ticker;
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

        void timer_tick(object _)
        {
            string json = HttpUtil.GetStocks().GetAwaiter().GetResult();
            Debug.WriteLine("Got Response");

            StocksResponse stocks = JsonConvert.DeserializeObject<IEnumerable<StocksResponse>>(json).First(x => x.Securities != null);
            Debug.WriteLine("Deserialized");

            var result = stocks.Securities.Where(s => s.Secid == _ticker).FirstOrDefault().Secname;

            foreach (var obs in _observers)
                obs.OnNext(result);
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
