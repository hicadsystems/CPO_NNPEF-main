﻿using NNPEFWEB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.Service
{
    public interface IMailService
    {
        void SendEmail(MailClass mail);
    }
}
