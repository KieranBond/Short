using CQRS.Base;

namespace CQRS.Commands
{
    public class Get<T> : Command<T>
    {
        public Get ( T dto ) : base( dto )
        {
        }
    }
}
