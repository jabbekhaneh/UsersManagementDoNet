namespace UsersManagement.TokenBase.Services;

public class UserMangementTokenBaseService : IUserMangementService
{
    private IUserRepository _repository;
   
    public UserMangementTokenBaseService(IUserRepository repository)
    {
        _repository = repository;
    }

}
