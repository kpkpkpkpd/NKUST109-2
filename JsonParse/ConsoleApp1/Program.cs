using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            // test
            WaterConsumption test = new WaterConsumption()
            {
                consumptionOfWater = 36685788,
                executingUnit = "宜蘭縣",
                populationServed = 421328,
                remarks = "無",
                theDailyDomesticConsumptionOfWaterPerPerson = 238,
                year = "2008-12-31T00:00:00"
            };

            //序列化
            //string jsonData = JsonConvert.SerializeObject(test);
            //Console.WriteLine(jsonData);

            //反序列化
            string jsontext = File.ReadAllText(@"..\..\..\7D1311DD-E1DE-4B01-A5ED-1FC589A7C082.json");
            //WaterConsumption model = JsonConvert.DeserializeObject<WaterConsumption>(test);

            JObject domesticConsumptionOfWater = JObject.Parse(jsontext);

            var consumption = domesticConsumptionOfWater["TaiwanWaterExchangingData"]["StatisticofWaterResourcesClass"]["TheDomesticConsumptionOfWater"].Children();

            // serialize JSON results into .NET objects
            List<WaterConsumption> results = new List<WaterConsumption>();

            foreach(JToken token in consumption)
            {
                WaterConsumption water = token.ToObject<WaterConsumption>();
                results.Add(water);
            }

            foreach (WaterConsumption result in results)
            {
                Console.WriteLine("ConsumptionOfWater: " + result.consumptionOfWater);
                Console.WriteLine("ExecutingUnit: " + result.executingUnit);
                Console.WriteLine("PopulationServed:" + result.populationServed);
                Console.WriteLine("Remarks: " + result.remarks);
                Console.WriteLine("TheDailyDomesticConsumptionOfWaterPerPerson: " + result.theDailyDomesticConsumptionOfWaterPerPerson);
                Console.WriteLine("Year: " + result.year);
                Console.WriteLine("");
            }
        }
    }
}
public class WaterConsumption
{
    public int consumptionOfWater { get; set; } // ConsumptionOfWater(生活用水量，立方公尺)
    public string executingUnit { get; set; } // ExecutingUnit(縣市別)
    public float populationServed { get; set; } // PopulationServed(年中供水人數，人)
    public string remarks { get; set; } // Remarks(備註)
    public int theDailyDomesticConsumptionOfWaterPerPerson { get; set; } // TheDailyDomesticConsumptionOfWaterPerPerson(每人每日生活用水量，公升)
    public string year { get; set; } // Year(統計年度)
} 