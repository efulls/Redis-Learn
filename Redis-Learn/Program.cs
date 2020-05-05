using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Redis_Learn
{
    class MyRedisClass
    {
        public string key { get; set; }
        public string name { get; set; }
        public string dob { get; set; }
        public string hoby { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            //Console.WriteLine("Saving random data in cache");
            //program.SaveBigData();

            //Console.WriteLine("Reading data from cache");
            //program.ReadData();

            program.SaveData();
            program.ReadOneData();
            Console.ReadLine();
        }

        public void SaveData()
        {

            var cache = RedisConnectorHelper.Connection.GetDatabase();
            MyRedisClass myRedisClass = new MyRedisClass
            {
                key = "3",
                name = "My Name Is Khan",
                dob = "2019-10-10",
                hoby = "gamming broo"
            };
            var value = JsonConvert.SerializeObject(myRedisClass);
            var keyName = $"PUSH-NOTIF:{myRedisClass.key}";

            cache.StringSet(keyName, value);
        }

        public void ReadOneData()
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase();
            var value = cache.StringGet("1");
            if(!value.IsNullOrEmpty)
            {
                Console.WriteLine(value);
            }
            else
            {
                Console.WriteLine("Gak ada data");
            }
        }



        public void ReadData()
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase();
            var devicesCount = 10000;
            for (int i = 0; i < devicesCount; i++)
            {
                var value = cache.StringGet($"Device_Status:{i}");
                Console.WriteLine($"Valor={value}");
            }
        }

        public void SaveBigData()
        {
            var devicesCount = 10000;
            var rnd = new Random();
            var cache = RedisConnectorHelper.Connection.GetDatabase();

            for (int i = 1; i < devicesCount; i++)
            {
                var value = rnd.Next(0, 10000);
                cache.StringSet($"Device_Status:{i}", value);
            }
        }
    }
}
