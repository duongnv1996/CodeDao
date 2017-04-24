using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeDao.DAL.Interfaces;
using CodeDao.DAL;
using CodeDao.MOV.Models;

using Firebase.Auth;

using User = CodeDao.MOV.Models.User;

namespace CodeDao.BUS
{
    public  class AuthenticationBUS
    {
        private IAuthentication _auth;

        public AuthenticationBUS()
        {
            _auth = new AuthenProvider();
        }

        public async  Task<ResponseData<User>> SignIn(FirebaseAuthType type, User user = null, string accessToken = null)
        {
            if (user != null && !user.Email.Equals("") && !user.Password.Equals(""))
            {
                return await _auth.SignIn(type, accessToken, user);
            }
           else
           {
                var response = new ResponseData<User>
                {
                    statusCode = MOV.Models.Constants.CODE_NOT_FOUND,
                    Msg = "Missing Data",
                    data = null

                };
                var tsk = newMethod();
               return await tsk;
           }
           
        }

        private async Task<ResponseData<User>> newMethod()
        {
            var response = new ResponseData<User>
            {
                statusCode = MOV.Models.Constants.CODE_NOT_FOUND,
                Msg = "Missing Data",
                data = null

            };
            return response;
        }

        public async Task<ResponseData<bool>> SignUp(FirebaseAuthType type, string accessToken, User user)
        {
            return await _auth.SignUp(type, accessToken, user);


        }
        public async Task<ResponseData<bool>> ForgotPassword(string email)
        {
            return await _auth.SendEmailToResetPasswordTask(email);


        }
    }
}
