using System;
using System.Collections.Generic;

namespace MoexXL.MoexApi
{
    public partial class StocksResponse
    {
        public Charsetinfo Charsetinfo { get; set; }
        public List<Security> Securities { get; set; }
        public List<Marketdatum> Marketdata { get; set; }
        public List<Dataversion> Dataversion { get; set; }
        public List<object> MarketdataYields { get; set; }
    }

    public partial class Charsetinfo
    {
        public string Name { get; set; }
    }

    public partial class Dataversion
    {
        public long DataVersion { get; set; }
        public long Seqnum { get; set; }
    }

    public partial class Marketdatum
    {
        public string Secid { get; set; }
        public string Boardid { get; set; }
        public object Bid { get; set; }
        public object Biddepth { get; set; }
        public object Offer { get; set; }
        public object Offerdepth { get; set; }
        public long Spread { get; set; }
        public long? Biddeptht { get; set; }
        public long? Offerdeptht { get; set; }
        public double? Open { get; set; }
        public double? Low { get; set; }
        public double? High { get; set; }
        public double? Last { get; set; }
        public long Lastchange { get; set; }
        public long Lastchangeprcnt { get; set; }
        public long Qty { get; set; }
        public double Value { get; set; }
        public double ValueUsd { get; set; }
        public double? Waprice { get; set; }
        public long Lastcngtolastwaprice { get; set; }
        public double Waptoprevwapriceprcnt { get; set; }
        public double Waptoprevwaprice { get; set; }
        public double? Closeprice { get; set; }
        public double? Marketpricetoday { get; set; }
        public double? Marketprice { get; set; }
        public double Lasttoprevprice { get; set; }
        public long Numtrades { get; set; }
        public long Voltoday { get; set; }
        public long Valtoday { get; set; }
        public long ValtodayUsd { get; set; }
        public double? Etfsettleprice { get; set; }
        public string Tradingstatus { get; set; }
        public DateTimeOffset Updatetime { get; set; }
        public double? Admittedquote { get; set; }
        public object Lastbid { get; set; }
        public object Lastoffer { get; set; }
        public double? Lcloseprice { get; set; }
        public double? Lcurrentprice { get; set; }
        public double? Marketprice2 { get; set; }
        public object Numbids { get; set; }
        public object Numoffers { get; set; }
        public double? Change { get; set; }
        public DateTimeOffset Time { get; set; }
        public object Highbid { get; set; }
        public object Lowoffer { get; set; }
        public double? Priceminusprevwaprice { get; set; }
        public double? Openperiodprice { get; set; }
        public long Seqnum { get; set; }
        public DateTimeOffset Systime { get; set; }
        public double? Closingauctionprice { get; set; }
        public long? Closingauctionvolume { get; set; }
        public double? Issuecapitalization { get; set; }
        public DateTimeOffset? IssuecapitalizationUpdatetime { get; set; }
        public string Etfsettlecurrency { get; set; }
        public long ValtodayRur { get; set; }
        public object Tradingsession { get; set; }
    }

    public partial class Security
    {
        public string Secid { get; set; }
        public string Boardid { get; set; }
        public string Shortname { get; set; }
        public double? Prevprice { get; set; }
        public long Lotsize { get; set; }
        public double Facevalue { get; set; }
        public string Status { get; set; }
        public string Boardname { get; set; }
        public long Decimals { get; set; }
        public string Secname { get; set; }
        public object Remarks { get; set; }
        public string Marketcode { get; set; }
        public string Instrid { get; set; }
        public object Sectorid { get; set; }
        public double Minstep { get; set; }
        public double? Prevwaprice { get; set; }
        public string Faceunit { get; set; }
        public DateTimeOffset Prevdate { get; set; }
        public long Issuesize { get; set; }
        public string Isin { get; set; }
        public string Latname { get; set; }
        public string Regnumber { get; set; }
        public double? Prevlegalcloseprice { get; set; }
        public double? Prevadmittedquote { get; set; }
        public string Currencyid { get; set; }
        public string Sectype { get; set; }
        public long Listlevel { get; set; }
        public DateTimeOffset Settledate { get; set; }
    }
}

