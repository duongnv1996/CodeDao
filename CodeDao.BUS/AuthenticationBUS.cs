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

        public  Task<ResponseData<User>> SignIn(FirebaseAuthType type, User user = null, string accessToken = null)
        {
//            if (user != null && !user.Email.Equals("") && user.Password.Equals(""))
                return _auth.SignIn(type, accessToken, user);
//            var response = new ResponseData<User>
//            {
//                statusCode = MOV.Models.Constants.CODE_NOT_FOUND,
//                Msg = "Missing Data"
//            };
//            return new Task<ResponseData<User>>(() =>
//            {
//         
//                return response;
//            });
        }

    }
}
