using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPipe
{
    class Program
    {
        private static  List<Func<RequestDelegate, RequestDelegate>> _list = new List<Func<RequestDelegate, RequestDelegate>>();
        static void Main(string[] args)
        {
            RequestDelegate end = (context) =>
            {
                Console.WriteLine("end...");
                return Task.CompletedTask;
            };

            

            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("111");
                    //return Task.CompletedTask;
                    return next.Invoke(context);

                };
            });

            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("222");
                    return next.Invoke(context);

                };
            });

            foreach (var middleware in _list.ToArray().Reverse())
            {
                end = middleware.Invoke(end);
            }


            end.Invoke(new Context());
            Console.ReadLine();
        }

        public static void Use(Func<RequestDelegate,RequestDelegate> middleware)
        {
            _list.Add(middleware);
        }
    }
}
