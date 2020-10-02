﻿using System;
using MyIoCContainer.Core;
using MyIoCContainer.Services;

namespace MyIoCContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var mySimpleContainer = new MySimpleContainer();
            mySimpleContainer.Register<IRepository, Repository>(new Repository());

            mySimpleContainer.Resolve<IRepository>();
            
            
            var myAdvancedContainer = new MyAdvancedContainer();
            myAdvancedContainer.Register<IEmailSender, SmtpEmailSender>();
            myAdvancedContainer.Register<IRepository, Repository>();
            
            myAdvancedContainer.GetInstance(typeof (LoginController));
        }
    }
}