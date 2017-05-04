using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDao.BUS
{
    public class Class1
    {
        public  static  void Main(string[] a)
        {
            AuthenticationBUS au = new AuthenticationBUS();
            signUpTesting(au);
        }

        private async static void signUpTesting(AuthenticationBUS au)
        {
            await au.SignUp(Firebase.Auth.FirebaseAuthType.EmailAndPassword, "", new MOV.Models.User
            {
                Email = "1@gmail.com",
                Password = "123456",
                DisplayName = "hello"
            }).ContinueWith((task) =>
            {

            });
        }
    }
}
