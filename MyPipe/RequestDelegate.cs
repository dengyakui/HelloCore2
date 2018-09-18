using System.Threading.Tasks;

namespace MyPipe
{
    public delegate Task RequestDelegate(Context context);

    public class Context
    {
    }
}
