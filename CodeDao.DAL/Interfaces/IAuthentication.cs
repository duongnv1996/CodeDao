using System.Threading.Tasks;

using CodeDao.MOV.Models;

using Firebase.Auth;

using User = CodeDao.MOV.Models.User;

namespace CodeDao.DAL.Interfaces
{
    public interface IAuthentication
    {
        Task<ResponseData<User>> SignIn(FirebaseAuthType type,string accessToken,User user = null);
        Task<ResponseData<bool>> SignUp(FirebaseAuthType type,string accessToken,User user = null);

        Task<ResponseData<bool>> SendEmailToResetPasswordTask(string email);
    }
}
