namespace Lab1;

public interface IWaiterDAO : IGenericDAO<Waiter>
{
    public IList<Waiter> GetWaitersByHeadmanName(string headmanName);
}