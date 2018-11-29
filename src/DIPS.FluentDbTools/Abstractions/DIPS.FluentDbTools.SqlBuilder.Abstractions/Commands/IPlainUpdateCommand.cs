﻿using System.Data;

namespace DIPS.FluentDbTools.SqlBuilder.Abstractions.Commands
{
    public interface IPlainUpdateCommand
    {
        void Execute(IDbConnection connection, IDbTransaction transaction);
    }
}