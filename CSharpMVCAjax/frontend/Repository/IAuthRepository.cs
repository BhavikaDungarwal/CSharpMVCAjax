using frontend.Models;

namespace Frontend.Repository
{
    public interface IAuthRepository
    {
        bool signup(authModel auth);

        authModel login(authModel auth);
    }
}