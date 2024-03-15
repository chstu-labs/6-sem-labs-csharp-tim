namespace Lab4;

abstract public class DAOFactory
{
    public abstract IOrderDAO getOrderDAO();
    public abstract void destroy();
}
