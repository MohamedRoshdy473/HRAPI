﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrAPI.ConfirmationMail
{
   public interface IEmailSender
    {
        void SendEmail(Message message);
        void SendEmailString(string message);


    }
}
