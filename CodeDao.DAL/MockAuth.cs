using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CodeDao.DAL.Interfaces;

using Firebase.Auth;


using CodeDao.MOV.Models;

using User = CodeDao.MOV.Models.User;

namespace CodeDao.DAL
{
    public class MockAuth : IAuthentication
    {
      

    public async   Task<ResponseData<User>> SignIn(FirebaseAuthType type, string accessToken, User user)
        {
            var response = new ResponseData<User>
            {
                data = user,
                statusCode = 200,
                Msg = "success"
            };
            return  response;
        }

         Task<ResponseData<bool>> IAuthentication.SignUp(FirebaseAuthType type, string accessToken, User user)
        {
            var response = new ResponseData<bool>
            {
                data = true,
                statusCode = 200,
                Msg = "success"
            };
            return new Task<ResponseData<bool>>(() => response);
        }

        Task<ResponseData<bool>> IAuthentication.SendEmailToResetPasswordTask(string email)
        {
        var response = new ResponseData<bool>
            {
                data = true,
                statusCode = 200,
                Msg = "success"
            };
            return new Task<ResponseData<bool>>(() => response);
        }
    }
}
