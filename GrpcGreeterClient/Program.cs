using Grpc.Core;
using IMClient;
using IMClient.Models;
using Microsoft.Extensions.DependencyInjection;
using SP.StudioCore.Ioc;
using System;
using System.Threading.Tasks;
namespace GrpcGreeterClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IocCollection.AddService(new ServiceCollection()
                .AddSingleton(opt => new GrpcClient("Host=localhost&Port=5000&SecretKey=1000:0871175E-D0CF-4574-ACAF-6103B843BE84")));

            Console.WriteLine(IMClientHelper.SaveUser("admin", "管理员", string.Empty, MemberType.Admin).ToString());

            Console.WriteLine(IMClientHelper.Init("admin", MemberType.Admin));

            // Console.WriteLine(IMClientHelper.CreateGroup("admin", "https://img.racdn.com/upload/201908/89fa33379a75b98b.jpeg", MemberType.Admin).ToString());
        }
    }
}
