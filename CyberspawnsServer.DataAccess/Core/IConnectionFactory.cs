﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CyberspawnsServer.DataAccess
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }

        IDbConnection CreateConnection(string connection);
    }
}
