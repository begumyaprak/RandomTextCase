using RandomTextCase.Models;

namespace RandomTextCase.SqlHelper
{
    public interface IUserDataRepository
    {
        List<Input> GetList();
        ResponseViewModel InsertData(Input entity);
    }
}