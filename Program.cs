using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace PingSweep
{
    class Program
    {
        static void Main(string[] args)
        {
		Console.WriteLine("Start");
		var t = MainAsync();
		t.Wait();
        }
		
		
	static async Task MainAsync()
	{
		var results = await PingSweepAsync();
		foreach(var res in results)
		{
			if(res.Status == IPStatus.Success)
			{
				Console.WriteLine(res.Address.ToString());
			}
		}
	}
	static async Task<List<PingReply>> PingSweepAsync()
	{
		var tList = new List<Task<PingReply>>();
		string addBase = "10.25.25.";
		for (int i = 1; i <= 255; i++)
		{
		var t = PingAsync(addBase +  i.ToString());
				tList.Add(t);
		}
		return (await Task.WhenAll(tList)).ToList();
	}
        static async Task<PingReply> PingAsync(string ip)
        {
		Ping p = new Ping();
		return await p.SendPingAsync(ip,2000);						
        }
        
    }
}
