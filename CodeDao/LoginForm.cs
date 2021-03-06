﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using BunifuAnimatorNS;

using CodeDao.BUS;

using Firebase.Auth;
using  CodeDao.MOV.Models;
using User = CodeDao.MOV.Models.User;

namespace CodeDao
{
    public partial class formauth : Form
    {
        private AuthenticationBUS _authentication;

        public formauth()
        {
            InitializeComponent();
       _authentication = new AuthenticationBUS();

        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {

        }

        private async void btnSignIn_Click(object sender, EventArgs e)
        {

            String email = txtEmail.Text;
            String password = txtPassword.Text;
            var userLogin = new User()
            {
                Email = email,
                Password = password
            };
            loadingView.Visible = true;

            Task<ResponseData<User>> newUser = _authentication.SignIn(FirebaseAuthType.EmailAndPassword, userLogin, null);
            ResponseData<User> responseData = await newUser;
            loadingView.Visible = false;
            if (responseData.statusCode == Constants.CODE_SUCCESS)
            {
               // MessageBox.Show("Hi " + responseData.data.DisplayName);
                new FormMain().Show();
            }
            else
            {
                MessageBox.Show(responseData.Msg);
            }

            //Task < ResponseData < bool >> signUpUser = _authentication.ForgotPassword("1@gmail.com");
            //ResponseData<bool> responseDataSignUp = await signUpUser;
            //loadingView.Visible = false;
            //if (responseDataSignUp.statusCode == Constants.CODE_SUCCESS)
            //{
            //    MessageBox.Show("Hi " + responseDataSignUp.data);
            //}
            //else
            //{
            //    MessageBox.Show(responseDataSignUp.Msg);
            //}
           
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void formauth_Load(object sender, EventArgs e)
        {

        }
    }
}
