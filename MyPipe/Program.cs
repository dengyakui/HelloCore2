using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPipe
{
    class Program
    {
        private static List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();

        static void Main(string[] args)
        {
            Use(next => async context =>
            {
                Console.WriteLine("111");
                await next.Invoke(context);
            });

            Use(next => async context =>
            {
                Console.WriteLine("222");
                await next.Invoke(context);
            });

            Run();
            
        }

        static void Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
        }

        static void Run()
        {
            RequestDelegate end = context =>
            {
                Console.WriteLine("end...");
                return Task.CompletedTask;
            };

            foreach (var middleware in _middlewares.ToArray().Reverse())
            {
                end = middleware.Invoke(end);
            }

            end.Invoke(new Context());

            Console.ReadLine();
        }


    }
}
