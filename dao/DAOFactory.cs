namespace Lab1;

abstract public class DAOFactory
{
    public abstract IStudentDAO getStudentDAO();
    public abstract IGroupDAO getGroupDAO();
    public abstract IOrderDAO getOrderDAO();
    public abstract IWaiterDAO getWaiterDAO();
    public abstract void destroy();
}
