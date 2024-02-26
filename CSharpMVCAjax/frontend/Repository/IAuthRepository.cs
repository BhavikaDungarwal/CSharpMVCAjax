using frontend.Models;

namespace frontend.Repository
{
    public interface IAuthRepository
    {
        bool signup(authModel auth);

        authModel login(authModel auth);
    }
}