using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodeDao.DAL.Interfaces;
using CodeDao.DAL.Utils;
using CodeDao.MOV.Models;

using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

using Constants = CodeDao.DAL.Utils.Constants;
using User = CodeDao.MOV.Models.User;

namespace CodeDao.DAL
{
    public class AuthenProvider : IAuthentication
    {
        private FirebaseAuthProvider _provider;

        public AuthenProvider()
        {
            _provider = new FirebaseAuthProvider(new FirebaseConfig(Constants.FIREBASE_API_KEY));
        }

        public async Task<ResponseData<User>> SignIn(FirebaseAuthType type, string accessToken, User user = null)
        {
            var response = new ResponseData<User>
            {
                Msg = "success",
                statusCode = 200
            };
            switch (type)
            {
                case FirebaseAuthType.EmailAndPassword:
                {
                  
                    try
                    {
                            
                        Task<FirebaseAuthLink> authTask = _provider.SignInWithEmailAndPasswordAsync(user.Email, user.Password);
                        FirebaseAuthLink authLink = await authTask;
                        if (authLink.FirebaseToken != null)
                        {
                            string idAuth = authLink.User.LocalId;
                            var client = new FirebaseClient(Constants.FIREBASE_URL_ROOT);

                            var userInfor = await client.Child("users")
                                                        .Child(idAuth)
                                                        .WithAuth(() => authLink.FirebaseToken)
                                                        .OnceSingleAsync<User>();

                            response.data = userInfor;
                            return response;
                        }
                        response.statusCode = MOV.Models.Constants.CODE_NOT_FOUND;
                        response.Msg = MOV.Models.Constants.MSG_AUTH;
                        return response;
                    }
                    catch (FirebaseException firebaseException)
                    {
                        Console.WriteLine(firebaseException.Message);
                        response.Msg = firebaseException.Message;
                        response.statusCode = firebaseException.GetHashCode();
                        return response;
                    }
                    catch (FirebaseAuthException firebaseAuthException)
                    {
                            Console.WriteLine(firebaseAuthException.Reason);
                            response.Msg = firebaseAuthException.Reason.ToString();
                            response.statusCode = firebaseAuthException.Reason.GetHashCode();
                            return response;
                        }
                }

                case FirebaseAuthType.Facebook:
                {
                    return response;
                }
                case FirebaseAuthType.Google:
                {
                    return response;
                }
            }
            return response;
        }

        public Task<ResponseData<bool>> SignUp(FirebaseAuthType type, string accessToken, User user = null)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<bool>> SendEmailToResetPasswordTask(string email)
        {
            throw new NotImplementedException();
        }
    }
}
