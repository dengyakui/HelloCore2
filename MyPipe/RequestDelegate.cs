using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPipe
{
    public class Context { }

    public delegate Task RequestDelegate(Context context);
}
