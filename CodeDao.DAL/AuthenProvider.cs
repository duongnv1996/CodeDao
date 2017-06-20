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

        public async Task<ResponseData<bool>> SignUp(FirebaseAuthType type, string accessToken, User user = null)
        {
            var response = new ResponseData<bool>
            {
                Msg = "success",
                statusCode = 200
            };
            switch (type)
            {
                case FirebaseAuthType.EmailAndPassword:
                    {
                   var signUpTask=   _provider.CreateUserWithEmailAndPasswordAsync(user.Email, user.Password, user.DisplayName);
                        await signUpTask.ContinueWith((task) =>
                         {
                             if(!task.IsFaulted  && task.Result != null)
                             {
                                 var result = task.Result;
                                 response.data = true;                         
                             }
                             else
                             {
                                 response.data = false;
                                 response.statusCode = CodeDao.MOV.Models.Constants.CODE_AUTH;
                            //     response.Msg =( (FirebaseAuthException) (task.Exception.InnerException) ).Reason.ToString();
                             }
                         });
                        return response;
                    }
            }
            return response;
        }

        public async Task<ResponseData<bool>> SendEmailToResetPasswordTask(string email)
        {
            var response = new ResponseData<bool>
            {
                Msg = "success",
                statusCode = 200
            };

            var signUpTask = _provider.SendPasswordResetEmailAsync(email);
            await signUpTask.ContinueWith((task) =>
            {
                if (!task.IsCanceled && task.IsCompleted)
                {
                    response.data = true;
                    response.Msg = "instruction was sent to your mail, Please check it out!";
                }
                else if (task.IsFaulted)
                {
                    response.data = false;
                    response.statusCode = CodeDao.MOV.Models.Constants.CODE_NOT_FOUND;
                    response.Msg = task.Exception.Message;
                }
            });
            return response;
        }
    }
}
