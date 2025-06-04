using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApplicationInsights.Tests
{
    public class AIRequestObject
    {
        public DateTime? time { get; set; }
        public Guid? iKey { get; set; }
        public string name { get; set; }
        public Dictionary<string, string> tags { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string baseType { get; set; }
        public Basedata baseData { get; set; }
    }

    public class Basedata
    {
        public int? ver { get; set; }
        public string name { get; set; }
        public string message { get; set; }
        public int? severityLevel { get; set; }
        public string url { get; set; }
        public string duration { get; set; }
        public Dictionary<string, string> properties { get; set; }
        public Dictionary<string, string> measurements { get; set; }
        public string id { get; set; }
        public string perfTotal { get; set; }
        public string networkConnect { get; set; }
        public string sentRequest { get; set; }
        public string receivedResponse { get; set; }
        public string domProcessing { get; set; }
        public Metric[] metrics { get; set; }
        public Exception[] exceptions { get; set; }
        public string resultCode { get; set; }
        public bool? success { get; set; }
        public string data { get; set; }
        public string target { get; set; }
        public string type { get; set; }
    }

    public class Metric
    {
        public string name { get; set; }
        public int kind { get; set; }
        public int value { get; set; }
        public int count { get; set; }
        public int min { get; set; }
        public int max { get; set; }
    }

    public class Exception
    {
        public string typeName { get; set; }
        public string message { get; set; }
        public bool hasFullStack { get; set; }
        public string stack { get; set; }
        public ParsedStack[] parsedStack { get; set; }
    }

    public class ParsedStack
    {
        public int level { get; set; }
        public string method { get; set; }
        public string assembly { get; set; }
        public string fileName { get; set; }
        public int line { get; set; }
    }
}
