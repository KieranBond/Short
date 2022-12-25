using CQRS.Base;

namespace CQRS.Commands
{
    public class Handle<T> : Command<T>
    {
        public Handle ( T dto ) : base( dto )
        {
        }
    }
}
