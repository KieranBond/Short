namespace CQRS.Base
{
    public abstract class Command<T>
    {
        public readonly T Dto;

        protected Command ( T dto )
        {
            Dto = dto;
        }
    }
}